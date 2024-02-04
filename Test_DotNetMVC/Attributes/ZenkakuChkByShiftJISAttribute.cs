using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Attributes
{
    public class ZenkakuChkByShiftJISAttribute : ValidationAttribute
    {
        static Encoding sjisEnc = Encoding.GetEncoding("Shift-JIS");
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success!;
            }
            string chkValue = value.ToString()!;
            int byteLenght = sjisEnc.GetByteCount(chkValue);
            var displayName = context.DisplayName;
            if (byteLenght != chkValue.Length * 2)
            {
                return new ValidationResult(FormatErrorMessage(displayName));
            }
            return ValidationResult.Success!;
        }

        public override string FormatErrorMessage(string _displayName)
        {
            string errorMessage = MessageUtil.MSG001E(_displayName);
            return errorMessage;
        }
    }
}
