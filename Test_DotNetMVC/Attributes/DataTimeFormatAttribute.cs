using System.ComponentModel.DataAnnotations;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Attributes
{
    public class DataTimeFormatAttribute : ValidationAttribute
    {
        private readonly string _formatDateTime;     //  日時フォーマット

        /// <summary>
        /// コンストラクタ
        /// 日時フォーマット
        /// </summary>
        /// <param name="_formatDateTime">日時フォーマット</param>
        /// 日時フォーマット
        /// ・yyyy/MM
        /// ・yyyy/MM/dd
        /// ・yyyy/MM/dd H:m:ss
        /// ・H:m:ss
        public DataTimeFormatAttribute(string formatDateTime)
        {
            this._formatDateTime = formatDateTime;
        }
        // バリデーションチェック
        protected override ValidationResult IsValid (object value, ValidationContext context)
        {
            // 値未入力はチェックしない
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success!;
            }
            string chkValue = value.ToString()!;
            var displayName = context.DisplayName;
            // 日付チェック
            if (!DateTime.TryParseExact(chkValue, _formatDateTime, null, System.Globalization.DateTimeStyles.None, out _))
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
            string errorMessage = MessageUtil.MSG001E(_displayName);
            return errorMessage;
        }
    }
}
