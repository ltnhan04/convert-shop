using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using convert_shop.Models;

namespace convert_shop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ConvertManagementEntities db = new ConvertManagementEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.ProductColor).Include(p => p.ProductImage).Include(p => p.ProductStatu);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            ViewBag.color_id = new SelectList(db.ProductColors, "color_id", "color_name");
            ViewBag.image_id = new SelectList(db.ProductImages, "image_id", "image_url_1");
            ViewBag.status_id = new SelectList(db.ProductStatus, "status_id", "status_name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,product_description,product_price,category_id,image_id,color_id,status_id,quantity,createdAt,updatedAt")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", product.category_id);
            ViewBag.color_id = new SelectList(db.ProductColors, "color_id", "color_name", product.color_id);
            ViewBag.image_id = new SelectList(db.ProductImages, "image_id", "image_url_1", product.image_id);
            ViewBag.status_id = new SelectList(db.ProductStatus, "status_id", "status_name", product.status_id);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", product.category_id);
            ViewBag.color_id = new SelectList(db.ProductColors, "color_id", "color_name", product.color_id);
            ViewBag.image_id = new SelectList(db.ProductImages, "image_id", "image_url_1", product.image_id);
            ViewBag.status_id = new SelectList(db.ProductStatus, "status_id", "status_name", product.status_id);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,product_description,product_price,category_id,image_id,color_id,status_id,quantity,createdAt,updatedAt")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", product.category_id);
            ViewBag.color_id = new SelectList(db.ProductColors, "color_id", "color_name", product.color_id);
            ViewBag.image_id = new SelectList(db.ProductImages, "image_id", "image_url_1", product.image_id);
            ViewBag.status_id = new SelectList(db.ProductStatus, "status_id", "status_name", product.status_id);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
