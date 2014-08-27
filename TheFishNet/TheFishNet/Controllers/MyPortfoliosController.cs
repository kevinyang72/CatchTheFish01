using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CatchTheFish.DbEntities;
using Microsoft.AspNet.Identity;

namespace TheFishNet.Controllers
{
    [Authorize]
    public class MyPortfoliosController : Controller
    {
        private TheFishEntities db = new TheFishEntities();

        // GET: MyPortfolios
        public ActionResult Index()
        {
            var profileId = new Guid(User.Identity.GetUserId());
            return View(db.MyPortfolios.Where(x => x.ProfileId == profileId).OrderBy(x => x.DisplayOrder).ToList());
        }

        // GET: MyPortfolios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyPortfolio myPortfolio = db.MyPortfolios.Find(id);
            if (myPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(myPortfolio);
        }

        // GET: MyPortfolios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyPortfolios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,DisplayOrder,LastModifiedDate")] MyPortfolio myPortfolio)
        {
            myPortfolio.LastModifiedDate = DateTime.Now;
            myPortfolio.ProfileId = new Guid(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.MyPortfolios.Add(myPortfolio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myPortfolio);
        }

        // GET: MyPortfolios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyPortfolio myPortfolio = db.MyPortfolios.Find(id);
            if (myPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(myPortfolio);
        }

        // POST: MyPortfolios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,DisplayOrder,LastModifiedDate")] MyPortfolio myPortfolio)
        {
            myPortfolio.LastModifiedDate = DateTime.Now;
            myPortfolio.ProfileId = new Guid(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.Entry(myPortfolio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myPortfolio);
        }

        // GET: MyPortfolios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyPortfolio myPortfolio = db.MyPortfolios.Find(id);
            if (myPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(myPortfolio);
        }

        // POST: MyPortfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MyPortfolio myPortfolio = db.MyPortfolios.Find(id);
            db.MyPortfolios.Remove(myPortfolio);
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
