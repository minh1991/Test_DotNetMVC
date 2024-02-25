using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Text.Json;
using System.Web.WebPages;
using Test_DotNetMVC.Extensions;
using Test_DotNetMVC.Model.Entities;
using Test_DotNetMVC.Models.Entities;
using Test_DotNetMVC.Models.ResponseModel;
using Test_DotNetMVC.Utils;
using static Test_DotNetMVC.Models.Entities.MTMR002SearchModel;

namespace Test_DotNetMVC.Controllers
{
    public class MTMR002SearchController : Controller
    {
        //private readonly T202312Context _db;
        private T202312Context _db { get; set; }
        private IConfiguration _configuration { get; set; }
        public MTMR002SearchController(T202312Context db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            MTMR002SearchModel model = new MTMR002SearchModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Search(MTMR002SearchModel.SearchItem model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return PartialView("_Search", SetInitialValues().Search);
            //}

            List<VMtmr002List> list = _db.VMtmr002Lists.ToList();
            // làm tạm vì có thể sử dụng WhereIf nhưng phải cài thêm
            if (model.ProductName != null)
            {
                list = list.Where(u => u.ProductName == model.ProductName).ToList();
            }
            if (model.OrderTxt != null)
            {
                list = list.Where(u => u.OrderTxt == model.OrderTxt).ToList();
            }
            list = list
                .OrderBy(u => u.Mtmr002No)
                .ThenBy(u => u.Mtmr002Index)
                .ToList();

            return Json(list.SearchCount());
        }

        [HttpPost]
        public IActionResult GetMtmr002Data(MTMR002SearchModel.MTMR002keys mTMR002key)
        {

            TMtmr002 mtmr002 = _db.TMtmr002s
                .Where(u => u.Mtmr002No == mTMR002key.Mtmr002No)
                .Where(u => u.Mtmr002Index == mTMR002key.Mtmr002Index)
                .Single();

            MTMR002SearchModel.InputItem model = new MTMR002SearchModel.InputItem()
            {
                Mtmr002No = mtmr002.Mtmr002No,
                Mtmr002Index = mtmr002.Mtmr002Index,
                OrderTxt = mtmr002.OrderTxt,
                DrawingNo = mtmr002.DrawingNo,
                ProductName = mtmr002.ProductName,
                Quantity = mtmr002.Quantity,
                UnitPrice = mtmr002.UnitPrice,
                EstimatedAmount = mtmr002.EstimatedAmount,
                DrawingFileNo = mtmr002.DrawingFileNo,
                DrawingFileName = mtmr002.DrawingFileName,
                Remarks = mtmr002.Remarks,
                SetFlg = mtmr002.SetFlg,
                RegistMode = '1'
            };
            return PartialView("_Input", model);
        }

        [HttpPost]
        public IActionResult newMtmr002Data()
        {
            return PartialView("_Input", new MTMR002SearchModel.InputItem()
            {
                RegistMode = '0'
            });
        }

        [HttpPost]
        public IActionResult BeforeRegistCheck(MTMR002SearchModel.InputItem  model)
        {
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG1003I(),
                Type = JsonResultModel.MessageTypeEnum.question.ToString(),
            });
        }

        [HttpPost]
        public IActionResult Regist(MTMR002SearchModel.InputItem model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Input", model);
            }

            TMtmr002? temp = _db.TMtmr002s
                .Where(u => u.Mtmr002No == model.Mtmr002No)
                .Where(u => u.Mtmr002Index == model.Mtmr002Index)
                .SingleOrDefault();
            if (model.RegistMode == '0' && temp != null)
            {
                ModelState.AddModelError(nameof(model.Mtmr002No), MessageUtil.MSG1024I());
                return PartialView("_Input", model);
            }
            //using (IDbContextTransaction trn = _db.Database.BeginTransaction())
            //{
            //    TMtmr002 mtmr002;
            //    if (model.RegistMode == '0')
            //    {
            //        //  INSERT
            //        mtmr002 = new TMtmr002
            //        {
            //            Mtmr002No = model.Mtmr002No,
            //            Mtmr002Index = Convert.ToInt32(model.Mtmr002Index),

