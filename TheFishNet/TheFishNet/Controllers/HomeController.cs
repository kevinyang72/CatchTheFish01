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
            // User.
            var profileId = new Guid(User.Identity.GetUserId());
            ViewBag.PortfolioSelection = db.MyPortfolios.Where(x => x.ProfileId==profileId).Select(x => new SelectListItem
             {
                 Value = x.Id + "|" +  x.Name,
                 Text = x.Name
             });
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PortfolioId,PortfolioName,Symbol,DisplayOrder,LastModifiedDate")] MyPortfolioStock myPortfolioStock)
        {
            if (ModelState.IsValid)
            {
                var profileId = new Guid(User.Identity.GetUserId());
                var portfolioId = int.Parse(myPortfolioStock.PortfolioName.Substring(0, myPortfolioStock.PortfolioName.IndexOf("|")));
                var portfolioName = myPortfolioStock.PortfolioName.Substring(myPortfolioStock.PortfolioName.IndexOf("|") + 1);
                var stockList = myPortfolioStock.Symbol.Split(';');
                var query = db.MyPortfolioStocks.Where(x => x.ProfileId == profileId && x.PortfolioId == portfolioId).OrderByDescending(n=>n.DisplayOrder).Select(x => x.DisplayOrder);
                var maxDisplayOrder = 0;
                if(query.Any())
                {
                    maxDisplayOrder = query.FirstOrDefault();
                }
                foreach(var item in stockList)
                {
                    
                    var stockExists = db.MyPortfolioStocks.Where(x => x.ProfileId == profileId && x.PortfolioId == portfolioId && x.Symbol.Equals(item)).Any();
                    if (!stockExists)
                    {
                        maxDisplayOrder++;
                        var stock = new MyPortfolioStock
                        {
                            ProfileId = profileId,
                            PortfolioId = portfolioId,
                            PortfolioName = portfolioName,
                            Symbol = item,
                            DisplayOrder = maxDisplayOrder,
                            LastModifiedDate = DateTime.Now
                        };
                        db.MyPortfolioStocks.Add(stock);
                        db.SaveChanges();
                        
                    }
                }
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
        public ActionResult Edit([Bind(Include = "Id,PortfolioId,PortfolioName,Symbol,DisplayOrder,LastModifiedDate")] MyPortfolioStock myPortfolioStock)
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
