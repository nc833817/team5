﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public ActionResult Create()
        {
            ViewBag.recognizerId = new SelectList(db.userDatas, "userId", "firstName");
            ViewBag.userId = new SelectList(db.userDatas, "userId", "fullName");
            ViewBag.valueId = new SelectList(db.values, "valueId", "valueName");
            return View();
        }

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
                return RedirectToAction("Index");
            }

            ViewBag.recognizerId = new SelectList(db.userDatas, "userId", "firstName", recognition.recognizerId);
            ViewBag.valueId = new SelectList(db.values, "valueId", "valueName", recognition.valueId);
            return View(recognition);
        }

        // GET: recognitions/Edit/5
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
            ViewBag.valueId = new SelectList(db.values, "valueId", "valueName", recognition.valueId);
            return View(recognition);
        }

        // GET: recognitions/Delete/5
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
