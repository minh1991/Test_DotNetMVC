using Microsoft.AspNetCore.Mvc;
using Test_DotNetMVC.Model.Entities;
using Test_DotNetMVC.Models.ResponseModel;
using Test_DotNetMVC.Utils;

namespace Test_DotNetMVC.Controllers
{
    public class MTMR003Controller : Controller
    {
        private readonly T202312Context _db;
        public MTMR003Controller(Model.Entities.T202312Context db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
             MTMR003Model model = SetInitialValues();
            return View(model);
            //return View();
        }
        private MTMR003Model SetInitialValues(MTMR003Model? prmModel = null)
        {
            var tmpProgramID = HttpContext.Session.GetString("ProgramID") ?? null;
            var tmpMitsumoriNo = HttpContext.Session.GetString("MTMRNo") ?? null;
            MTMR003Model rtnModel = prmModel == null ? new MTMR003Model() : prmModel;
            if (tmpProgramID == "002" && tmpMitsumoriNo != null)
            {
                var MTMRInfo = getMTMR(tmpMitsumoriNo);
                rtnModel._MTMRInfo = MTMRInfo;
                if (rtnModel._MTMRInfo != null)
                {
                    var MTMRComment = getMTMRComment(tmpMitsumoriNo);
                    rtnModel._MTMRComment = MTMRComment.ToList();
                }
            }
            return rtnModel;
        }
        private MTMRInfo getMTMR(string _mtmrNo)
        {
            var query = from mtmr in _db.TMtmr001s
                        where mtmr.Mtmr001No == _mtmrNo
                        select new MTMRInfo
                        {
                            Mtmr001No = mtmr.Mtmr001No,
                            CustomerCd = mtmr.CustomerCd,
                            UserId = mtmr.UserId,
                            CustomerAd = mtmr.CustomerAd,
                            CustomerContact = mtmr.CustomerContact,
                            Subject = mtmr.Subject,
                            Deadline = mtmr.Deadline,
                            DeliveryLocation = mtmr.DeliveryLocation,
                            Payment = mtmr.Payment,
                            QuoteExpirationDate = mtmr.QuoteExpirationDate,
                            EstimatedTotalAmount = mtmr.EstimatedTotalAmount,
                            EstimatedSubtotalAmount = mtmr.EstimatedSubtotalAmount,
                            Discount = mtmr.Discount,
                            Remarks = mtmr.Remarks,
                            DelFlg = mtmr.DelFlg,
                        };
            var result = query.SingleOrDefault();
            return result!;
        }

        private IEnumerable<MtMRComment> getMTMRComment(string _mtmrNo)
        {
            var query = from mtmr in _db.TMtmr001s
                        join mtmrComment in _db.TMtmr003s
                        on mtmr.Mtmr001No equals mtmrComment.Mtmr003No into mtmrCommentGroup
                        from mtmrComment in mtmrCommentGroup.DefaultIfEmpty()
                        where mtmr.Mtmr001No == _mtmrNo
                        select new MtMRComment
                        {
                            LineNo = mtmrComment.LineNo,
                            Content = mtmrComment.Content,
                        };
            var result = query.ToList();
            return result!;
        }

        public IActionResult BeforeRegistCheck(MTMR003RequestModel request)
        {
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG1003I(),
                Type = JsonResultModel.MessageTypeEnum.question.ToString(),
            });
        }
        public async Task<IActionResult> Regist(MTMR003RequestModel request)
        {
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG1004I(),
                Type = JsonResultModel.MessageTypeEnum.success.ToString(),
            });
        }

    }
}
