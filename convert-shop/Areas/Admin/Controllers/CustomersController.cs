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
    public class CustomersController : Controller
    {
        private ConvertManagementEntities db = new ConvertManagementEntities();

        // GET: Admin/Customers
        public ActionResult Index(string searchString, string customerRankingId, string customerTypeId, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var customers = db.Customers.Include(c => c.CustomerRanking)
                                         .Include(c => c.CustomerType)
                                         .Include(c => c.Role)
                                         .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.customer_name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(customerRankingId))
            {
                customers = customers.Where(c => c.CustomerRanking.customer_ranking_id.ToString() == customerRankingId);
            }

            if (!string.IsNullOrEmpty(customerTypeId))
            {
                customers = customers.Where(c => c.CustomerType.customer_type_id.ToString() == customerTypeId);
            }

            ViewBag.CustomerRanking = new SelectList(db.CustomerRankings, "customer_ranking_id", "customer_ranking_name", customerRankingId);
            ViewBag.CustomerType = new SelectList(db.CustomerTypes, "customer_type_id", "customer_type", customerTypeId);

            ViewBag.CurrentFilter = searchString;

            return View(customers.OrderBy(c => c.customer_name).ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            ViewBag.customer_ranking_id = new SelectList(db.CustomerRankings, "customer_ranking_id", "customer_ranking_name");
            ViewBag.customer_type_id = new SelectList(db.CustomerTypes, "customer_type_id", "customer_type");
            ViewBag.role_id = new SelectList(db.Roles, "id", "role_name");
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customer_name,email,phone_number,password,address,customer_type_id,role_id,loyaltyPoints,usedPoint,customer_ranking_id")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isEmailExist = db.Customers.Any(c => c.email == customer.email);
                    bool isPhoneExist = db.Customers.Any(c => c.phone_number == customer.phone_number);

                    if (isEmailExist)
                    {
                        ModelState.AddModelError("email", "Email already exists.");
                    }
                    if (isPhoneExist)
                    {
                        ModelState.AddModelError("phone_number", "Phone number already exists.");
                    }

                    if (isEmailExist || isPhoneExist)
                    {
                        LoadDropdownLists(customer);
                        return View(customer);
                    }


                    customer.createdAt = DateTime.Now;
                    customer.updatedAt = DateTime.Now;

                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the customer: " + ex.Message);
            }

            LoadDropdownLists(customer);
            return View(customer);
        }




        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_ranking_id = new SelectList(db.CustomerRankings, "customer_ranking_id", "customer_ranking_name", customer.customer_ranking_id);
            ViewBag.customer_type_id = new SelectList(db.CustomerTypes, "customer_type_id", "customer_type", customer.customer_type_id);
            ViewBag.role_id = new SelectList(db.Roles, "id", "role_name", customer.role_id);
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customer_id,customer_name,email,phone_number,password,address,customer_type_id,role_id,loyaltyPoints,usedPoint,customer_ranking_id,createdAt,updatedAt")] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdownLists(customer);
                return View(customer);
            }

            var existingCustomer = db.Customers.Find(customer.customer_id);
            if (existingCustomer == null)
            {
                return HttpNotFound();
            }

            if (!string.IsNullOrEmpty(customer.password) && customer.password != existingCustomer.password)
            {
                customer.password = customer.password;
            }
            else
            {
                customer.password = existingCustomer.password;
            }

            existingCustomer.customer_name = customer.customer_name;
            existingCustomer.email = customer.email;
            existingCustomer.phone_number = customer.phone_number;
            existingCustomer.address = customer.address;
            existingCustomer.customer_type_id = customer.customer_type_id;
            existingCustomer.role_id = customer.role_id;
            existingCustomer.loyaltyPoints = customer.loyaltyPoints;
            existingCustomer.usedPoint = customer.usedPoint;
            existingCustomer.customer_ranking_id = customer.customer_ranking_id;
            existingCustomer.updatedAt = DateTime.Now;

            db.Entry(existingCustomer).State = EntityState.Modified;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Customer updated successfully!";
            return RedirectToAction("Index");
        }

        private void LoadDropdownLists(Customer customer)
        {
            ViewBag.customer_ranking_id = new SelectList(db.CustomerRankings, "customer_ranking_id", "customer_ranking_name", customer.customer_ranking_id);
            ViewBag.customer_type_id = new SelectList(db.CustomerTypes, "customer_type_id", "customer_type", customer.customer_type_id);
            ViewBag.role_id = new SelectList(db.Roles, "id", "role_name", customer.role_id);
        }



        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
