using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class MCategory
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Position { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeyWords { get; set; }
        public DateTime? InsDate { get; set; }
        public string? InsUser { get; set; }
        public string? InsCd { get; set; }
        public DateTime? UpdDate { get; set; }
        public string? UpdUser { get; set; }
        public string? UpdCd { get; set; }
        public string? DelFlg { get; set; }
    }
}
