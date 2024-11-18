using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using PetWalker.Models;
using System.Web.Security;
using System.IO;

namespace PetWalker.Controllers
{
    public class UserController : Controller
    {
        PetWalkerDBEntities db = new PetWalkerDBEntities();

        // GET: User
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult SignUP()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
            return View();
        }
        public ActionResult AddPet()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            return View();
        }
        //User creation
        [HttpPost]
        public bool userCreater(string FName, string LName, string EMail, string Phone, string Password, string DOB, string Address, string ZipPostal)
        {
            Customer newCustomer = new Customer();
            if (newCustomer != null)
            {
                newCustomer.FName = FName;
                newCustomer.LName = LName;
                newCustomer.EMail = EMail;
                newCustomer.Phone = Phone;
                newCustomer.Password = Password;
                newCustomer.DOB = DOB;
                newCustomer.Address = Address;
                newCustomer.ZipPostal = ZipPostal;
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                System.Threading.Thread.Sleep(1000);
                return true;

            }


            return false;
        }
        [HttpPost]
        public bool LoginCheck(string EMail, string Password)
        {
            Customer Customer = db.Customers.Where(b => b.EMail.Equals(EMail) && b.Password.Equals(Password)).FirstOrDefault();
            if (Customer != null)
            {
                Session["FName"] = Customer.FName;
                Session["LName"] = Customer.LName;
                Session["EMail"] = Customer.Address;
                Session["Phone"] = Customer.Address;
                Session["Password"] = Customer.Password;
                Session["DOB"] = Customer.DOB;
                Session["Address"] = Customer.Address;
                Session["ZipPostal"] = Customer.ZipPostal;
                Session["ID"] = Customer.CustomerID;
                Session["IsLoggedIn"] = "yes";
                System.Threading.Thread.Sleep(1000);
                return true;
            }
            else { return false; }

        }
        [HttpPost]
        public bool PetCreater(string PetName, string PetWeight, string PetAge, string PetBraid, string PetColor)
        {
            Pet newPet = new Pet();
            if (newPet != null)
            {
                newPet.Name = PetName;
                newPet.Weight = PetWeight;
                newPet.Age = PetAge;
                newPet.Braid = PetBraid;
                newPet.Color = PetColor;
                newPet.OwnerID = Convert.ToInt16(Session["ID"]);
                db.Pets.Add(newPet);
                db.SaveChanges();
                System.Threading.Thread.Sleep(1000);
                return true;

            }


            return false;
        }
        //[HttpPost]
        //public bool PetGet()
        //{
        //    int ID =Convert.ToInt16(Session["ID"]);
        //    Pet GetPet = db.Pets.Where(b => b.OwnerID==(ID)).FirstOrDefault();
        //    int Count = db.Pets.Where(b => b.OwnerID == (ID)).Count();
        //    if (GetPet != null)
        //    {
        //        Session["PetName"] = GetPet.Name;
        //        Session["PetWeight"] = GetPet.Weight;
        //        Session["PetAge"] = GetPet.Age;
        //        Session["PetBraid"] = GetPet.Braid;
        //        Session["PetColor"] = GetPet.Color;
        //        Session["PetID"] = GetPet.PetID;
        //        return true;

        //    }


        //    return false;
        //} OLD PET GET
        [HttpPost]
        public JsonResult GetPet()
        {
            int ID = Convert.ToInt16(Session["ID"]);
            var ListOfPets = new List<string>();
            var PetData = db.Pets.Where(b => b.OwnerID == (ID)).ToList();
            foreach (var b in PetData)
            {
                ListOfPets.Add(b.Name.ToString());
                ListOfPets.Add(b.Weight.ToString());
                ListOfPets.Add(b.Age.ToString());
                ListOfPets.Add(b.Braid.ToString());
                ListOfPets.Add(b.Color.ToString());
            }
            return Json(ListOfPets);
        }
    }
}