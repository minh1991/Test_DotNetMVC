namespace Test_DotNetMVC.Models.ResponseModel
{
    public class algorithm01Model
    {
        public List<Algorithm01_DB> _DB { get; set; } = new List<Algorithm01_DB>();
        public List<Algorithm01_TXT> _TXT { get; set; } = new List<Algorithm01_TXT>();
        public List<Algorithm01_EXCEL> _EXCEL { get; set; } = new List<Algorithm01_EXCEL>();
    }

    public class Algorithm01_DB
    {
        public string? Item_1 { get; set; } = string.Empty;
        public string? Item_2 { get; set; } = string.Empty;
        public int? Item_3 { get; set; } = 0;
        public decimal? Item_4 { get; set; } = 0;
        public decimal? Item_5 { get; set; } = 0;
        public decimal? Item_6 { get; set; } = 0;
        public decimal? Item_7 { get; set; } = 0;
    }
    public class Algorithm01_TXT
    {
        public string? Item_1 { get; set; } = string.Empty;
        public string? Item_2 { get; set; } = string.Empty;
        public decimal? Item_4 { get; set; } = 0;
        public decimal? Item_6 { get; set; } = 0;
    }
    public class Algorithm01_EXCEL
    {
        public string? Item_1 { get; set; } = string.Empty;
        public string? Item_2 { get; set; } = string.Empty;
        public string? Item_3 { get; set; } = string.Empty;
        public int? Honsu { get; set; } = 0;
        public string? Item_4 { get; set; } = string.Empty;
        public string? Item_5 { get; set; } = string.Empty;
        public string? Item_6 { get; set; } = string.Empty;
        public string? Item_7 { get; set; } = string.Empty;
    }
}
