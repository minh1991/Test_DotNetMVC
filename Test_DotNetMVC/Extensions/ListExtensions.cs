using System.Text.Json;
using Test_DotNetMVC.Models.ResponseModel;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Extensions
{
    public static class ListExtensions
    {
        public static JsonResultModel ListCountCheck<T>(this List<T> list, bool zeroCheck, bool oneCheck)
        {
            JsonResultModel result = new JsonResultModel();

            if (zeroCheck && list.Count() <= 0)
            {
                result.Result = false;
                result.Title = MessageUtil.MSG5039E();
                result.Type = JsonResultModel.MessageTypeEnum.error.ToString();
            }
            if (oneCheck && 1 < list.Count())
            {
                result.Result = false;
                result.Title = MessageUtil.MSG5040E();
                result.Type = JsonResultModel.MessageTypeEnum.error.ToString();
            }

            return result;
        }

        public static JsonResultModel SearchCount<T>(this List<T> list)
        {
            JsonResultModel result = new JsonResultModel();

            if (list.Count() <= 0)
            {
                result.Result = false;
                result.Title = MessageUtil.MSG1019I();
                result.Type = JsonResultModel.MessageTypeEnum.warning.ToString();
            }
            else
            {
                result.Json = JsonSerializer.Serialize(
                    list,
                    new JsonSerializerOptions
                    {
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });
            }

            return result;
        }
    }
}
