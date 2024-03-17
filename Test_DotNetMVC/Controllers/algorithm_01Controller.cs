using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Test_DotNetMVC.Model.Entities;
using Test_DotNetMVC.Models.ResponseModel;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Controllers
{
    public class algorithm_01Controller : Controller
    {
        private T202312Context _db { get; set; }
        private IConfiguration _configuration { get; set; }
        public algorithm_01Controller(T202312Context db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExportExcel()
        {
            /* ----------Demo Data---------- */
            List<Algorithm01Db> listDB = _db.Algorithm01Dbs.ToList();
            List<Algorithm01Txt> listTXT = _db.Algorithm01Txts.ToList();
            /* --------------------------- */
            List<Algorithm01_EXCEL> _EXCELs = new List<Algorithm01_EXCEL>();

            Dictionary<string, Tuple<decimal, decimal, int>> keyValuesTxt = new Dictionary<string, Tuple<decimal, decimal, int>>();
            foreach (var item in listTXT)
            {
                string key = $"{item.Item1}_{item.Item2}";
                if (keyValuesTxt.ContainsKey(key))
                {
                    keyValuesTxt[key] = new Tuple<decimal, decimal, int>(
                        keyValuesTxt[key].Item1 + item.Item4.GetValueOrDefault(),
                        keyValuesTxt[key].Item2 + item.Item6.GetValueOrDefault(),
                        keyValuesTxt[key].Item3 + 1
                        );
                }
                else
                {
                    keyValuesTxt.Add(key, new Tuple<decimal, decimal, int>(item.Item4.GetValueOrDefault(), item.Item6.GetValueOrDefault(), 1));
                }
            }

            Console.WriteLine(string.Join("\t", keyValuesTxt));

            //string _temporayFolder = _configuration.GetValue<string>("FilePosition:EXPORTFILE");
            //string _templateFolder = _configuration.GetValue<string>("OutputFileSettings:TemplateFolder");
            //string _templateFile = _configuration.GetValue<string>("OutputFileSettings:TESTEXCEL2_TEMP");
            //string templateFilePath = Path.Combine(_templateFolder, $@"{_templateFile}.xlsx");
            //using(var workbook = new XLWorkbook(templateFilePath)){

            //    var worksheet = workbook.Worksheet("sheetTest");
            //    int startRowBody = 2;
            //    foreach (var item in _EXCELs)
            //    {
            //        worksheet.Cell(startRowBody, 1).Value = item.Item_1;
            //        worksheet.Cell(startRowBody, 2).Value = item.Item_2;
            //        worksheet.Cell(startRowBody, 3).Value = item.Item_3;
            //        worksheet.Cell(startRowBody, 4).Value = item.Honsu;
            //        worksheet.Cell(startRowBody, 5).Value = item.Item_4;
            //        worksheet.Cell(startRowBody, 6).Value = item.Item_5;
            //        worksheet.Cell(startRowBody, 7).Value = item.Item_6;
            //        worksheet.Cell(startRowBody, 7).Value = item.Item_7;
            //        startRowBody++;
            //    }

            //    string exportFileName = $"ExportAlgorithm01_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            //    string exportFilePath = Path.Combine(_temporayFolder, $@"EXCEL\\{exportFileName}");
            //    workbook.SaveAs(exportFilePath);

            //}
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG001(),
                Type = JsonResultModel.MessageTypeEnum.question.ToString(),
            });
        }
    }
}
