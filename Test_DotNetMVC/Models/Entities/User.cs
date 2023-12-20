using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Models.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? DeleteFlg { get; set; }
        public string? Sex { get; set; }
        public string? School { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public int? GiaTri { get; set; }
        public int? SoLuong { get; set; }
    }
}
