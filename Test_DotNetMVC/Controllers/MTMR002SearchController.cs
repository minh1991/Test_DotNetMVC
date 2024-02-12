using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Text.Json;
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
        public MTMR002SearchController(T202312Context db)
        {
            _db = db;
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




    }
}
