using convert_shop.Models;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace convert_shop.Areas.Admin.Controllers
{
    public class ProductImagesController : Controller
    {
        private ConvertManagementEntities db = new ConvertManagementEntities();

        // GET: Admin/ProductImages
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var images = db.ProductImages.AsQueryable();

            return View(images.OrderBy(c => c.image_url_1).ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/ProductImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductImage productImage)
        {
            try
            {
                string uploadPath = Server.MapPath("~/Content/Uploads/");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (productImage.ImageFile1 != null && productImage.ImageFile1.ContentLength > 0)
                {
                    string fileName1 = Path.GetFileName(productImage.ImageFile1.FileName);
                    string path1 = Path.Combine(uploadPath, fileName1);
                    System.Diagnostics.Debug.WriteLine("File saved: " + path1);
                    productImage.ImageFile1.SaveAs(path1);
                    productImage.image_url_1 = "/Uploads/" + fileName1;
                }
                else if (string.IsNullOrEmpty(productImage.image_url_1))
                {
                    ModelState.AddModelError("ImageFile1", "Please upload the first image.");
                }

                if (productImage.ImageFile2 != null && productImage.ImageFile2.ContentLength > 0)
                {
                    string fileName2 = Path.GetFileName(productImage.ImageFile2.FileName);
                    string path2 = Path.Combine(uploadPath, fileName2);
                    productImage.ImageFile2.SaveAs(path2);
                    productImage.image_url_2 = "/Uploads/" + fileName2;
                }

                if (productImage.ImageFile3 != null && productImage.ImageFile3.ContentLength > 0)
                {
                    string fileName3 = Path.GetFileName(productImage.ImageFile3.FileName);
                    string path3 = Path.Combine(uploadPath, fileName3);
                    productImage.ImageFile3.SaveAs(path3);
                    productImage.image_url_3 = "/Uploads/" + fileName3;
                }


                if (ModelState.IsValid)
                {
                    db.ProductImages.Add(productImage);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return View(productImage);
        }

        // GET: Admin/ProductImages/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: Admin/ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductImage productImage)
        {
            var existingImage = db.ProductImages.Find(productImage.image_id);
            if (existingImage == null)
            {
                return HttpNotFound();
            }
            string uploadPath = Server.MapPath("~/Content/Uploads/");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            if (productImage.ImageFile1 != null && productImage.ImageFile1.ContentLength > 0)
            {
                string fileName1 = Path.GetFileName(productImage.ImageFile1.FileName);
                string filePath1 = Path.Combine(uploadPath, fileName1);
                productImage.ImageFile1.SaveAs(filePath1);
                existingImage.image_url_1 = "/Uploads/" + fileName1;
            }

            if (productImage.ImageFile2 != null && productImage.ImageFile2.ContentLength > 0)
            {
                string fileName2 = Path.GetFileName(productImage.ImageFile2.FileName);
                string filePath2 = Path.Combine(uploadPath, fileName2);
                productImage.ImageFile2.SaveAs(filePath2);
                existingImage.image_url_2 = "/Uploads/" + fileName2;
            }

            if (productImage.ImageFile3 != null && productImage.ImageFile3.ContentLength > 0)
            {
                string fileName3 = Path.GetFileName(productImage.ImageFile3.FileName);
                string filePath3 = Path.Combine(uploadPath, fileName3);
                productImage.ImageFile3.SaveAs(filePath3);
                existingImage.image_url_3 = "/Uploads/" + fileName3;
            }


            if (ModelState.IsValid)
            {
                existingImage.updatedAt = DateTime.Now;
                db.Entry(existingImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productImage);
        }

        // GET: Admin/ProductImages/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: Admin/ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
            db.ProductImages.Remove(productImage);
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
