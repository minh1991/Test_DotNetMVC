namespace Test_DotNetMVC.Models.ResponseModel
{
    public class JsonResultModel
    {
        public enum MessageTypeEnum
        {
            info,
            question,
            success,
            warning,
            error,
        }
        public bool Result { get; set; } = true;
        public bool SystemError { get; set; } = false;
        public string exceptionMessage { get; set; } = string.Empty;
        public string stackTrace { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Json { get; set; } = string.Empty;
    }
}
