using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class TMtmr001
    {
        public string Mtmr001No { get; set; } = null!;
        public string? CustomerCd { get; set; }
        public string? UserId { get; set; }
        public int? KrNo { get; set; }
        public DateTime? EstimatedDate { get; set; }
        public string? CustomerAd { get; set; }
        public string? CustomerContact { get; set; }
        public string? Subject { get; set; }
        public string? Deadline { get; set; }
        public string? DeliveryLocation { get; set; }
        public string? Payment { get; set; }
        public string? QuoteExpirationDate { get; set; }
        public int? EstimatedTotalAmount { get; set; }
        public int? EstimatedSubtotalAmount { get; set; }
        public int? Discount { get; set; }
        public string? Remarks { get; set; }
        public DateTime? InsDate { get; set; }
        public string? InsUser { get; set; }
        public string? InsCd { get; set; }
        public DateTime? UpdDate { get; set; }
        public string? UpdUser { get; set; }
        public string? UpdCd { get; set; }
        public string? DelFlg { get; set; }
    }
}
