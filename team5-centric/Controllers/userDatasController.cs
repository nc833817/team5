using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using team5_centric.DAL;
using team5_centric.Models;

namespace team5_centric.Controllers
{
    public class userDatasController : Controller
    {
        private centricContext db = new centricContext();

        // GET: userDatas
        [AllowAnonymous]
        public ActionResult Index(string searchString)
        {
            var userSearch = from o in db.userDatas select o;
            string[] userNames;
	        if (!String.IsNullOrEmpty(searchString))
                {
		            userNames = searchString.Split(' ');
		            if (userNames.Count() == 1)
			            {
				            userSearch = userSearch.Where(c => c.lastName.Contains(searchString) ||
				            c.firstName.Contains(searchString)).OrderBy(c => c.lastName);
			            }
		            else
			            {
				            string s1 = userNames [0];
				            string s2 = userNames [1];
				            userSearch = userSearch.Where(c => c.lastName.Contains(s2) &&
				            c.firstName.Contains(s1)).OrderBy(c => c.lastName);
			            }	
		            return View(userSearch.ToList());
                }
            return View(db.userDatas.ToList());
        }
        

        // GET: userDatas/Details/5
        [AllowAnonymous]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData userData = db.userDatas.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // GET: userDatas/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: userDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,firstName,lastName,email,officeLocation,position,startDate,avatar")] userData userData)
        {
            if (ModelState.IsValid)
            {
                userData.userId = Guid.NewGuid();
                db.userDatas.Add(userData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userData);
        }

        // GET: userDatas/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData userData = db.userDatas.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // POST: userDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,firstName,lastName,email,officeLocation,position,startDate,avatar")] userData userData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userData);
        }

        // GET: userDatas/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData userData = db.userDatas.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // POST: userDatas/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            userData userData = db.userDatas.Find(id);
            db.userDatas.Remove(userData);
            db.SaveChanges();
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

     
         
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
            {
                var path = " ";
            if(file!=null)
                {
                   if(file.ContentLength>0)
                    {
                        //for checking uploaded file is image or not
                        if(Path.GetExtension(file.FileName).ToLower()==".jpg"
                            || Path.GetExtension(file.FileName).ToLower() == ".png"
                            || Path.GetExtension(file.FileName).ToLower() == ".gif"
                            || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                            {
                            path=Path.Combine(Server.MapPath("~/Content/img"),file.FileName);
                            file.SaveAs(path);
                            ViewBag.UploadSuccess = true;
                            }
                    }
            }
                return View();
            }
    }
}
