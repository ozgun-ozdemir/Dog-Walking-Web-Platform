using PetWalker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PetWalker.Controllers
{
    public class WalkerController : Controller
    {
        PetWalkerDBEntities db = new PetWalkerDBEntities();
        // GET: Walker
        public ActionResult Search()
        {
            using (var datamodel = new PetWalkerDBEntities())
            {
                var j = datamodel.Walkers.ToList();
                return View(j);
            }
        }
        public ActionResult WalkerProfile()
        {
            return View();
        }
        public ActionResult WalkDetail()
        {
            return View();
        }
        [HttpPost]
        public bool AddWalk(string InternalPetID, string WalkDate, string Length, string Price, string WalkerID)
        {
            Walk NewWalk = new Walk();
            if (NewWalk != null)
            {
                NewWalk.Date = WalkDate;
                NewWalk.Length = Length;
                NewWalk.Price = Price;
                NewWalk.WalkerID = Convert.ToInt16(WalkerID);
                NewWalk.CustomerID = Convert.ToInt16(Session["ID"]);
                NewWalk.PetID = Convert.ToInt16(InternalPetID);
                db.Walks.Add(NewWalk);
                db.SaveChanges();
                System.Threading.Thread.Sleep(1000);
                return true;

            }
            return false;
        }
        [HttpPost]
        public JsonResult GetWalk()
        {
            int ID = Convert.ToInt16(Session["ID"]);
            var ListOfWalks = new List<string>();
            var WalkData = db.Walks.Where(b => b.CustomerID == (ID)).ToList();
            foreach (var b in WalkData) {
                ListOfWalks.Add(b.Customer.FName + b.Customer.LName.ToString());
                ListOfWalks.Add(b.Pet.Name.ToString());
                ListOfWalks.Add(b.Walker.FName + b.Walker.LName.ToString());
                ListOfWalks.Add(b.Length.ToString());
                ListOfWalks.Add(b.Price.ToString());
                ListOfWalks.Add(b.Date.ToString());
            }
            return Json(ListOfWalks);
        }
        [HttpPost]
        public JsonResult GetPetNames()
        {
            int ID = Convert.ToInt16(Session["ID"]);
            var PetNames = new List<string>();
            var PetData = db.Pets.Where(b => b.OwnerID == (ID)).ToList();
            foreach (var b in PetData)
            {
                PetNames.Add(b.PetID.ToString());
                PetNames.Add(b.Name.ToString());
            }
            return Json(PetNames);
        }
    }
}