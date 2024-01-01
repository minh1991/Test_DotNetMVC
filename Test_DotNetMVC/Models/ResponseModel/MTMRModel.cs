
namespace Test_DotNetMVC.Models.ResponseModel
{
    public class MTMRModel
    {
        public MTMRInfo _MTMRInfo { get; set; } = new MTMRInfo();
        public List<MTMRDetail> _MTMRDetailList { get; set; } = new List<MTMRDetail>();
        public MTMRDetail _MTMRDetail { get; set; } = new MTMRDetail();
        public OrthersItems _OrthersItems { get; set; } = new OrthersItems();
        public MTMRRequestModel _RequestModel { get; set; } = new MTMRRequestModel();
    }

    public class MTMRInfo
    {
        public string? Mtmr001No { get; set; } = string.Empty;
        public string? CustomerCd { get; set; } = string.Empty;
        public DateTime? EstimatedDate { get; set; }
        public string? UserId { get; set; } = string.Empty;
        // -------------------------------
        public string? CustomerAd { get; set; } = string.Empty;
        public string? CustomerContact { get; set; } = string.Empty;
        public string? Subject { get; set; } = string.Empty;
        public string? Deadline { get; set; } = string.Empty;
        public string? DeliveryLocation { get; set; } = string.Empty;
        public string? Payment { get; set; } = string.Empty;
        public string? QuoteExpirationDate { get; set; } = string.Empty;
        // -------------------------------
        public int? EstimatedTotalAmount { get; set; } = 0;
        public int? EstimatedSubtotalAmount { get; set; } = 0;
        public int? Discount { get; set; } = 0;
        // -------------------------------
        public string? Remarks { get; set; } = string.Empty;
        public string? DelFlg { get; set; } = string.Empty;
    }

    public class MTMRDetail
    {
        public int Mtmr002Index { get; set; } = 0;
        public string? OrderTxt { get; set; } = string.Empty;
        public string? DrawingNo { get; set; } = string.Empty;
        public string? ProductName { get; set; } = string.Empty;
        public int? Quantity { get; set; } = 0;
        public int? UnitPrice { get; set; } = 0;
        public int? EstimatedAmount { get; set; }
        public string? DrawingFileNo { get; set; } = string.Empty;
        public string? DrawingFileName { get; set; } = string.Empty;
        public string? Remarks { get; set; } = string.Empty;
        public string? SetFlg { get; set; } = string.Empty;
    }

    public class OrthersItems
    {
        public char? PageModeFlg { get; set; }
    }

    public class MTMRRequestModel
    {
        public string? Mtmr001No { get; set; } = string.Empty;
        public string? CustomerCd { get; set; } = string.Empty;
        public string? UserId { get; set; } = string.Empty;
        // -------------------------------
        public string? CustomerAd { get; set; } = string.Empty;
        public string? CustomerContact { get; set; } = string.Empty;
        public string? Subject { get; set; } = string.Empty;
        public string? Deadline { get; set; } = string.Empty;
        public string? DeliveryLocation { get; set; } = string.Empty;
        public string? Payment { get; set; } = string.Empty;
        public string? QuoteExpirationDate { get; set; } = string.Empty;
        // -------------------------------
        public int? EstimatedTotalAmount { get; set; } = 0;
        public int? EstimatedSubtotalAmount { get; set; } = 0;
        public int? Discount { get; set; } = 0;
        // -------------------------------
        public string? Remarks { get; set; } = string.Empty;
        public char? PageModeFlg { get; set; }

        public List<MTMR_record_list?> record_list { get; set; } = new List<MTMR_record_list?>();
    }
    public class MTMR_record_list
    {
        public string? OrderTxt { get; set; } = string.Empty;
        public string? DrawingNo { get; set; } = string.Empty;
        public string? ProductName { get; set; } = string.Empty;
        public int? Quantity { get; set; } = 0;
        public int? UnitPrice { get; set; } = 0;
        public int? EstimatedAmount { get; set; }
        public string? DrawingFileNo { get; set; } = string.Empty;
        public string? DrawingFileName { get; set; } = string.Empty;
        public string? Remarks { get; set; } = string.Empty;
        public string? SetFlg { get; set; } = string.Empty;
        public string? Mtmr002Index { get; set; } = string.Empty;
    }

}
