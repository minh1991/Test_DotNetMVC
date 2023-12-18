using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            var item = HttpContext.Session.Get("sex");
            string result = System.Text.Encoding.UTF8.GetString(item);

            var displayDataList = GetUsers(result);
              return _context.Users != null ? 
                          View(displayDataList) :
                          Problem("Entity set 'T202312Context.Users'  is null.");
        }
        public IEnumerable<UserResultModel> GetUsers(string sexValue)
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
                        // && sex.SexId == sexValue
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,Address")] User user)
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

    }
}
