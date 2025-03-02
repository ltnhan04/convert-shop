using convert_shop.Models;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace convert_shop.Areas.Admin.Controllers
{
    public class ProductStatusController : Controller
    {
        private ConvertManagementEntities db = new ConvertManagementEntities();

        // GET: Admin/ProductStatus
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var productStatus = db.ProductStatus.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                productStatus = productStatus.Where(c => c.status_name.Contains(searchString));
            }
            ViewBag.CurrentFilter = searchString;

            return View(productStatus.OrderBy(c => c.status_name).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/ProductStatus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStatu productStatu = db.ProductStatus.Find(id);
            if (productStatu == null)
            {
                return HttpNotFound();
            }
            return View(productStatu);
        }

        // GET: Admin/ProductStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "status_name")] ProductStatu productStatu)
        {
            if (ModelState.IsValid)
            {
                productStatu.createdAt = DateTime.Now;
                productStatu.updatedAt = DateTime.Now;
                db.ProductStatus.Add(productStatu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productStatu);
        }

        // GET: Admin/ProductStatus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStatu productStatu = db.ProductStatus.Find(id);
            if (productStatu == null)
            {
                return HttpNotFound();
            }
            return View(productStatu);
        }

        // POST: Admin/ProductStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductStatu productStatu)
        {
            if (ModelState.IsValid)
            {
                productStatu.updatedAt = DateTime.Now;
                db.Entry(productStatu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productStatu);
        }

        // GET: Admin/ProductStatus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStatu productStatu = db.ProductStatus.Find(id);
            if (productStatu == null)
            {
                return HttpNotFound();
            }
            return View(productStatu);
        }

        // POST: Admin/ProductStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ProductStatu productStatu = db.ProductStatus.Find(id);
            db.ProductStatus.Remove(productStatu);
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
