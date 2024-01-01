namespace Test_DotNetMVC.Utils
{
    public class MessageUtil
    {
        public static string MSG1003I(params string[] args) => string.Format("Đăng ký nhé ?", args);
        public static string MSG1004I(params string[] args) => string.Format("Đăng ký Done", args);
    }
}
