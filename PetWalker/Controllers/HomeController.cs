using PetWalker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace PetWalker.Controllers
{
    public class HomeController : Controller
    {
        PetWalkerDBEntities db = new PetWalkerDBEntities();
        // GET: Walker
        public ActionResult Index()
        {
            using (var datamodel = new PetWalkerDBEntities())
            {
                var j = datamodel.Messages.ToList();
                return View(j);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ChatPage()
        {
            using (var datamodel = new PetWalkerDBEntities())
            {
                var j = datamodel.Walkers.ToList();
                return View(j);
            }
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult PWReset(string EMail, string Phone, string IdentityNumber)
        {
            Customer User = db.Customers.Where(b => b.EMail.Equals(EMail) && b.Phone.Equals(Phone) && b.IDNumber.Equals(IdentityNumber)).FirstOrDefault();
            if (User != null)
            {
                var NewPassword = RandomString();
                User.Password = NewPassword;
                db.SaveChanges();
                System.Threading.Thread.Sleep(1000);
                return Json(NewPassword);
            }
            else { return Json(false); }

        }
        private static Random random = new Random();

        public static string RandomString()
        {
            int length = 7;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpPost]
        public bool SendMessage(string message)
        {
            Message MyMessage  = new Message();
            var ID=Convert.ToInt16(Session["ID"]);
            if (MyMessage != null)
            {
                if (ID==0)
                {
                    return false;
                }
                else{ 
                MyMessage.MessageSenderID = Convert.ToInt16(Session["ID"]);
                MyMessage.MessageText = message;
                MyMessage.MessageDate = DateTime.Now;
                MyMessage.MessageSenderNameSurname = (Session["FName"] + " " + Session["LName"]).ToString();
                db.Messages.Add(MyMessage);
                db.SaveChanges();
                System.Threading.Thread.Sleep(1000);
                return true;
                }
            }
            return false;
        }
    }
}