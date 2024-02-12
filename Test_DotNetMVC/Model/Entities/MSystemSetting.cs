using System;
using System.Collections.Generic;

namespace Test_DotNetMVC.Model.Entities
{
    public partial class MSystemSetting
    {
        public string SettingKey { get; set; } = null!;
        public string? SettingValue { get; set; }
        public string? SettingDescription { get; set; }
    }
}
