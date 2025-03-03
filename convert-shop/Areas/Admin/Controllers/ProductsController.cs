using convert_shop.Models;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace convert_shop.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ConvertManagementEntities db = new ConvertManagementEntities();

        // GET: Admin/Products
        public ActionResult Index(string searchString, string categoryId, string colorId, string statusId, int? minPrice, int? maxPrice, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColor)
                .Include(p => p.ProductImage)
                .Include(p => p.ProductStatu)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.product_name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                products = products.Where(p => p.Category.category_name == categoryId);
            }

            if (!string.IsNullOrEmpty(colorId))
            {
                products = products.Where(p => p.ProductColor.color_name == colorId);
            }

            if (!string.IsNullOrEmpty(statusId))
            {
                products = products.Where(p => p.ProductStatu.status_name == statusId);
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.product_price >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.product_price <= maxPrice);
            }

            ViewBag.Categories = new SelectList(db.Categories.Select(c => c.category_name).Distinct().ToList(), categoryId);
            ViewBag.Colors = new SelectList(db.ProductColors.Select(c => c.color_name).Distinct().ToList(), colorId);
            ViewBag.Statuses = new SelectList(db.ProductStatus.Select(s => s.status_name).Distinct().ToList(), statusId);

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentColor = colorId;
            ViewBag.CurrentStatus = statusId;
            ViewBag.CurrentMinPrice = minPrice;
            ViewBag.CurrentMaxPrice = maxPrice;

            return View(products.OrderBy(p => p.product_name).ToPagedList(pageNumber, pageSize));
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
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_name,product_description,product_price,category_id,image_id,color_id,status_id,quantity,slug")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.createdAt = System.DateTime.Now;
                product.updatedAt = System.DateTime.Now;
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
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,product_description,product_price,category_id,image_id,color_id,status_id,quantity,slug")] Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = db.Products.Find(product.product_id);
                if (existingProduct == null)
                {
                    return HttpNotFound();
                }

                existingProduct.product_name = product.product_name;
                existingProduct.product_description = product.product_description;
                existingProduct.product_price = product.product_price;
                existingProduct.category_id = product.category_id;
                existingProduct.image_id = product.image_id;
                existingProduct.color_id = product.color_id;
                existingProduct.status_id = product.status_id;
                existingProduct.quantity = product.quantity;
                existingProduct.slug = product.slug;
                existingProduct.updatedAt = System.DateTime.Now;

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
