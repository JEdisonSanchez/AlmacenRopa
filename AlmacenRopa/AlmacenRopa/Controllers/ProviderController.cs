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
    public class ProviderController : Controller
    {
        private CLOTHINGSTOREEntities db = new CLOTHINGSTOREEntities();

        // GET: Provider
        public ActionResult Index()
        {
            return View(db.C_PROVIDER.ToList());
        }

        // GET: Provider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_PROVIDER c_PROVIDER = db.C_PROVIDER.Find(id);
            if (c_PROVIDER == null)
            {
                return HttpNotFound();
            }
            return View(c_PROVIDER);
        }

        // GET: Provider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provider/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PROVIDER,IDENTIFICATIONCARD,NAMES,SURNAMES,PHONE,EMAIL,NIT")] C_PROVIDER c_PROVIDER)
        {
            if (ModelState.IsValid)
            {
                db.C_PROVIDER.Add(c_PROVIDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_PROVIDER);
        }

        // GET: Provider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_PROVIDER c_PROVIDER = db.C_PROVIDER.Find(id);
            if (c_PROVIDER == null)
            {
                return HttpNotFound();
            }
            return View(c_PROVIDER);
        }

        // POST: Provider/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PROVIDER,IDENTIFICATIONCARD,NAMES,SURNAMES,PHONE,EMAIL,NIT")] C_PROVIDER c_PROVIDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_PROVIDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_PROVIDER);
        }

        // GET: Provider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_PROVIDER c_PROVIDER = db.C_PROVIDER.Find(id);
            if (c_PROVIDER == null)
            {
                return HttpNotFound();
            }
            return View(c_PROVIDER);
        }

        // POST: Provider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_PROVIDER c_PROVIDER = db.C_PROVIDER.Find(id);
            db.C_PROVIDER.Remove(c_PROVIDER);
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
