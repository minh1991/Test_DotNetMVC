﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class TPost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public string? Image { get; set; }
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
