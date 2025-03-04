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
    public class CustomerTypesController : Controller
    {
        private ConvertManagementEntities db = new ConvertManagementEntities();

        // GET: Admin/CustomerTypes
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var types = db.CustomerTypes.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                types = types.Where(c => c.customer_type.Contains(searchString));
            }
            ViewBag.CurrentFilter = searchString;

            return View(types.OrderBy(c => c.customer_type).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/CustomerTypes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerType customerType = db.CustomerTypes.Find(id);
            if (customerType == null)
            {
                return HttpNotFound();
            }
            return View(customerType);
        }

        // GET: Admin/CustomerTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CustomerTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customer_type")] CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                customerType.createdAt = DateTime.Now;
                customerType.updatedAt = DateTime.Now;
                db.CustomerTypes.Add(customerType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerType);
        }

        // GET: Admin/CustomerTypes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerType customerType = db.CustomerTypes.Find(id);
            if (customerType == null)
            {
                return HttpNotFound();
            }
            return View(customerType);
        }

        // POST: Admin/CustomerTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                customerType.updatedAt = DateTime.Now;
                db.Entry(customerType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerType);
        }

        // GET: Admin/CustomerTypes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerType customerType = db.CustomerTypes.Find(id);
            if (customerType == null)
            {
                return HttpNotFound();
            }
            return View(customerType);
        }

        // POST: Admin/CustomerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CustomerType customerType = db.CustomerTypes.Find(id);
            db.CustomerTypes.Remove(customerType);
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
