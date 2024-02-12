using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class TOrder
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? Quantiry { get; set; }
        public DateTime? InsDate { get; set; }
        public string? InsUser { get; set; }
        public string? InsCd { get; set; }
        public DateTime? UpdDate { get; set; }
        public string? UpdUser { get; set; }
        public string? UpdCd { get; set; }
        public string? DelFlg { get; set; }
    }
}
