using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_DotNetMVC.Models.RequestModel
{
    public class RequestUser
    {
        [Display(Name = "ユーザーID")]
        public int? Id { get; set; } = 0;

        [Display(Name = "ユーザー名")]
        public string? UserNameTxt { get; set; } = null;
        [Display(Name = "ユーザーフラグ")]
        public char? UserNameFlg { get; set; } = null;
        [Display(Name = "生年月日Text")]
        public string? BithdayTxt { get; set; } = null;
        [Display(Name = "テキスト１")]
        public string? Text01 { get; set; } = null;
        [Display(Name = "テキスト２")]
        public string? Text02 { get; set; } = null;
        [Display(Name = "数字１")]
        public int? Num01 { get; set; } = 0;
        [Display(Name = "数字２")]
        public int? Num02 { get; set; } = 0;
    }
}
