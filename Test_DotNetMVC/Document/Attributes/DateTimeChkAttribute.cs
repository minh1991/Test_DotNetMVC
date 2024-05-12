using System.ComponentModel.DataAnnotations;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Attributes
{
    public class DateTimeChkAttribute : ValidationAttribute
    {
        private readonly string _formatDateTime;
        public DateTimeChkAttribute(string formatDateTime)
        {
            this._formatDateTime = formatDateTime;
        }
        protected override ValidationResult IsValid (object value, ValidationContext context)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success!;
            }
            string chkValue = value.ToString()!;
            var displayName = context.DisplayName;
            if (!DateTime.TryParseExact(chkValue, _formatDateTime, null, System.Globalization.DateTimeStyles.None, out _))
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
