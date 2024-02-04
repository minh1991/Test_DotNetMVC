using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Test_DotNetMVC.Models.RequestModel;
using Test_DotNetMVC.Models.ResponseModel;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Controllers
{
    public class TestValidController : Controller
    {
        static Encoding sjisEnc = Encoding.GetEncoding("Shift-JIS");
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult TestCheck(TestValidModelByFunction model)      // nếu không gọi các check bất đồng bộ
        public async Task<IActionResult> TestCheckByFunction(TestValidModelByFunction model)
        {
            if (!ModelState.IsValid)
            {
                var errorMsg = ModelState.Select(e => e.Value!.Errors).First(y => y.Count > 0)[0].ErrorMessage;
                return Json(new JsonResultModel
                {
                    Title = MessageUtil.MSG001E(errorMsg),
                    Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                });
            }

            #region 必須チェック
            var HissuChk_list = new List<string> {
                nameof(TestValidModelByFunction.HissuChk)
            };
            foreach (var propertyName in HissuChk_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                if (!HissuChk(propertyValue))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region テキスト長さチェック
            var TxtLenghtChk_list = new List<(string PropertyName, int maxLength)> {
                (nameof(TestValidModelByFunction.TxtLenghtChk), 3)
            };
            foreach (var (propertyName, maxLength) in TxtLenghtChk_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                if (!TxtLenghtChk(propertyValue, maxLength))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region 全角チェック By ShiftJIS
            var zenkakuChkByShiftJIS_list = new List<string> {
                nameof(TestValidModelByFunction.ZenkakuChk)
            };
            foreach (var propertyName in zenkakuChkByShiftJIS_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                if (!ZenkakuChkByShiftJIS(propertyValue))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region 全角チェック By Regex
            var zenkakuChkByRegex_list = new List<string> {
                nameof(TestValidModelByFunction.ZenkakuChk)
            };
            foreach (var propertyName in zenkakuChkByRegex_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                if (!ZenkakuChkByRegex(propertyValue))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region 半角チェック By ShiftJIS
            var hankakuChkByShiftJIS_list = new List<string> {
                nameof(TestValidModelByFunction.HankakuChk)
            };
            foreach (var propertyName in hankakuChkByShiftJIS_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                if (!HankakuChkByShiftJIS(propertyValue))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region 半角チェック By Regex
            var hankakuChkByRegex_list = new List<string> {
                nameof(TestValidModelByFunction.HankakuChk)
            };
            foreach (var propertyName in hankakuChkByRegex_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                if (!HankakuChkByRegex(propertyValue))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region 数値チェック
            var numberChk_list = new List<(string PropertyName, int prmInt, int prmDecimal, bool acceptNegative)> {
                (nameof(TestValidModelByFunction.NumberChk), 3, 2, false)
            };
            foreach (var (propertyName, prmInt, prmDecimal, acceptNegative) in numberChk_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                if (!NumberChk(propertyValue, prmInt, prmDecimal, acceptNegative))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region 日時チェック
            var DateTimeChk_list = new List<(string PropertyName, string formatDateTime)> {
                (nameof(TestValidModelByFunction.DateTimeChk), "yyyy/MM/dd H:m:ss")
            };
            foreach (var (propertyName, formatDateTime) in DateTimeChk_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                if (!DateTimeChk(propertyValue, formatDateTime))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region 日時範囲内チェック
            var GreaterDateChk_list = new List<(string dataTimeStart, string dataTimeEnd, string formatDateTime)> {
                (nameof(TestValidModelByFunction.GreaterDateChkStart), nameof(TestValidModelByFunction.GreaterDateChkEnd), "yyyy/MM/dd")
            };
            foreach (var (dataTimeStart, dataTimeEnd, formatDateTime) in GreaterDateChk_list)
            {
                var propertyDataTimeStart = GetPropertyValue(model, dataTimeStart);
                var propertyDataTimeEnd = GetPropertyValue(model, dataTimeEnd);
                if (!GreaterDateChk(propertyDataTimeStart, propertyDataTimeEnd, formatDateTime))
                {
                    var displayDataTimeStartName = GetDisplayName(model, dataTimeStart);
                    var displayDataTimeEndName = GetDisplayName(model, dataTimeEnd);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG002E(displayDataTimeStartName, displayDataTimeEndName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region 郵便番号チェック
            var JPYubinBangoChk_list = new List<string> {
                nameof(TestValidModelByFunction.JPYubinBangoChk)
            };
            foreach (var propertyName in JPYubinBangoChk_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                var chkResult = await JPYubinBangoChk(propertyValue);
                if (!chkResult)
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            #region 電話番号チェック
            var PhoneNumberChk_list = new List<string> {
                nameof(TestValidModelByFunction.PhoneNumberChk)
            };
            foreach (var propertyName in PhoneNumberChk_list)
            {
                var propertyValue = GetPropertyValue(model, propertyName);
                if (!PhoneNumberChk(propertyValue))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG001E(displayName),
                        Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                    });
                }
            }
            #endregion

            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG001(),
                Type = JsonResultModel.MessageTypeEnum.success.ToString(),
            });
        }

        [HttpPost]
        public IActionResult TestCheckByAttribute(TestValidModelByAttribute model)
        {
            if (!ModelState.IsValid)
            {
                var errorMsg = ModelState.Select(e => e.Value!.Errors).First(y => y.Count > 0)[0].ErrorMessage;
                return Json(new JsonResultModel
                {
                    Title = MessageUtil.MSG001E(errorMsg),
                    Type = JsonResultModel.MessageTypeEnum.error.ToString(),
                });
            }
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG001(),
                Type = JsonResultModel.MessageTypeEnum.success.ToString(),
            });
        }

        #region GetPropertyValue
        /// <summary>
        /// プロパティの値を取得します。
        /// </summary>
        public static object GetPropertyValue(object model, string propertyName)
        {
            var propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model, null);

            return propertyValue!;
        }
        #endregion

        #region GetDisplayName
        /// <summary>
        /// プロパティの表示名を取得します。
        /// </summary>
        public static string GetDisplayName(object model, string propertyName)
        {
            var displayAttribute = model.GetType().GetProperty(propertyName)?.GetCustomAttributes(typeof(DisplayAttribute), true)
                                  .FirstOrDefault() as DisplayAttribute;

            return displayAttribute?.Name ?? propertyName;
        }
        #endregion

        #region HissuChk
        public bool HissuChk(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()) == true
                || string.IsNullOrEmpty(_propertyValue?.ToString()) == true)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region TxtLenghtChk
        public bool TxtLenghtChk(object _propertyValue, int maxLenght)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                if (chkValue.Length > maxLenght)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region ZenkakuChkByShiftJIS
        public bool ZenkakuChkByShiftJIS(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue =  _propertyValue.ToString()!;
                int byteLenght = sjisEnc.GetByteCount(chkValue);
                if (byteLenght != chkValue.Length * 2)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region ZenkakuChkByRegex
        public bool ZenkakuChkByRegex(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                string regexPattern = @"^[^ -~｡-ﾟ]+$";
                if (Regex.IsMatch(chkValue, regexPattern) == false)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region HankakuChkByShiftJIS
        public bool HankakuChkByShiftJIS(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                int byteLenght = sjisEnc.GetByteCount(chkValue);
                if (byteLenght != chkValue.Length)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region HankakuChkByRegex
        public bool HankakuChkByRegex(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                string regexPattern = @"^[a-zA-Z0-9!-/:-@¥[-`{-~]*$";
                if (Regex.IsMatch(chkValue, regexPattern) == false)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region NumberChk
        /*******************
             Đối với Model kiểu Int hoặc Long
             Khi nhập không phải số (a123) sẽ tự chuyển thành giá trị defaul -> Check bằng ModelState.IsValid
             Khi nhập giá trị bắt đầu bằng 0 (00123) sẽ tự chuyển thành 123 -> Check bằng ModelState.IsValid MaxLength
             
             Các case đã tái hiện bằng NumberChk
             +) âm dương
             +) check lenght
             +) Model kiểu decimal: check length phần nguyên, phần thập phân
             
             Model kiểu string sẽ check được toàn bộ
         ********************/
        public static bool NumberChk(object _propertyValue, int prmInt, int prmDecimal, bool acceptNegative = false)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            } else
            {
                string chkValue = _propertyValue.ToString()!;
                if (Regex.IsMatch(chkValue, @"^-?(?=(\d|\.\d))\d*(\.\d*)?$") == false)
                {
                    return false;
                }
                if (chkValue.StartsWith("0") && chkValue.Length > 1)
                {
                    return false;
                }
                if (acceptNegative == false && chkValue!.StartsWith("-"))
                {
                    return false;
                }
                var parts = chkValue!.Replace("-", "").Split('.');
                if (parts[0].Length > prmInt)
                {
                    return false;
                }
                if (parts.Length == 2 && parts[1].Length > prmDecimal)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region DateTimeChk
        /*******************
            Check khi Model kiểu string
                "2022/01/28"            "yyyy/MM/dd"
                "2022/01/28 08:34:56"   "yyyy/MM/dd H:m:ss"
                "2022/01/28 8:34:56"    "yyyy/MM/dd H:m:ss"
                "15:03:25"              "H:m:ss"
         ********************/
        public static bool DateTimeChk(object _propertyValue, string formatDateTime)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                DateTime result;
                if (!DateTime.TryParseExact(chkValue, formatDateTime, null, System.Globalization.DateTimeStyles.None, out result))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region GreaterDateChk
        /*******************
            Check khi Model kiểu string
                "2022/01/28"            "yyyy/MM/dd"
                "2022/01/28 08:34:56"   "yyyy/MM/dd H:m:ss"
                "2022/01/28 8:34:56"    "yyyy/MM/dd H:m:ss"
                "15:03:25"              "H:m:ss"
         ********************/
        public static bool GreaterDateChk(object _dataTimeStart, object _dataTimeEnd, string formatDateTime)
        {
            if (string.IsNullOrWhiteSpace(_dataTimeStart?.ToString()) || string.IsNullOrWhiteSpace(_dataTimeEnd?.ToString()))
            {
                return true;
            }
            else
            {
                string dataTimeStart = _dataTimeStart.ToString()!;
                string dataTimeEnd = _dataTimeEnd.ToString()!;
                if (!DateTime.TryParseExact(dataTimeStart, formatDateTime, null, System.Globalization.DateTimeStyles.None, out DateTime tmpStart))
                {
                    return false;
                }
                if (!DateTime.TryParseExact(dataTimeEnd, formatDateTime, null, System.Globalization.DateTimeStyles.None, out DateTime tmpEnd))
                {
                    return false;
                }
                if (tmpStart > tmpEnd)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region JPYubinBangoChk
        /*******************
            Có sử dụng API này để lấy ra thông tin của 郵便番号.
            {
	            "message": null,
	            "results": [
		            {
			            "address1": "愛知県",
			            "address2": "名古屋市中区",
			            "address3": "大須",
			            "kana1": "ｱｲﾁｹﾝ",
			            "kana2": "ﾅｺﾞﾔｼﾅｶｸ",
			            "kana3": "ｵｵｽ",
			            "prefcode": "23",
			            "zipcode": "4600011"
		            }
	            ],
	            "status": 200
            }
         ********************/
        public static async Task<bool> JPYubinBangoChk(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string postalCode = _propertyValue.ToString()!;
                if (postalCode.Length == 7 && int.TryParse(postalCode, out _))
                {
                    string chkApiUrl = $"https://zipcloud.ibsnet.co.jp/api/search?zipcode={postalCode}";
                    using (HttpClient httpClient = new HttpClient())
                    {
                        try
                        {
                            var response = await httpClient.GetAsync(chkApiUrl);
                            if (!response.IsSuccessStatusCode)
                            {
                                return false;
                            }
                            else
                            {
                                var data = await response.Content.ReadAsStringAsync();
                                var addressInfo = JsonSerializer.Deserialize<YubinInfoModel>(data);
                                if (addressInfo!.results == null)
                                {
                                    return false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }

                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region PhoneNumberChk
        public bool PhoneNumberChk(object _propertyValue)
        {
            if (string.IsNullOrWhiteSpace(_propertyValue?.ToString()))
            {
                return true;
            }
            else
            {
                string chkValue = _propertyValue.ToString()!;
                string regexPattern1 = @"^[0-9\-]+$";
                if (Regex.IsMatch(chkValue, regexPattern1) == false)
                {
                    return false;
                }
                string numericPart = new string(chkValue.Where(char.IsDigit).ToArray());
                if (numericPart.Length < 10)
                {
                    return false;
                }
                else
                {
                    string regexPattern2 = @"^0\d{9,10}$";
                    if (Regex.IsMatch(numericPart, regexPattern2) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region test

        #endregion

    }
}
