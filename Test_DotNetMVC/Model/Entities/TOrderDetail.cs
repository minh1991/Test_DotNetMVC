﻿using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class TOrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public int? Quanrity { get; set; }
        public DateTime? InsDate { get; set; }
        public string? InsUser { get; set; }
        public string? InsCd { get; set; }
        public DateTime? UpdDate { get; set; }
        public string? UpdUser { get; set; }
        public string? UpdCd { get; set; }
        public string? DelFlg { get; set; }
    }
}
