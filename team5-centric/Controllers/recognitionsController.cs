using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using team5_centric.DAL;
using team5_centric.Models;

namespace team5_centric.Controllers
{
    public class recognitionsController : Controller
    {
        private centricContext db = new centricContext();

        // GET: recognitions
        [Authorize]
        public ActionResult Index()
        {
            var recognitions = db.recognitions.Include(r => r.recognizer).Include(r => r.values);
            return View(recognitions.ToList());
        }

        // GET: recognitions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recognition recognition = db.recognitions.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // GET: recognitions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.recognizerId = new SelectList(db.userDatas, "userId", "firstName");
            ViewBag.userId = new SelectList(db.userDatas, "userId", "fullName");
            ViewBag.valueId = new SelectList(db.values, "valueId", "valueName");
            return View();

        }

        //{

        //    string userId = User.Identity.GetUserId();
        //    SelectList user = new SelectList(db.userDetails, "userId", "fullName");
        //    user = new SelectList(user.Where(x => x.Value != userId).ToList(), "Value", "Text");
        //    ViewBag.recId = userData;

        //}

        // POST: recognitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recId,userId,valueId,valueComment,recognizerId")] recognition recognition)
        {
            if (ModelState.IsValid)
            {

                recognition.recId = Guid.NewGuid();
                Guid memberID;
                Guid.TryParse(User.Identity.GetUserId(), out memberID);
                recognition.recognizerId = memberID;
                db.recognitions.Add(recognition);
                db.SaveChanges();

                //var recognizerFirstName = recognition.recognizer.firstName;
                //var recognizerLastName = recognition.recognizer.lastName;
                //var recipientFirstName = recognition.userDatas.firstName;
                //var recipientLastName = recognition.userDatas.lastName;
                //var recipientEmail = recognition.userDatas.email;
                //var valueName = recognition.values.valueName;
                //var valueDescription = recognition.values.valueDescription;
                //var recgonitionComment = recognition.valueComment;

                //var message = "Hello " + recipientFirstName + " " + recipientLastName + ", \n\nCongratulations! ";
                //message += "You have been recognized by " + recognizerFirstName + " " + recognizerLastName + " for exemplifying one of Centric's core values.";
                //message += recognizerFirstName + " recognized you for demonstrating" + valueName + " in the workplace.";
                //message += "\n\nOn behalf of Centric,\nThank you for all your hard work!";

                //MailMessage myMessage = new MailMessage();
                //MailAddress from = new MailAddress("systemcentricvalues@gmail.com", "Centric Values System");
                //myMessage.From = from;
                //myMessage.To.Add(recipientEmail);
                //myMessage.Subject = "Centric Core Values Recognition";
                //myMessage.Body = message;
                //try
                //{
                //    SmtpClient smtp = new SmtpClient();
                //    smtp.Host = "smtp.gmail.com";
                //    smtp.Port = 587;
                //    smtp.UseDefaultCredentials = false;
                //    smtp.Credentials = new System.Net.NetworkCredential("systemcentricvalues", "Values2020!");
                //    smtp.EnableSsl = true;
                //    smtp.Send(myMessage);
                //    TempData["mailError"] = "";
                //}
                //catch (Exception ex)
                //{
                //    // (not sure if needed in our case) -> "this captures an Execption and allows you to display the message in the View"
                //    TempData["mailError"] = ex.Message;
                //    return View("mailError");
                //}
                return RedirectToAction("Index");
            }

            ViewBag.recognizerId = new SelectList(db.userDatas, "userId", "firstName", recognition.recognizerId);
            ViewBag.userId = new SelectList(db.userDatas, "userId", "fullName");
            ViewBag.valueId = new SelectList(db.values, "valueId", "valueName", recognition.valueId);
            return View(recognition);
        }

        // GET: recognitions/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recognition recognition = db.recognitions.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            ViewBag.recognizerId = new SelectList(db.userDatas, "userId", "firstName", recognition.recognizerId);
            ViewBag.userId = new SelectList(db.userDatas, "userId", "fullName");
            ViewBag.valueId = new SelectList(db.values, "valueId", "valueName", recognition.valueId);
            return View(recognition);
        }

        // POST: recognitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recId,userId,valueId,valueComment,recognizerId")] recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recognizerId = new SelectList(db.userDatas, "userId", "firstName", recognition.recognizerId);
            ViewBag.userId = new SelectList(db.userDatas, "userId", "fullName");
            ViewBag.valueId = new SelectList(db.values, "valueId", "valueName", recognition.valueId);
            return View(recognition);
        }

        // GET: recognitions/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recognition recognition = db.recognitions.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // POST: recognitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            recognition recognition = db.recognitions.Find(id);
            db.recognitions.Remove(recognition);
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
    }

}
