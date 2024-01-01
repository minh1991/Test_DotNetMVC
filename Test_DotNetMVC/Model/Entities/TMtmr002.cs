using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class TMtmr002
    {
        public string Mtmr002No { get; set; } = null!;
        public int Mtmr002Index { get; set; }
        public string? OrderTxt { get; set; }
        public string? DrawingNo { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public int? UnitPrice { get; set; }
        public int? EstimatedAmount { get; set; }
        public string? DrawingFileNo { get; set; }
        public string? DrawingFileName { get; set; }
        public string? Remarks { get; set; }
        public string? SetFlg { get; set; }
        public DateTime? InsDate { get; set; }
        public string? InsUser { get; set; }
        public string? InsCd { get; set; }
        public DateTime? UpdDate { get; set; }
        public string? UpdUser { get; set; }
        public string? UpdCd { get; set; }
        public string? DelFlg { get; set; }
    }
}
