using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CatchTheFish.DbEntities;

namespace TheFishNet.Controllers
{
    [Authorize]
    public class FdaCalendarsController : Controller
    {
        private TheFishEntities db = new TheFishEntities();

        // GET: FdaCalendars
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                ViewBag.Title = "Fda Canlendars All";
                return View(db.FdaCalendars.ToList());
            }
            else
            {
                ViewBag.Title = "My Fda Canlendars";
                var nowDate = DateTime.Now.AddDays(-1);
                return View(db.FdaCalendars.Where(x => x.Price < 10
                    && (x.Type.Equals("PDUFA", StringComparison.OrdinalIgnoreCase) || x.Type.Equals("Advisory Committee", StringComparison.OrdinalIgnoreCase))
                    && x.CatalystDate > nowDate).ToList());
            }
        }

        // GET: FdaCalendars/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FdaCalendar fdaCalendar = db.FdaCalendars.Find(id);
            if (fdaCalendar == null)
            {
                return HttpNotFound();
            }
            return View(fdaCalendar);
        }

        // GET: FdaCalendars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FdaCalendars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Symbol,Type,CatalystDate,CatelystNotes,LastMoidfiedDate,Price,MarketCapital")] FdaCalendar fdaCalendar)
        {
            if (ModelState.IsValid)
            {
                db.FdaCalendars.Add(fdaCalendar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fdaCalendar);
        }

        // GET: FdaCalendars/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FdaCalendar fdaCalendar = db.FdaCalendars.Find(id);
            if (fdaCalendar == null)
            {
                return HttpNotFound();
            }
            return View(fdaCalendar);
        }

        // POST: FdaCalendars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Symbol,Type,CatalystDate,CatelystNotes,LastMoidfiedDate,Price,MarketCapital")] FdaCalendar fdaCalendar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fdaCalendar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fdaCalendar);
        }

        // GET: FdaCalendars/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FdaCalendar fdaCalendar = db.FdaCalendars.Find(id);
            if (fdaCalendar == null)
            {
                return HttpNotFound();
            }
            return View(fdaCalendar);
        }

        // POST: FdaCalendars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FdaCalendar fdaCalendar = db.FdaCalendars.Find(id);
            db.FdaCalendars.Remove(fdaCalendar);
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
