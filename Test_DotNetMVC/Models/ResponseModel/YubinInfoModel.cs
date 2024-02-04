namespace Test_DotNetMVC.Models.ResponseModel
{
    public class YubinInfoModel
    {
        public string? message { get; set; } = null;
        public int? status { get; set; } = 0;
        public List<Result> results { get; set; } = new List<Result>();
    }

    public class Result
    {
        public string? address1 { get; set; } = null;
        public string? address2 { get; set; } = null;
        public string? address3 { get; set; } = null;
        public string? kana1 { get; set; } = null;
        public string? kana2 { get; set; } = null;
        public string? kana3 { get; set; } = null;
        public string? prefcode { get; set; } = null;
        public string? zipcode { get; set; } = null;
    }
}
