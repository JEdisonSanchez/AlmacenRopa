using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using AlmacenRopa.Models;

namespace AlmacenRopa.Controllers
{
    public class ProductController : Controller
    {
        private CLOTHINGSTOREEntities db = new CLOTHINGSTOREEntities();

        // GET: Product
        public ActionResult Index()
        {
            var pRODUCT = db.PRODUCT.Include(p => p.C_PROVIDER);
            return View(pRODUCT.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.ID_PROVIDER = new SelectList(db.C_PROVIDER, "ID_PROVIDER", "NAMES");
            return View();
        }

        // POST: Product/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PRODUCT,CODE_PRODUCT,PHOTO,NAME_PRODUCT,DESCRIPTION_PRODUCT,STOCK,PRICE_PRODUCT,ID_PROVIDER")] PRODUCT pRODUCT)
        {
            HttpPostedFileBase fileBase = Request.Files[0];

            if (fileBase.ContentLength == 0)
            {
                WebImage image = new WebImage("~/Img/default.png");
                pRODUCT.PHOTO = image.GetBytes();
            }
            else
            {
                if (fileBase.FileName.EndsWith(".jpg"))
                {
                    WebImage image = new WebImage(fileBase.InputStream);
                    pRODUCT.PHOTO = image.GetBytes();

                }
            }

            if (ModelState.IsValid)
            {
                db.PRODUCT.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PROVIDER = new SelectList(db.C_PROVIDER, "ID_PROVIDER", "IDENTIFICATIONCARD", pRODUCT.ID_PROVIDER);
            return View(pRODUCT);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PROVIDER = new SelectList(db.C_PROVIDER, "ID_PROVIDER", "IDENTIFICATIONCARD", pRODUCT.ID_PROVIDER);
            return View(pRODUCT);
        }

        // POST: Product/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PRODUCT,CODE_PRODUCT,PHOTO,NAME_PRODUCT,DESCRIPTION_PRODUCT,STOCK,PRICE_PRODUCT,ID_PROVIDER")] PRODUCT pRODUCT)
        {
            PRODUCT _Product = new PRODUCT();

            HttpPostedFileBase fileBase = Request.Files[0];

            if (fileBase.ContentLength == 0)
            {
                _Product = db.PRODUCT.Find(pRODUCT.ID_PRODUCT);
                pRODUCT.PHOTO = _Product.PHOTO;
            }
            else
            {
                if (fileBase.FileName.EndsWith(".jpg"))
                {
                    WebImage image = new WebImage(fileBase.InputStream);

                    pRODUCT.PHOTO = image.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("PHOTO", "El sistema unicamente acepta imagenes con formato JPG.");
                }


            }

            if (ModelState.IsValid)
            {
                db.Entry(_Product).State = EntityState.Detached;
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PROVIDER = new SelectList(db.C_PROVIDER, "ID_PROVIDER", "IDENTIFICATIONCARD", pRODUCT.ID_PROVIDER);
            return View(pRODUCT);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            db.PRODUCT.Remove(pRODUCT);
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

        public ActionResult getImage(int id)
        {
            PRODUCT product = db.PRODUCT.Find(id);
            byte[] byteImage = product.PHOTO;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");

        }

    }
}
