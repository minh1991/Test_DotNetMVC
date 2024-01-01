

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
            console.log("contentType", contentType)
            if (contentType.match('json')) {
                console.log("contentType", contentType);
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
            console.log("data", data);
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