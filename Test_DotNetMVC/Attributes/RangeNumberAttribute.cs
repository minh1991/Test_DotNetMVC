using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Attributes
{
    public class RangeNumberAttribute : ValidationAttribute
    {
        private int _minNum;            //  最小数値
        private int _maxNum;            //  最大数値

        /// <summary>
        /// コンストラクタ
        /// 範囲数値チェック
        /// </summary>
        /// <param name="minNum">最小数値</param>
        /// <param name="maxNum">最大数値</param>
        public RangeNumberAttribute(int minNum, int maxNum)
        {
            _minNum = minNum; 
            _maxNum = maxNum;
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
            // 範囲数値チェック
            int chkValueInt = int.Parse(chkValue);
            if ((chkValueInt > _maxNum) || (chkValueInt < _minNum))
            {
                return new ValidationResult(FormatErrorMessage("2", displayName));
            }
            return ValidationResult.Success!;
        }

        /// <summary>
        /// エラーメッセージカスタマイズ
        /// メッセージID：MSGxxxxx
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
                    errorMessage = MessageUtil.MSGTest06(_minNum.ToString() ,_maxNum.ToString());
                    break;
            }

            return errorMessage;
        }
    }
}
