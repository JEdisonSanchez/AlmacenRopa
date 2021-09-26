using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlmacenRopa.Models;

namespace AlmacenRopa.Controllers
{
    public class SaleController : Controller
    {
        private CLOTHINGSTOREEntities db = new CLOTHINGSTOREEntities();

        // GET: Sale
        public ActionResult Index()
        {
            var sALE = db.SALE.Include(s => s.PRODUCT);
            return View(sALE.ToList());
        }

        // GET: Sale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE sALE = db.SALE.Find(id);
            if (sALE == null)
            {
                return HttpNotFound();
            }
            return View(sALE);
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            ViewBag.ID_PRODUCT = new SelectList(db.PRODUCT, "ID_PRODUCT", "CODE_PRODUCT");
            return View();
        }

        // POST: Sale/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SALE,USER_CLIENT,USER_SALLER,AMOUNT,DATE_SALE,TOTAL_VALUE,ID_PRODUCT")] SALE sALE)
        {
            if (ModelState.IsValid)
            {
                db.SALE.Add(sALE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PRODUCT = new SelectList(db.PRODUCT, "ID_PRODUCT", "CODE_PRODUCT", sALE.ID_PRODUCT);
            return View(sALE);
        }

        // GET: Sale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE sALE = db.SALE.Find(id);
            if (sALE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PRODUCT = new SelectList(db.PRODUCT, "ID_PRODUCT", "CODE_PRODUCT", sALE.ID_PRODUCT);
            return View(sALE);
        }

        // POST: Sale/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SALE,USER_CLIENT,USER_SALLER,AMOUNT,DATE_SALE,TOTAL_VALUE,ID_PRODUCT")] SALE sALE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sALE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PRODUCT = new SelectList(db.PRODUCT, "ID_PRODUCT", "CODE_PRODUCT", sALE.ID_PRODUCT);
            return View(sALE);
        }

        // GET: Sale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE sALE = db.SALE.Find(id);
            if (sALE == null)
            {
                return HttpNotFound();
            }
            return View(sALE);
        }

        // POST: Sale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SALE sALE = db.SALE.Find(id);
            db.SALE.Remove(sALE);
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
