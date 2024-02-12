

function addBeforeunloadEvent() {
    window.addEventListener('beforeunload', cancelEvent);
}
function cancelEvent(e) {
    e.preventDefault();
    e.returnValue = '';
}

function checkAndExecution(fromData, checkerUrl, processUrl, refreshElementId) {
    return new Promise((resolve, reject) => {
       
        sendAjax(
            checkerUrl,
            fromData,
        ).then(data => {
            //return alertOrRefresh(data, refreshElementId);
        }).then(() => {
            return sendAjax(
                processUrl,
                fromData,
            );
        }).then(data => {
            //toastOrRefresh(data, refreshElementId);
            resolve(data.json);
        }).catch(() => {
            reject();
        });
    });
}
function sendAjax(url, formData = null, contentType = null) {
    
    return new Promise((resolve, reject) => {
        //TOAST.close();
        validClear();
        spinnerSwitcher();
        let headers = new Headers();
        
        if (contentType != null) {
            headers.set('Content-Type', contentType);
        }
        headers.set('X-Requested-With', 'XMLHttpRequest');
        fetch(url, {
            method: 'POST',
            headers,
            body: formData

        }).then(response => {
            if (!response.ok) {
                window.removeEventListener('beforeunload', cancelEvent);
                throw new Error('Ajax error');
            }
            let contentType = response.headers.get('content-type');
            
            if (contentType.match('json')) {
                
                return response.json();
            } else if (contentType.match('text/csv') ||
                contentType.match('application/pdf') ||
                contentType.match('application/vnd.openxmlformats-officedocument.spreadsheetml.sheet')) {
                let temp = /''(.+)/.exec(response.headers.get('content-disposition'));
                let fileName = temp[1] !== undefined ? decodeURIComponent(temp[1]) : '';
                return response.blob().then(blob => { return { blob, fileName } });
            } else {
                return response.text();
            }
        }).then(data => {
            
            if (data.systemError) {
                window.location.href = makeSystemErrorUrl(DATA.stackTrace, data.exceptionMessage);
                return;
            }
            if (data.result == false) {
                TOAST.fire({
                    icon: data.type,
                    title: data.title,
                    html: data.message,
                });
                reject();
                return;
            }
            resolve(data);

        }).catch(exception => {
            window.location.href = makeSystemErrorUrl(exception);
        }).finally(() => {
            spinnerSwitcher();
        });
    });
}
function validClear(parentElement = null) {
    let elements = parentElement != null ?
        document.querySelectorAll(`${parentElement} .is-invalid`) :
        document.getElementsByClassName('is-invalid');

    [...elements].forEach(element => {
        element.classList.remove('is-invalid');
    });
    console.log("validClear");
}
function spinnerSwitcher() {
    var element = document.getElementById('spinner-bg');
    //console.log("spinnerSwitcher", element);
    element.classList.toggle('d-flex');
    element.classList.toggle('d-none');
}
function makeSystemErrorUrl(stackTrace, exceptionMessage = null) {
    return exceptionMessage == null ?
        `/Error?exceptionMessage=Ajax errors.&stackTrace=${stackTrace}` :
        `/Error?exceptionMessage=${exceptionMessage}&stackTrace=${stackTrace}`;
}

function makeFormData(formElement) {
    let target = [];
    [...formElement].forEach(e => {
        if (e.disabled) {
            target.push(e.id);
            e.removeAttribute('disabled');
        }
    });
    let formData = new FormData(formElement);
    [...formElement].forEach(e => {
        if (target.includes(e.id)) {
            e.setAttribute('disabled', '');
        }
    });

    return formData;
}

function tabulatorOrRefresh(data, refreshElementId, tabulatorInstance) {
    if (data.result) {
        tabulatorInstance.replaceData(data.json);
        tableResize(tabulatorInstance);
    } else {
        document.getElementById(refreshElementId).innerHTML = data;
        firstIsInvalidFocus();
        beFlatpickr();
    }
}

function toastOrRefresh(data, refreshElementId) {
    if (data.result) {
        TOAST.fire({
            icon: data.type,
            title: data.title,
            html: data.message,
        });
    } else {
        document.getElementById(refreshElementId).innerHTML = data;
        firstIsInvalidFocus();
        beFlatpickr();
    }
}
function firstIsInvalidFocus() {
    let isInvalidElements = document.querySelectorAll(':not(div).is-invalid,.is-invalid input');
    if (0 < isInvalidElements.length) {
        isInvalidElements[0].focus();
    }
}
function beFlatpickr() {
    let fp = flatpickr('.flatpickr', FLATPICKR);
    flatpickrAddEvent(fp);
    let fpm = flatpickr('.flatpickr-ym', FLATPICKR_YM);
    flatpickrAddEvent(fpm);
}
function flatpickrAddEvent(flatpickrInstance) {
    let fp;
    if (typeof flatpickrInstance.forEach === 'function') {
        fp = flatpickrInstance;
    } else {
        fp = [flatpickrInstance];
    }

    fp.forEach(item => {
        item.altInput.addEventListener('focus', e => {
            selectTextAll(e);
        });
        if (item.input.classList.contains('is-invalid')) {
            item.altInput.classList.add('is-invalid');
        }
    });
}
function selectTextAll(e) {
    e.target.select();
}