using Microsoft.AspNetCore.Mvc;

namespace Test_DotNetMVC.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index() { 
            var articles = new List<Article>
            {
                new Article {Id = 1,Title = "Test",Content = "TestContent",Author = "TestAuthor"},
                new Article {Id = 2,Title = "das",Content = "rwe",Author = "54ys"},
                new Article {Id = 3,Title = "qew",Content = "r432",Author = "eq"},
                new Article {Id = 4,Title = "f43f",Content = "dr23r",Author = "t2sdfs"},
            };
            return View(articles); 
        }

        #region Test URL
        //public String StringOut(int id, string firstName, string lastName)
        public String StringOut(int id, Employee employee)
        {
            return ($"Say Hello ghg - MyID: {id} - firstName: {employee.Firstname} - lastName: {employee.LastName}");
        }

        public IActionResult StringOut2(int id, Employee employee)
        {
            return Content($"Say Hello ghg - MyID: {id} - firstName: {employee.Firstname} - lastName: {employee.LastName}");
        }
        public IActionResult JsonOut(int id, Employee employee)
        {
            var obj = new {Id = id, Employee = employee};
            return Json(obj);
        }
        #endregion
        public class Employee
        {
            public String? Firstname { get; set; }
            public String? LastName { get; set; }
        }

        public class Article
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Content { get; set; }
            public string? Author { get; set;}
        }
    }
}
