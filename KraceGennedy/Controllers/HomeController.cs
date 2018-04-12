using KraceGennedy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KraceGennedy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(KraceGennedy.Models.Login login)
        {
          
            using(DatabaseCon db = new DatabaseCon())
            {
                var crypt = new System.Security.Cryptography.SHA256Managed();
                var hash = new System.Text.StringBuilder();
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(login.password));
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }

                if ((bool)db.UserLogin(login.username, hash.ToString()).FirstOrDefault())
                {
                    Session["user"] = "admin";
                    return RedirectToAction("Index","Dashboard");
                }
                else
                    return RedirectToAction("Index");

            }



        }

    }
}