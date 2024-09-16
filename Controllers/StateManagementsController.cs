using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class StateManagementsController : Controller
    {
        public IActionResult SetSession(string name,int age)
        {
            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetString("Age", age.ToString());
            return Content("done");
        } 
        public IActionResult GetSession()
        {

           var name= HttpContext.Session.GetString("Name");
            var age = HttpContext.Session.GetString("Age");
            return Content($"the name is {name} and the age is {age}");
        }

        public IActionResult SetCookie(string name)
        {
            Response.Cookies.Append("Name", name);
            return Content("done");
        }
        public IActionResult GetCookie() {

            var name = Request.Cookies["Name"];
            return Content(name);
        }

        public IActionResult SetPersistentCookie(string name) {

            var options = new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddDays(1)

            };
            Response.Cookies.Append("Name", name,options);
            return Content("done");

        }

        public IActionResult SetTempData(string name) {

            TempData["Name"] = name;
            return Content("done");
        }

        public ActionResult GetTempData()
        {
            var name = "";
            if (TempData.ContainsKey("Name"))
            {
                name = TempData["Name"].ToString(); // it will remove the data
            }
            return Content(name);

        }

        public ActionResult GetTempDataByPeek() {
            var name = "";
            if (TempData.ContainsKey("Name"))
            {
                 name= TempData.Peek("Name").ToString(); // it will note remove the data
            }
            return Content(name);

        }

        public ActionResult GetTempDataByKeep()
        {
            var name = "";
            if (TempData.ContainsKey("Name"))
            {
                name = TempData["Name"].ToString(); 
                TempData.Keep(); // it will keep all keys
                TempData.Keep("Name");// it will keep specific key
            }
            return Content(name);

        }
    }
}
