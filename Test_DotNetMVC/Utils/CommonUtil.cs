using System.Text.Json;

namespace Test_DotNetMVC.Utils
{
    public class CommonUtil
    {
        public static string forFrontJson<T>(List<T> list)
        {
            return JsonSerializer.Serialize(
                list,
                new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
        }
    }
}
