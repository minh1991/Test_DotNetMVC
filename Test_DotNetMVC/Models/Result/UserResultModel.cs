using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Models.Result
{
    public partial class UserResultModel
    {
        public int UserId { get; set; } = 0;
        public string? UserName { get; set; } = "";
        public string? UserEmail { get; set; } =  "";
        public string? UserPhone { get; set; } =  "";
        public string? UserAddress { get; set; } =  "";
        public string? SchoolNm { get; set; } =  "";
        public string? SexNm { get; set; } =  "";
        public string? SexId { get; set; } = "";
        public DateTime? UpdateDateTime { get; set; }
        public string? DeleteFlg { get; set; } = "";
        public int? GiaTri { get; set; } = 0;
        public int? SoLuong { get; set; } = 0;
    }
}
