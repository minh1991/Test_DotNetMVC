using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class VMtmr002List
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
        public string? SetFlg { get; set; }
        public string? DelFlg { get; set; }
    }
}
