namespace Test_DotNetMVC.Models.Entities
{
    public class MTMR002SearchModel
    {
        public SearchItem Search { get; set; } = new SearchItem();
        public InputItem Input { get; set; } = new InputItem();
        public MTMR002keys Mtmr002Keys { get; set; } = new MTMR002keys();
        public class SearchItem
        { 
            public string? OrderTxt { get; set; } = null;
            public string? ProductName { get; set; } = null;
        }
        public class InputItem
        {
            public string? Mtmr002No { get; set; } = null;
            public int? Mtmr002Index { get; set; }  = 0;
            public string? OrderTxt { get; set; } = null;
            public string? DrawingNo { get; set; } = null;
            public string? ProductName { get; set; } = null;
            public int? Quantity { get; set; } = 0;
            public int? UnitPrice { get; set; } = 0;
            public int? EstimatedAmount { get; set; } = 0;
            public string? DrawingFileNo { get; set; } = null;
            public string? DrawingFileName { get; set; } = null;
            public string? Remarks { get; set; } = null;
            public string? SetFlg { get; set; } = null;
            public char? RegistMode { get; set; } = null;
        }

        public class MTMR002keys
        {
            public string? Mtmr002No { get; set; } = string.Empty;
            public int? Mtmr002Index { get; set; } = 0;
        }
    }
}
