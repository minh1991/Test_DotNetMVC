using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Models.Result
{
    public partial class UserResultModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserAddress { get; set; }
        public string? SchoolNm { get; set; }
        public string? SexNm { get; set; }
        public string? SexId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
