using convert_shop.Models;
using PagedList;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace convert_shop.Areas.Admin.Controllers
{
    public class CustomerRankingsController : Controller
    {
        private ConvertManagementEntities db = new ConvertManagementEntities();

        // GET: Admin/CustomerRankings
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var rankings = db.CustomerRankings.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                rankings = rankings.Where(c => c.customer_ranking_name.Contains(searchString));
            }
            ViewBag.CurrentFilter = searchString;

            return View(rankings.OrderBy(c => c.customer_ranking_name).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/CustomerRankings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRanking customerRanking = db.CustomerRankings.Find(id);
            if (customerRanking == null)
            {
                return HttpNotFound();
            }
            return View(customerRanking);
        }

        // GET: Admin/CustomerRankings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CustomerRankings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customer_ranking_name,required_points,voucher_discount_percentage")] CustomerRanking customerRanking)
        {
            if (ModelState.IsValid)
            {
                db.CustomerRankings.Add(customerRanking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerRanking);
        }

        // GET: Admin/CustomerRankings/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRanking customerRanking = db.CustomerRankings.Find(id);
            if (customerRanking == null)
            {
                return HttpNotFound();
            }
            return View(customerRanking);
        }

        // POST: Admin/CustomerRankings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customer_ranking_id,customer_ranking_name,required_points,voucher_discount_percentage,createdAt,updatedAt")] CustomerRanking customerRanking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerRanking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerRanking);
        }

        // GET: Admin/CustomerRankings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRanking customerRanking = db.CustomerRankings.Find(id);
            if (customerRanking == null)
            {
                return HttpNotFound();
            }
            return View(customerRanking);
        }

        // POST: Admin/CustomerRankings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CustomerRanking customerRanking = db.CustomerRankings.Find(id);
            db.CustomerRankings.Remove(customerRanking);
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
