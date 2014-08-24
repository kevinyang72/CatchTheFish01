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
    public class HomeController : Controller
    {
        private TheFishEntities db = new TheFishEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.MyPortfolioStocks.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyPortfolioStock myPortfolioStock = db.MyPortfolioStocks.Find(id);
            if (myPortfolioStock == null)
            {
                return HttpNotFound();
            }
            return View(myPortfolioStock);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PortotfolioName,Symbol,DisplayOrder,Price,Volume,LastModifiedDate")] MyPortfolioStock myPortfolioStock)
        {
            if (ModelState.IsValid)
            {
                db.MyPortfolioStocks.Add(myPortfolioStock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myPortfolioStock);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyPortfolioStock myPortfolioStock = db.MyPortfolioStocks.Find(id);
            if (myPortfolioStock == null)
            {
                return HttpNotFound();
            }
            return View(myPortfolioStock);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PortotfolioName,Symbol,DisplayOrder,Price,Volume,LastModifiedDate")] MyPortfolioStock myPortfolioStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myPortfolioStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myPortfolioStock);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyPortfolioStock myPortfolioStock = db.MyPortfolioStocks.Find(id);
            if (myPortfolioStock == null)
            {
                return HttpNotFound();
            }
            return View(myPortfolioStock);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MyPortfolioStock myPortfolioStock = db.MyPortfolioStocks.Find(id);
            db.MyPortfolioStocks.Remove(myPortfolioStock);
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