            //        };
            //    }
            //    else
            //    {
            //        // UPDATE
            //        mtmr002 = _db.TMtmr002s
            //            .Where(u => u.Mtmr002No == model.Mtmr002No)
            //            .Where(u => u.Mtmr002Index == model.Mtmr002Index)
            //            .Single();
            //    }

            //}
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG1004I(),
                Type = JsonResultModel.MessageTypeEnum.question.ToString(),
                Json = CommonUtil.forFrontJson<VMtmr002List>(_db.VMtmr002Lists.ToList()),
            });
        }

        public IActionResult TestViewPDF()
        {
            return File(System.IO.File.ReadAllBytes($@"Temp\\UploadFiles\\testExport.pdf")
                , "application/pdf"
                , "testExport");
        }

        [HttpPost]
        public IActionResult ExportExcel()
        {
            List<VMtmr002List> list = _db.VMtmr002Lists.ToList();
            list = list
                .Where(u => u.Mtmr002No.Trim() == "1")
                .OrderBy(u => u.Mtmr002No)
                .ThenBy(u => u.Mtmr002Index)
                .ToList();
            string _temporayFolder = _configuration.GetValue<string>("FilePosition:EXPORTFILE");
            string _templateFolder = _configuration.GetValue<string>("OutputFileSettings:TemplateFolder");
            string _templateFile = _configuration.GetValue<string>("OutputFileSettings:TESTEXCEL_TEMP");

            string templateFilePath = Path.Combine(_templateFolder, $@"{_templateFile}.xlsx");
            using (var workbook = new XLWorkbook(templateFilePath))
            {
                var worksheet = workbook.Worksheet("sheetTest");
                // Hearder
                //B5
                int startRowHeader = 5;
                foreach (var item in list)
                {
                    if (startRowHeader > 4 && startRowHeader < 6)
                    {
                        worksheet.Cell(startRowHeader, 2).Value = item.Mtmr002No;
                    }
                }

                // Body Table List
                // A10 B10 C10 D10 E10 F10 G10
                int startRowBody = 10;
                foreach (var item in list)
                {
                    worksheet.Cell(startRowBody, 1).Value = item.Mtmr002Index;
                    worksheet.Cell(startRowBody, 2).Value = item.OrderTxt;
                    worksheet.Cell(startRowBody, 3).Value = item.DrawingNo;
                    worksheet.Cell(startRowBody, 4).Value = item.ProductName;
                    worksheet.Cell(startRowBody, 5).Value = item.Quantity;
                    worksheet.Cell(startRowBody, 6).Value = item.UnitPrice;
                    worksheet.Cell(startRowBody, 7).Value = item.EstimatedAmount;
                    startRowBody++;
                }

                string exportFileName = $"Export_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                string exportFilePath = Path.Combine(_temporayFolder, $@"EXCEL\\{exportFileName}");
                workbook.SaveAs(exportFilePath);
            }

            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG001(),
                Type = JsonResultModel.MessageTypeEnum.question.ToString(),
            });
        }

        [HttpPost]
        public IActionResult ExportCSV()
        {
            List<VMtmr002List> list = _db.VMtmr002Lists.ToList();
            list = list
                .Where(u => u.Mtmr002No.Trim() == "1")
                .OrderBy(u => u.Mtmr002No)
                .ThenBy(u => u.Mtmr002Index)
                .ToList();
            string csv = string.Empty;
            //Header
            string[] headerNames = new string[]
            {
                "Mtmr002No"
                ,"Mtmr002Index"
                ,"OrderTxt"
                ,"DrawingNo"
                ,"ProductName"
                ,"Quantity"
                ,"UnitPrice"
                ,"EstimatedAmount"
                ,"DrawingFileNo"
                ,"DrawingFileName"
            };
            foreach (var item in headerNames)
            {
                csv += item + ',';
            }
            csv += "\r\n";
            // Body
            foreach (var item in list)
            {
                csv += item.Mtmr002No.ToString().Replace(",", ";") + ',';
                csv += item.Mtmr002Index.ToString().Replace(",", ";") + ',';
                csv += item.OrderTxt == null? "" + ',' : item.OrderTxt.ToString().Replace(",", ";") + ',';
                csv += item.DrawingNo == null ? "" + ',' : item.DrawingNo.ToString().Replace(",", ";") + ',';
                csv += item.ProductName == null ? "" + ',' : item.ProductName.ToString().Replace(",", ";") + ',';
                csv += item.Quantity == null ? "" + ',' : item.Quantity.ToString().Replace(",", ";") + ',';
                csv += item.UnitPrice == null ? "" + ',' : item.UnitPrice.ToString().Replace(",", ";") + ',';
                csv += item.EstimatedAmount == null ? "" + ',' : item.EstimatedAmount.ToString().Replace(",", ";") + ',';
                csv += item.DrawingFileNo == null ? "" + ',' : item.DrawingFileNo.ToString().Replace(",", ";") + ',';
                csv += item.DrawingFileName == null ? "" + ',' : item.DrawingFileName.ToString().Replace(",", ";") + ',';
                csv += "\r\n";
            }

            string _temporayFolder = _configuration.GetValue<string>("FilePosition:EXPORTFILE");
            string _temporayCSVFolder = Path.Combine(_temporayFolder, $@"CSV");
            if (!Directory.Exists(_temporayCSVFolder))
            {
                Directory.CreateDirectory(_temporayCSVFolder);
            }
            string exportFileName = $"ExportCSV_{DateTime.Now:yyyyMMddHHmmss}.csv";
            string exportFilePath = Path.Combine(_temporayCSVFolder, $@"{exportFileName}");
            System.IO.File.WriteAllText(exportFilePath, csv.ToString());
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG001(),
                Type = JsonResultModel.MessageTypeEnum.question.ToString(),
            });
        }

        [HttpPost]
        public IActionResult ExportTXT()
        {
            List<VMtmr002List> list = _db.VMtmr002Lists.ToList();
            list = list
                .Where(u => u.Mtmr002No.Trim() == "1")
                .OrderBy(u => u.Mtmr002No)
                .ThenBy(u => u.Mtmr002Index)
                .ToList();
            string txtContents = string.Empty;
            foreach (var item in list)
            {
                txtContents += item.Mtmr002No.ToString() + "    ";
                txtContents += item.Mtmr002Index.ToString()+ "    ";
                txtContents += item.OrderTxt == null ? "" + "    " : item.OrderTxt.ToString() + "    ";
                txtContents += item.DrawingNo == null ? "" + "    " : item.DrawingNo.ToString() + "    ";
                txtContents += item.ProductName == null ? "" + "    " : item.ProductName.ToString() + "    ";
                txtContents += item.Quantity == null ? "" + "    " : item.Quantity.ToString() + "    " ;
                txtContents += item.UnitPrice == null ? "" + "    " : item.UnitPrice.ToString() + "    ";
                txtContents += item.EstimatedAmount == null ? "" + "    " : item.EstimatedAmount.ToString()+ "    ";
                txtContents += item.DrawingFileNo == null ? "" + "    " : item.DrawingFileNo.ToString() + "    ";
                txtContents += item.DrawingFileName == null ? "" + "    " : item.DrawingFileName.ToString() + "    ";
                txtContents += "\r\n";
            }
            string _temporayFolder = _configuration.GetValue<string>("FilePosition:EXPORTFILE");
            string _temporayTXTFolder = Path.Combine(_temporayFolder, $@"TXT");
            if (!Directory.Exists(_temporayTXTFolder))
            {
                Directory.CreateDirectory(_temporayTXTFolder);
            }
            string exportFileName = $"ExportTXT_{DateTime.Now:yyyyMMddHHmmss}.txt";
            string exportFilePath = Path.Combine(_temporayTXTFolder, $@"{exportFileName}");
            System.IO.File.WriteAllText(exportFilePath, txtContents.ToString());
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG001(),
                Type = JsonResultModel.MessageTypeEnum.question.ToString(),
            });
        }
        

    }
}
