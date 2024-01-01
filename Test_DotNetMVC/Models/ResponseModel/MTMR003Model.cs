namespace Test_DotNetMVC.Models.ResponseModel
{
    public class MTMR003Model
    {
        public MTMRInfo _MTMRInfo { get; set; } = new MTMRInfo();
        public List<MtMRComment> _MTMRComment { get; set; } = new List<MtMRComment>();

        public MTMR003RequestModel _RequestModel { get; set; } = new MTMR003RequestModel();
    }
    public class MtMRComment
    {
       // public string? Mtmr003No { get; set; } = string.Empty!;
       // public int? Mtmr003Index { get; set; } = 0;
        public int? LineNo { get; set; } = 0;
        public string? Content { get; set; } = string.Empty;
    }

    public class MTMR003RequestModel 
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
        public List<MtMRComment> _MTMRComment { get; set; } = new List<MtMRComment>();
    }

}
