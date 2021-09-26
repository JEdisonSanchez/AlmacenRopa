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
    public class RoleUserController : Controller
    {
        private CLOTHINGSTOREEntities db = new CLOTHINGSTOREEntities();

        // GET: RoleUser
        public ActionResult Index()
        {
            return View(db.ROLE_USER.ToList());
        }

        // GET: RoleUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROLE_USER rOLE_USER = db.ROLE_USER.Find(id);
            if (rOLE_USER == null)
            {
                return HttpNotFound();
            }
            return View(rOLE_USER);
        }

        // GET: RoleUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleUser/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ROLE,NAME_ROLE,DESCRIPTION_ROLE")] ROLE_USER rOLE_USER)
        {
            if (ModelState.IsValid)
            {
                db.ROLE_USER.Add(rOLE_USER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rOLE_USER);
        }

        // GET: RoleUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROLE_USER rOLE_USER = db.ROLE_USER.Find(id);
            if (rOLE_USER == null)
            {
                return HttpNotFound();
            }
            return View(rOLE_USER);
        }

        // POST: RoleUser/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ROLE,NAME_ROLE,DESCRIPTION_ROLE")] ROLE_USER rOLE_USER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOLE_USER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rOLE_USER);
        }

        // GET: RoleUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROLE_USER rOLE_USER = db.ROLE_USER.Find(id);
            if (rOLE_USER == null)
            {
                return HttpNotFound();
            }
            return View(rOLE_USER);
        }

        // POST: RoleUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ROLE_USER rOLE_USER = db.ROLE_USER.Find(id);
            db.ROLE_USER.Remove(rOLE_USER);
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
