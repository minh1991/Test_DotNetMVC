namespace Test_DotNetMVC.Utils
{
    public class MessageUtil
    {
        public static string MSG1003I(params string[] args) => string.Format("Đăng ký nhé ?", args);
        public static string MSG1004I(params string[] args) => string.Format("Đăng ký Done", args);
        public static string MSG001(params string[] args) => string.Format("Done", args);
        public static string MSG001E(params string[] args) => string.Format("{0} Error", args);
        public static string MSG002E(params string[] args) => string.Format("{0} ~ {1} Error", args);
        public static string MSG1019I(params string[] args) => string.Format("ko có Data", args);
        public static string MSG5039E(params string[] args) => string.Format("chưa chọn dòng nào", args);
        public static string MSG5040E(params string[] args) => string.Format("chọn nhiều dòng 1 lúc", args);
        public static string MSG1024I(params string[] args) => string.Format("Data nay đã DK", args);
        

    }
}
