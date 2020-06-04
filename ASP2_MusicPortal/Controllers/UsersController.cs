using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP2_MusicPortal.Models.Entities;
using ASP2_MusicPortal.Models.ViewModels;

namespace ASP2_MusicPortal.Controllers
{
    public class UsersController : Controller
    {
        private MusicPortalModel db = new MusicPortalModel();

        public async Task<ActionResult> Index()
        {
            List<Users> dbusers = await db.Users.ToListAsync();
            List<ViewUser> users = new List<ViewUser>();
            foreach(Users us in dbusers)
            {
                ViewUser viewUser = new ViewUser { ID = us.ID, City = us.Cities.CityName, Name = us.Name, Surname = us.Surname, Login = us.Login, isAdmin = us.isAdmin, isActive = us.isActive };
                users.Add(viewUser);
            }

            return View(users);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewUser viewUser = new ViewUser { ID = users.ID, City = users.Cities.CityName, Name = users.Name, Surname = users.Surname, Login = users.Login, isActive = users.isActive, isAdmin = users.isAdmin };

            return View(viewUser);
        }

        
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewUser viewUser = new ViewUser { ID = users.ID, City = users.Cities.CityName, Name = users.Name, Surname = users.Surname, Login = users.Login, isActive = users.isActive, isAdmin = users.isAdmin };
            return View(viewUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Surname,Login,City,isActive,isAdmin")] ViewUser eduser)
        {
            if (ModelState.IsValid)
            {
                Users user = await db.Users.FindAsync(eduser.ID);
                if (user == null)
                {
                    return HttpNotFound();
                }
                user.isActive = eduser.isActive;
                user.isAdmin = eduser.isAdmin;

                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eduser);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewUser viewUser = new ViewUser { ID = users.ID, City = users.Cities.CityName, Name = users.Name, Surname = users.Surname, Login = users.Login, isActive = users.isActive, isAdmin = users.isAdmin };
            return View(viewUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Users users = await db.Users.FindAsync(id);
            db.Users.Remove(users);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
