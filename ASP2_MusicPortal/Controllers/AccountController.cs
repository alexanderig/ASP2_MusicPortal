using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP2_MusicPortal.Models.Entities;
using ASP2_MusicPortal.Models.ViewModels;
using ASP2_MusicPortal.Models;

namespace ASP2_MusicPortal.Controllers
{
    public class AccountController : Controller
    {
        private IMPRepository repos;
       // private MusicPortalModel db = new MusicPortalModel();

        public AccountController(IMPRepository context)
        {
            repos = context;
        }
        public ActionResult Authorize()
        {
            return View(new ViewLogin { Login = "", Password = ""});
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize(ViewLogin ulogin)
        {
            if(ModelState.IsValid)
            {
                Users user = repos.User.Where(u => u.Login == ulogin.Login).FirstOrDefault();
                if(user != null && SecurityHandlerClass.Compare(ulogin.Password, user.Password, user.Salt))
                {
                    Session["Name"] = user.Name;
                    Session["Surname"] = user.Surname;
                    Session["City"] = user.Cities.CityName;
                    Session["isAdmin"] = user.isAdmin == true ? "Yes":"No";
                    Session["isActive"] = user.isActive == true ? "Active" : "Disabled";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "You wrong somewhere");
                    return View(ulogin);
                }

            }

            return View(ulogin);
        }


        public ActionResult Register()
        {
            ViewBag.ListCities = new SelectList(repos.City, "ID", "CityName");
            return View(new ViewRegister { Name = "", Surname = "", Login = "", Password = "", PasswordDouble = "", City = null, CityID = 2});

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ViewRegister reguser)
        {
            if (ModelState.IsValid)
            {
                Users nuser = new Users();
                nuser.Name = reguser.Name;
                nuser.Surname = reguser.Surname;
                nuser.Login = reguser.Login;
                nuser.Cities = repos.City.Where(c => c.ID == reguser.CityID).FirstOrDefault();
                nuser.isActive = false;
                nuser.isAdmin = false;
                nuser.Salt = SecurityHandlerClass.GetSalt();
                nuser.Password = SecurityHandlerClass.GetHash(reguser.Password, nuser.Salt);
                repos.User.Add(nuser);
                repos.SaveChangesAsync();
                Session["Name"] = nuser.Name;
                Session["Surname"] = nuser.Surname;
                Session["isAdmin"] = nuser.isAdmin == true ? "Yes" : "No";
                Session["isActive"] = nuser.isActive == true ? "Active" : "Disabled";
                Session["City"] = nuser.Cities.CityName;
                return RedirectToAction("Index", "Home");

            }
            ViewBag.ListCities = new SelectList(repos.City, "ID", "CityName", reguser.CityID);
            return View(reguser);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        repos.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


    }
}
