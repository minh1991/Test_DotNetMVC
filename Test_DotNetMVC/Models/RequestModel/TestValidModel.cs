using System.ComponentModel.DataAnnotations;
using Test_DotNetMVC.Attributes;
using Test_DotNetMVC.Utils;

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

    public class TestValidModelByAttribute
    {
        [Display(Name = "必須チェック")]
        [Required]
        public string? HissuChk { get; set; } = null;

        [Display(Name = "固定桁数チェック")]
        [FixedLength(5)]
        public string? FixedLengthChk { get; set; } = null;

        [Display(Name = "範囲数値チェック")]
        [RangeNumber(1,99)]
        public int? RangeNumberChk { get; set; } = 0;

        [Display(Name = "テキスト長さチェック")]
        [MaxLength(3)]
        public string? TxtLenghtChk { get; set; } = null;

        [Display(Name = "全角チェック")]
        [ZenkakuChkByShiftJIS]
        public string? ZenkakuChk { get; set; } = null;

        [Display(Name = "半角チェック")]
        [HankakuChkByShiftJIS]
        public string? HankakuChk { get; set; } = null;

        [Display(Name = "数値チェック")]
        [NumberFormat(4, false)]
        public int? NumberChk { get; set; } = 0;

        [Display(Name = "日時チェック")]
        [DateTimeChk("yyyy/MM/dd")]
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
