using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Attributes
{
    public class ZenkakuFormatAttribute : ValidationAttribute
    {
        // 文字コードを指定
        static Encoding sjisEnc = Encoding.GetEncoding("Shift-JIS");

        /// <summary>
        /// コンストラクタ
        /// 全角チェック
        /// </summary>
        /// <param></param>
        // バリデーションチェック
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            // 値未入力はチェックしない
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success!;
            }
            string chkValue = value.ToString()!;
            int byteLenght = sjisEnc.GetByteCount(chkValue);
            var displayName = context.DisplayName;

            //全角チェック
            if (byteLenght != chkValue.Length * 2)
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
