using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Test_DotNetMVC.Model.Entities;
using Test_DotNetMVC.Models.Entities;
using Test_DotNetMVC.Models.RequestModel;
using Test_DotNetMVC.Models.Result;

namespace Test_DotNetMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly T202312Context _context;

        public UsersController(T202312Context context)
        {
            _context = context;
        }

        // sesstion code màn hình + biến giới tính
        // biến giới tính -> input disable
        // hiển thị bảng tìm kiếm theo cái input disable

        // trước khi delete check key + update có tồn tại trong bảng hay không?
        // INPUT tên bảng bất kỳ, PK(key value), updatetime mà đã lấy


        // GET: Users
        public async Task<IActionResult> Index(string Addresaaew = "", bool reload = false)
        {
            if (Addresaaew.Length > 0)
            {
                HttpContext.Session.SetString("Address", Addresaaew);
            }

            var item = HttpContext.Session.Get("Address");

            string result = System.Text.Encoding.UTF8.GetString(item);

            var displayDataList = GetUsers(result); ;

            Console.WriteLine("result", result);
            return _context.Users != null ? 
                          View(displayDataList) :
                          Problem("Entity set 'T202312Context.Users'  is null.");
        }
        public IEnumerable<UserResultModel> GetUsers(string addrert)
        {
            var query = from user in _context.Users
                        join school in _context.MSchools
                        on user.School equals school.SchoolId into schoolGroup
                        from school in schoolGroup.DefaultIfEmpty()
                        join sex in _context.MSexes
                        on user.Sex equals sex.SexId into sexGroup
                        from sex in sexGroup.DefaultIfEmpty()
                        where user.DeleteFlg == "1"
                        && school.DeleteFlg == "1"
                        && sex.DeleteFlg == "1"
                         && user.Address == addrert
                        select new UserResultModel
                        {
                            UserId = user.Id,
                            UserName = user.Name,
                            UserEmail =  user.Email,
                            UserPhone = user.Phone,
                            UserAddress = user.Address,
                            SchoolNm = school != null ? school.SchoolName : null,
                            SexNm = sex != null ? sex.SexName : null,
                            SexId = sex != null ? sex.SexId : null,
                            UpdateDateTime = user.UpdateDateTime,
                            DeleteFlg = user.DeleteFlg,
                            GiaTri = user.GiaTri,
                            SoLuong = user.SoLuong,
                        };

            var  result = query.ToList();
            return result;
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,Address")] UserResultModel user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,Address")] Model.Entities.User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromBody] RequestUser requestUser)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'T202312Context.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(requestUser.Id);
            if (user != null)
            {
                user.DeleteFlg = "0";
                _context.Update(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost, ActionName("TestCheck")]
        public async Task<IActionResult> TestCheck([FromBody] RequestUser model)
        {
            string msg;
            // -----Hissu
            /* -- OK---
            var requiredFields = new List<string> { nameof(RequestUser.Id), nameof(RequestUser.UserNameTxt), nameof(RequestUser.UserNameFlg) };
            foreach (var propertyName in requiredFields)
            {
                var propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model, null);
                if (!HissuCheck(propertyValue!))
                {
                    var displayName = GetDisplayName(model, propertyName);
                    msg = $"{displayName} not entered";
                    Console.WriteLine(msg);
                    return RedirectToAction("TestCheck");
                }
            }
            ----*
            // ----- length
            /* -- OK---
            var lengthChecks = new List<(string PropertyName, int MaxLength)>
                {
                    (nameof(RequestUser.Text01), 10),
                    (nameof(RequestUser.Text02), 5),
                    (nameof(RequestUser.Num01), 3),
                    (nameof(RequestUser.Num02), 3)
                };
            foreach (var (propertyName, maxLength) in lengthChecks)
            {
                var propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model, null);
                if (LengthCheck(propertyValue!, maxLength) == false)
                {
                    var displayName = GetDisplayName(model, propertyName);
                    msg = $"{displayName} must not exceed {maxLength} characters";
                    Console.WriteLine(msg);
                    return RedirectToAction("TestCheck");
                }
            }
            ----*/
            // ----- length co dinh
            /* -- OK---
            var lengthValidationChecks = new List<(string PropertyName, int FixedLength)>
                {
                    (nameof(RequestUser.Text01), 10),
                    (nameof(RequestUser.Text02), 5),
                };
            foreach (var (propertyName, FixedLength) in lengthValidationChecks)
            {
                var propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model, null);
                if (LengthFixedCheck(propertyValue!, FixedLength) == false)
                {
                    var displayName = GetDisplayName(model, propertyName);
                    msg = $"{displayName} must not exceed {FixedLength} characters";
                    Console.WriteLine(msg);
                    return RedirectToAction("TestCheck");
                }
            }
            ----*/
            // ---- checkStringDateTime
            /* -- OK---
            var dateValidationChecks = new List<(string PropertyName, string Format)>
                {
                    (nameof(RequestUser.BithdayTxt), "yyyyMMdd"),
                };
            foreach (var (propertyName, format) in dateValidationChecks)
            {
                var propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model, null);
                if (DateTimeStringCheck(propertyValue!, format) == true)
                {
                    var displayName = GetDisplayName(model, propertyName);
                    msg = $"{displayName} is in wrong date format";
                    Console.WriteLine(msg);
                    return RedirectToAction("TestCheck");
                }
            }
            ----*/
            // ---- checkNumber
            // AllowNegative = true là cho phép nhập âm
            // DecimalPlaces số thập phân sau dấu phẩy
            // var numberValidationChecks = new List<(string PropertyName, bool AllowNegative, int DecimalPlaces)>
            //     {
            //         (nameof(RequestUser.Text01), false, 2),
            //         (nameof(RequestUser.BithdayTxt), true, 1),
            //         //(nameof(RequestUser.Num02), false, 1),
            //     };
            // foreach (var (propertyName, allowNegative, decimalPlaces) in numberValidationChecks)
            // {
            //     var propertyValue = model.GetType().GetProperty(propertyName)?.GetValue(model, null);
            //     if (NumberCheck(propertyValue!, allowNegative, decimalPlaces) == false)
            //     {
            //         var displayName = GetDisplayName(model, propertyName);
            //         msg = $"{displayName} is not num";
            //         Console.WriteLine(msg);
            //         return RedirectToAction("TestCheck");
            //     }
            // }

            var dataStart = new 
            {
                data1= "abc",
                data2 = "ioqoiqw",
            };

            return Json(dataStart);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile fileUpload)
        {
            if (fileUpload != null && fileUpload.Length > 0) 
            {
                //D:\\ASP.NET\\20231216\\Test_DotNetMVC\\Test_DotNetMVC\\Uploads
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = Path.GetFileName(fileUpload.FileName);
                string filePath = Path.Combine(uploadPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileUpload.CopyTo(fileStream);
                }
                var returnJson = new
                {
                    fileName = fileName
                };
                return Json(fileName);
            }
            return View();
        }













        private string GetDisplayName(object model, string propertyName)
        {
            var displayAttribute = model.GetType().GetProperty(propertyName)?.GetCustomAttributes(typeof(DisplayAttribute), true)
                                  .FirstOrDefault() as DisplayAttribute;

            return displayAttribute?.Name ?? propertyName;
        }

        private bool HissuCheck(object _propertyValue)
        {
            if (_propertyValue == null || (_propertyValue is string stringValue && string.IsNullOrEmpty(stringValue)))
            {
                return false;
            }
            return true;
        }
        private bool LengthCheck(object _propertyValue, int _maxLength)
        {
            if (_propertyValue is string stringValue && (stringValue?.Length ?? 0) > _maxLength)
            {
                return false;
            }
            return true;
        }

        private bool DateTimeStringCheck(object _propertyValue, string _format)
        {
            if (_propertyValue is string stringValue)
            {
                return !DateTime.TryParseExact(stringValue, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
            }
            return true;
        }

        private bool NumberCheck(object _propertyValue, bool _allowNegative, int _decimalPlaces)
        {
            if (_propertyValue is string stringValue)
            {
                // regexPattern ko ổn
                string regexPattern = _allowNegative
                    ? @"^-?\d+(\.\d{1," + _decimalPlaces + @"})?$"
                    : @"^\d+(\.\d{1," + _decimalPlaces + @"})?$";

                return Regex.IsMatch(stringValue, regexPattern);
            }
            return false; 
        }

        private bool LengthFixedCheck(object _propertyValue, int _FixedLength)
        {
            if (_propertyValue is string stringValue && stringValue.Length != _FixedLength)
            {
                return false;
            }
            return true;
        }

        // 全角チェック
        private bool IsFullWidth(string value)
        {
            string regexPattern = @"^[^ -~｡-ﾟ]+$";

            return Regex.IsMatch(value, regexPattern);
        }
        // 半角チェック
        private bool IsHalfWidth(string value)
        {
            // Biểu thức chính quy để kiểm tra bán kính ký tự
            string regexPattern = @"^[a-zA-Z0-9!-/:-@¥[-`{-~]*$";

            return Regex.IsMatch(value, regexPattern);
        }

    }
}
