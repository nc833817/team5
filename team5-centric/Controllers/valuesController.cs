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
    public class valuesController : Controller
    {
        private centricContext db = new centricContext();

        // GET: values
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.values.ToList());
        }

        // GET: values/Details/5
        [Authorize]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            values values = db.values.Find(id);
            if (values == null)
            {
                return HttpNotFound();
            }
            return View(values);
        }

        // GET: values/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: values/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "valueId,valueName,valueDescription,valueIcon,valueWeight")] values values)
        {
            if (ModelState.IsValid)
            {
                values.valueId = Guid.NewGuid();
                db.values.Add(values);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(values);
        }

        // GET: values/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            values values = db.values.Find(id);
            if (values == null)
            {
                return HttpNotFound();
            }
            return View(values);
        }

        // POST: values/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "valueId,valueName,valueDescription,valueIcon,valueWeight")] values values)
        {
            if (ModelState.IsValid)
            {
                db.Entry(values).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(values);
        }

        // GET: values/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            values values = db.values.Find(id);
            if (values == null)
            {
                return HttpNotFound();
            }
            return View(values);
        }

        // POST: values/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            values values = db.values.Find(id);
            db.values.Remove(values);
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
