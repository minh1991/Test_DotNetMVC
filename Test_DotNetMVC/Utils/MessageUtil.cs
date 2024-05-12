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
        public static string MSGTest01(params string[] args) => string.Format("cố định {0} ký tự", args);
        public static string MSGTest02(params string[] args) => string.Format("Không phải số", args);
        public static string MSGTest03(params string[] args) => string.Format("Có số 0 đầu tiên", args);
        public static string MSGTest04(params string[] args) => string.Format("Không được âm", args);
        public static string MSGTest05(params string[] args) => string.Format("Tối đa {0} ký tự", args);
        public static string MSGTest06(params string[] args) => string.Format("Chỉ được nhập từ {0}～{1}", args);
    }
}
