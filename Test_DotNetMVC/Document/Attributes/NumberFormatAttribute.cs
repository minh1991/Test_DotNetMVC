using DocumentFormat.OpenXml.Bibliography;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Attributes
{
    public class NumberFormatAttribute : ValidationAttribute
    {
        private int _length;            //  最大桁数
        private bool _acceptNegative;   //  マイナス （true:マイナスOK   false:マイナス無し）

        /// <summary>
        /// コンストラクタ
        /// 数値チェック
        /// </summary>
        /// <param name="length">最大桁数</param>
        /// <param name="acceptNegative">マイナス</param>
        public NumberFormatAttribute(int length, bool acceptNegative)
        {
            // 全体桁数は1より小さくならない
            _length = length <= 0 ? 1 : length;
            _acceptNegative = acceptNegative;
        }

        // バリデーションチェック
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            // 値未入力はチェックしない
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success!;
            }
            string chkValue = value.ToString()!;
            var displayName = context.DisplayName;
            // 数字型チェック
            if (Regex.IsMatch(chkValue, @"^-?(?=(\d|\.\d))\d*(\.\d*)?$") == false)
            {
                return new ValidationResult(FormatErrorMessage("1", displayName));
            }
            // 先頭の「0」チェック
            if (chkValue.StartsWith("0") && chkValue.Length > 1)
            {
                return new ValidationResult(FormatErrorMessage("2", displayName));

            }
            // マイナスチェック
            if (_acceptNegative == false && chkValue!.StartsWith("-"))
            {
                return new ValidationResult(FormatErrorMessage("3", displayName));
            }
            // 桁数チェック
            if(chkValue.Trim().Length > _length)
            {
                displayName = _length.ToString();
                return new ValidationResult(FormatErrorMessage("4", displayName));
            }
            return ValidationResult.Success!;
        }

        /// <summary>
        /// エラーメッセージカスタマイズ
        /// メッセージID：MSGTest02
        /// </summary>
        /// <param name="_displayName"></param>
        /// <returns></returns>
        public string FormatErrorMessage(string _errCode, string _displayName)
        {
            string errorMessage = "";
            switch (_errCode)
            {
                case "1":
                    errorMessage = MessageUtil.MSGTest02(_displayName);
                    break;
                case "2":
                    errorMessage = MessageUtil.MSGTest03(_displayName);
                    break;
                case "3":
                    errorMessage = MessageUtil.MSGTest04(_displayName);
                    break;
                case "4":
                    errorMessage = MessageUtil.MSGTest05(_displayName);
                    break;
            }
            
            return errorMessage;
        }
    }
}
