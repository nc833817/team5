using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
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
        [Authorize]
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
        [Authorize]
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
        //public ActionResult Create([Bind(Include = "userId,firstName,lastName,email,officeLocation,position,startDate,avatar")] userData userData)
        public ActionResult Create([Bind(Include = "userId,firstName,lastName,email,officeLocation,position,startDate,avatar")] userData userData)
        {
            if (ModelState.IsValid)
            {
                Guid memberID;
                Guid.TryParse(User.Identity.GetUserId(), out memberID);
                userData.email = User.Identity.Name;
                userData.userId = memberID;
                db.userDatas.Add(userData);

                HttpPostedFileBase file = Request.Files["UploadedImage"]; //(A) – see notes below
                if (file != null && file.FileName != null && file.FileName != "") //(B)
                {
                    FileInfo fi = new FileInfo(file.FileName); //(C)
                    if (fi.Extension != ".png" && fi.Extension != ".jpg" && fi.Extension != ".gif")  //(D)
                    {
                        TempData["Errormsg"] = "Image File Extension is not valid"; //(E)
                        return View(userData);
                    }
                    else
                    {
                        // this saves the file as the user’s ID and file extension
                        userData.avatar = userData.userId + fi.Extension; //(F)
                        file.SaveAs(Server.MapPath("~/Content/Avatars/" + userData.avatar)); //(G)
                    }
                }
                try
                {
                    db.userDatas.Add(userData);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View("duplicateUser");
                }  
            }
            else
            {
                return View(userData);
            }
        }
        // GET: userDatas/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData user = db.userDatas.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (user.userId == memberID)
            {
                // find the user's record
                var currentUser = db.userDatas.Find(memberID);
                // save the current photo into TempData
                TempData["oldPhoto"] = currentUser.avatar; // save the current photo info
                return View(user);
            }
            else
            {
                return View("notAuthorized");
            }
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
                HttpPostedFileBase file = Request.Files["UploadedImage"];
                if (file != null && file.FileName != null && file.FileName != "")
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    if (fi.Extension != ".png" && fi.Extension != ".jpg" && fi.Extension != "gif")
                {
                        TempData["Errormsg"] = "Image File Extension is not valid";
                        return View(userData);
                    }
            else
                    {
                        // the following statement prevents the File statements from throwing Exceptions if the file isn't found
                        string imageName = "none";
                        // if the old photo isn't null, load it's name into imageName
                        if (TempData["oldPhoto"] != null)
                        {
                            imageName = TempData["oldPhoto"].ToString();
                        }
                        string path = Server.MapPath("~/Content/Avatars/" + imageName);
                        // there may not be a file, so use try/catch
                        try
                        {
                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                            else
                            {
                                // must already be deleted
                            }
                        }
                        catch (Exception)
                        {
                            // delete failed - probably not a real issue
                        }
                        // now upload the new image
                        if (fi.Name != null && fi.Name != "") // i.e., there was a file selected
                        {
                            //update the stored file name, if there is one
                            //the file name is changed to the userID, to avoid name conflicts
                            userData.avatar = userData.userId + fi.Extension;
                            file.SaveAs(Server.MapPath("~/Content/Avatars/" + userData.avatar));
                        }
                    }
                }
                else
                {
                    // no file was selected, so set the photo field back to its original value
                    if (TempData["oldPhoto"] != null)
                    {
                        userData.avatar = TempData["oldPhoto"].ToString();
                    }
                }
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
            userData user = db.userDatas.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Guid memberId;
            Guid.TryParse(User.Identity.GetUserId(), out memberId);
            if (user.userId == memberId)
            {
                return View(user);
            }
            else
            {
                return View("notAuthorized");
            }
            // old code: return View(userData);
        }

        // POST: userDatas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Image userData = db.userDatas.Find(id);
        //    string imageName = userData.avatar; //(A)
        //    string path = Server.MapPath("~/Content/Avatars/" + imageName); //(B)
        //                                                           // there may not be a file, so use try/catch
        //    try
        //    {
        //        if (System.IO.File.Exists(path)) //(C)
        //        {
        //            System.IO.File.Delete(path); //(D)
        //        }
        //        else
        //        {
        //            // must already be deleted (E)
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        // delete failed - probably not a real issue (F)
        //    }
        //    db.userDatas.Remove(image);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
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
