using System.ComponentModel.DataAnnotations;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Attributes
{
    public class FixedLengthAttribute : ValidationAttribute
    {
        private int _length;    // 長さ（数値）

        /// <summary>
        /// コンストラクタ
        /// 固定桁数チェック
        /// </summary>
        /// <param name="length">固定桁数</param>
        public FixedLengthAttribute(int length)
        {
            _length = length;
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
            var displayName = _length.ToString();
            // 固定桁数チェック
            if (chkValue.Trim().Length != _length)
            {
                return new ValidationResult(FormatErrorMessage(displayName));
            }
            return ValidationResult.Success!;
        }

        /// <summary>
        /// エラーメッセージカスタマイズ
        /// メッセージID：MSGxxxxx
        /// </summary>
        /// <param name="_displayName"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string _displayName)
        {
            string errorMessage = MessageUtil.MSGTest01(_displayName);
            return errorMessage;
        }
    }
}
