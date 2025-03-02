using convert_shop.Models;
using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace convert_shop.Areas.Admin.Controllers
{
    public class ProductColorsController : Controller
    {
        private ConvertManagementEntities db = new ConvertManagementEntities();

        // GET: Admin/ProductColors
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var productColors = db.ProductColors.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                productColors = productColors.Where(c => c.color_name.Contains(searchString));
            }
            ViewBag.CurrentFilter = searchString;

            return View(productColors.OrderBy(c => c.color_name).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/ProductColors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "color_name,color_code")] ProductColor productColor)
        {
            if (ModelState.IsValid)
            {
                productColor.createdAt = DateTime.Now;
                productColor.updatedAt = DateTime.Now;
                db.ProductColors.Add(productColor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productColor);
        }

        // GET: Admin/ProductColors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductColor productColor = db.ProductColors.Find(id);
            if (productColor == null)
            {
                return HttpNotFound();
            }
            return View(productColor);
        }

        // POST: Admin/ProductColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductColor productColor)
        {
            if (ModelState.IsValid)
            {
                productColor.updatedAt = DateTime.Now;
                db.Entry(productColor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productColor);
        }

        // GET: Admin/ProductColors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductColor productColor = db.ProductColors.Find(id);
            if (productColor == null)
            {
                return HttpNotFound();
            }
            return View(productColor);
        }

        // POST: Admin/ProductColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ProductColor productColor = db.ProductColors.Find(id);
            db.ProductColors.Remove(productColor);
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
