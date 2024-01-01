using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class MSystemSetting
    {
        [Key]
        public string SettingKey { get; set; } = null!;
        public string? SettingValue { get; set; }
        public string? SettingDescription { get; set; }
    }
}
