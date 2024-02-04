using System.ComponentModel.DataAnnotations;

namespace Test_DotNetMVC.Models.RequestModel
{
    public class TestValidModelByFunction
    {
        [Display(Name = "必須チェック")]
        public string? HissuChk { get; set; } = null;

        [Display(Name = "テキスト長さチェック")]
        public string? TxtLenghtChk { get; set; } = null;
        
        [Display(Name = "全角チェック")]
        public string? ZenkakuChk { get; set; } = null;

        [Display(Name = "半角チェック")]
        public string? HankakuChk { get; set; } = null;

        [Display(Name = "数値チェック")]
        public decimal? NumberChk { get; set; } = 0;

        [Display(Name = "日時チェック")]
        public string? DateTimeChk { get; set; } = null;

        [Display(Name = "日時範囲内チェックSTART")]
        public string? GreaterDateChkStart { get; set; } = null;

        [Display(Name = "日時範囲内チェックEND")]
        public string? GreaterDateChkEnd { get; set; } = null;

        [Display(Name = "郵便番号チェック")]
        public string? JPYubinBangoChk { get; set; } = null;

        [Display(Name = "電話番号チェック")]
        public string? PhoneNumberChk { get; set; } = null;
    }
}
