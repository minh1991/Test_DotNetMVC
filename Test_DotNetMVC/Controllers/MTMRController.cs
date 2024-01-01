using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.Protocol;
using System.Text.Json;
using Test_DotNetMVC.Model.Entities;
using Test_DotNetMVC.Models.ResponseModel;
using Test_DotNetMVC.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Test_DotNetMVC.Controllers
{
    public class MTMRController : Controller
    {
        private readonly T202312Context _db;
        public MTMRController(Model.Entities.T202312Context db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            MTMRModel model = SetInitialValues();
            return View(model);
        }

        private MTMRModel SetInitialValues(MTMRModel? prmModel = null)
        {
            var tmpProgramID = HttpContext.Session.GetString("ProgramID") ?? null;
            var tmpMitsumoriNo = HttpContext.Session.GetString("MTMRNo") ?? null;
            MTMRModel rtnModel = prmModel == null ? new MTMRModel() : prmModel;
            if (string.IsNullOrEmpty(tmpProgramID))
            {
                rtnModel._OrthersItems.PageModeFlg = '0';
                rtnModel._MTMRInfo.Mtmr001No = "";
            }
            else
            {
                if (tmpProgramID == "001" && tmpMitsumoriNo != null)
                {
                    rtnModel._OrthersItems.PageModeFlg = '1';
                    var MTMRInfo = getMTMR(tmpMitsumoriNo);
                    rtnModel._MTMRInfo = MTMRInfo;
                    if (rtnModel._MTMRInfo != null)
                    {
                        var MTMRDetailList = getMTMRDetailList(tmpMitsumoriNo);
                        rtnModel._MTMRDetailList = MTMRDetailList.ToList();
                    }
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
                            CustomerAd= mtmr.CustomerAd,
                            CustomerContact= mtmr.CustomerContact,
                            Subject = mtmr.Subject,
                            Deadline = mtmr.Deadline,
                            DeliveryLocation = mtmr.DeliveryLocation,
                            Payment = mtmr.Payment,
                            QuoteExpirationDate = mtmr.QuoteExpirationDate,
                            EstimatedTotalAmount = mtmr.EstimatedTotalAmount,
                            EstimatedSubtotalAmount = mtmr.EstimatedSubtotalAmount,
                            Discount= mtmr.Discount,
                            Remarks= mtmr.Remarks,
                            DelFlg= mtmr.DelFlg,
                        };
            var result = query.SingleOrDefault();
            return result!;
        }

        private IEnumerable<MTMRDetail> getMTMRDetailList(string _mtmrNo)
        {
            var query = from mtmr in _db.TMtmr001s
                        join mtmrDetail in _db.TMtmr002s
                        on mtmr.Mtmr001No equals mtmrDetail.Mtmr002No into mtmrDetailGroup
                        from mtmrDetail in mtmrDetailGroup.DefaultIfEmpty()
                        where mtmr.Mtmr001No == _mtmrNo
                        select new MTMRDetail
                        {
                            Mtmr002Index = mtmrDetail.Mtmr002Index,
                            OrderTxt= mtmrDetail.OrderTxt,
                            DrawingNo = mtmrDetail.DrawingNo,
                            ProductName= mtmrDetail.ProductName,
                            Quantity= mtmrDetail.Quantity,
                            UnitPrice= mtmrDetail.UnitPrice,
                            EstimatedAmount = mtmrDetail.EstimatedAmount,
                            DrawingFileNo = mtmrDetail.DrawingFileNo,
                            DrawingFileName = mtmrDetail.DrawingFileName,
                            Remarks = mtmrDetail.Remarks,
                            SetFlg = mtmrDetail.SetFlg,
                        };
            var result = query.ToList();
            return result!;
        }

        public IActionResult BeforeRegistCheck(MTMRRequestModel request)
        {
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG1003I(),
                Type = JsonResultModel.MessageTypeEnum.question.ToString(),
            });
        }
        public async Task<IActionResult> Regist(MTMRRequestModel request)
        {
            using (IDbContextTransaction trn = _db.Database.BeginTransaction())
            {
                try 
                {
                    DateTime sysdate = DateTime.Now;
                    if (request.PageModeFlg == '1')
                    {
                        await MTMRUpdateAsync(request, request.PageModeFlg, sysdate);
                        if (request.record_list.Count() > 0)
                        {
                            int maxIndex = 0;
                            foreach (var registerItem in request.record_list)
                            {
                                if (registerItem.Mtmr002Index == null)
                                {
                                    maxIndex = await GetMaxIndex(request, registerItem, maxIndex);
                                    await MTMRDetailInsertAsync(request, registerItem, request.PageModeFlg, sysdate, maxIndex);
                                }
                                else
                                {
                                    await MTMRDetailUpdateAsync(request, registerItem, request.PageModeFlg, sysdate);
                                }
                            }
                        }
                    }
                    //-----------------------------
                    else
                    {
                        await MTMRInsertAsync(request, request.PageModeFlg, sysdate);
                        if (request.record_list.Count() > 0)
                        {
                            int maxIndex = 0;
                            foreach (var registerItem in request.record_list)
                            {
                                maxIndex++;
                                await MTMRDetailInsertAsync(request, registerItem, request.PageModeFlg, sysdate, maxIndex);
                            }
                        }
                    }
                    await _db.SaveChangesAsync();
                    trn.Commit();
                    if (request.PageModeFlg != '1')
                    {
                        HttpContext.Session.SetString("ProgramID", "001");
                        HttpContext.Session.SetString("MTMRNo", request.Mtmr001No);
                        await HttpContext.Session.CommitAsync();
                    }
                    MTMRModel model = SetInitialValues();
                    string jsonModel = JsonSerializer.Serialize(model);
                    return Json(new JsonResultModel
                    {
                        Title = MessageUtil.MSG1004I(),
                        Type = JsonResultModel.MessageTypeEnum.success.ToString(),
                        Json = jsonModel,
                    });
                }
                catch (Exception e)
                {
                    trn.Rollback();
                    throw new Exception(e.Message, e);
                }
            }
            return Json(new JsonResultModel
            {
                Title = MessageUtil.MSG1004I(),
                Type = JsonResultModel.MessageTypeEnum.success.ToString(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> NextPageMTMR003(MTMRRequestModel request)
        {
            HttpContext.Session.SetString("ProgramID", "002");
            HttpContext.Session.SetString("MTMRNo", request.Mtmr001No);
            await HttpContext.Session.CommitAsync();
            return Json(new { redirectTo = Url.Action("Index", "MTMR003") });
        }
        //[HttpPost]
        //public async Task<IActionResult> NextPageMTMR003(MTMRRequestModel request)
        //{
        //    HttpContext.Session.SetString("ProgramID", "002");
        //    HttpContext.Session.SetString("MTMRNo", request.Mtmr001No);
        //    await HttpContext.Session.CommitAsync();
        //    return RedirectToAction("Index", "MTMR003"); // đang bị trả về cả đoạn HTML chứ không hiện lên
        //}

        private async Task MTMRInsertAsync(MTMRRequestModel request, char? mode, DateTime sysdate)
        {
            try 
            {
                TMtmr001 ins;
                ins = new TMtmr001
                {
                    Mtmr001No = request.Mtmr001No,
                    CustomerCd = request.CustomerCd,
                    UserId = request.UserId,
                    CustomerAd = request.CustomerAd,
                    CustomerContact = request.CustomerContact,
                    Subject = request.Subject,
                    Deadline = request.Deadline,
                    DeliveryLocation = request.DeliveryLocation,
                    Payment = request.Payment,
                    QuoteExpirationDate = request.QuoteExpirationDate,
                    EstimatedTotalAmount = request.EstimatedTotalAmount,
                    UpdDate = sysdate,
                    InsDate = sysdate,
                    DelFlg = "0",
                };
                _db.TMtmr001s.Add(ins);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private async Task MTMRUpdateAsync(MTMRRequestModel request, char? mode, DateTime sysdate)
        {
            try
            {
                var upd = await _db.TMtmr001s.Where(e => e.Mtmr001No == request.Mtmr001No).FirstOrDefaultAsync();
                if (upd != null)
                {
                    upd.CustomerCd = request.CustomerCd;
                    upd.UserId = request.UserId;
                    upd.CustomerAd = request.CustomerAd;
                    upd.CustomerContact = request.CustomerContact;
                    upd.Subject = request.Subject;
                    upd.Deadline = request.Deadline;
                    upd.DeliveryLocation = request.DeliveryLocation;
                    upd.Payment= request.Payment;
                    upd.QuoteExpirationDate = request.QuoteExpirationDate;
                    upd.EstimatedTotalAmount = request.EstimatedTotalAmount;
                    upd.UpdDate = sysdate;
                    upd.InsDate = sysdate;
                    upd.DelFlg = "0";
                    _db.TMtmr001s.Update(upd);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            
        }
        private async Task<int> GetMaxIndex(MTMRRequestModel request, MTMR_record_list registerItem, int maxIndex)
        {
            try
            {
                var tmpMaxindex = _db.TMtmr002s
                                   .Where(c => c.Mtmr002No == request.Mtmr001No)
                                   .Select(c => c.Mtmr002No)
                                   .ToList();
                if (tmpMaxindex.Any())
                {
                    maxIndex = tmpMaxindex
                       .Select(c => int.Parse(c.ToString()))
                       .DefaultIfEmpty(0)
                       .Max() + 1;
                }
                else
                {
                    maxIndex = maxIndex + 1;
                }
                return maxIndex;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private async Task MTMRDetailInsertAsync(MTMRRequestModel request, MTMR_record_list registerItem, char? mode, DateTime sysdate, int maxIndex)
        {
            try
            {
                TMtmr002 ins;
                ins = new TMtmr002
                {
                    Mtmr002No = request.Mtmr001No,
                    Mtmr002Index = maxIndex,
                    OrderTxt = registerItem.OrderTxt,
                    DrawingNo = registerItem.DrawingNo,
                    ProductName = registerItem.ProductName,
                    Quantity = registerItem.Quantity,
                    UnitPrice = registerItem.UnitPrice,
                    EstimatedAmount= registerItem.EstimatedAmount,
                    DrawingFileName= registerItem.DrawingFileName,
                    Remarks= registerItem.Remarks,
                    SetFlg = registerItem.SetFlg,
                    UpdDate = sysdate,
                    InsDate = sysdate,
                    DelFlg = "0",
                };
                _db.TMtmr002s.Add(ins);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private async Task MTMRDetailUpdateAsync(MTMRRequestModel request, MTMR_record_list registerItem, char? mode, DateTime sysdate)
        {
            try
            {
                var upd = await _db.TMtmr002s
                                .Where(e => 
                                    e.Mtmr002No == request.Mtmr001No
                                    && e.Mtmr002Index == int.Parse(registerItem.Mtmr002Index)
                                ).FirstOrDefaultAsync();
                if( upd != null )
                {
                    upd.OrderTxt = registerItem.OrderTxt;
                    upd.DrawingNo = registerItem.DrawingNo;
                    upd.ProductName = registerItem.ProductName;
                    upd.Quantity = registerItem.Quantity;
                    upd.UnitPrice = registerItem.UnitPrice;
                    upd.EstimatedAmount = registerItem.EstimatedAmount;
                    upd.DrawingFileName = registerItem.DrawingFileName;
                    upd.Remarks = registerItem.Remarks;
                    upd.SetFlg = registerItem.SetFlg;
                    upd.UpdDate = sysdate;
                    upd.InsDate = sysdate;
                    upd.DelFlg = "0";
                    _db.TMtmr002s.Update(upd);
                }
            }
            catch (Exception e) 
            { 
                throw new Exception(e.Message, e); 
            }
        }

        
    }
}
