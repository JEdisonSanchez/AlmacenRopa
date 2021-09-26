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
    public class UserController : Controller
    {
        private CLOTHINGSTOREEntities db = new CLOTHINGSTOREEntities();

        // GET: User
        public ActionResult Index()
        {
            var c_USER = db.C_USER.Include(c => c.ROLE_USER);
            return View(c_USER.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_USER c_USER = db.C_USER.Find(id);
            if (c_USER == null)
            {
                return HttpNotFound();
            }
            return View(c_USER);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.IDROLE = new SelectList(db.ROLE_USER, "ID_ROLE", "NAME_ROLE");
            return View();
        }

        // POST: User/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDUSER,IDENTIFICATIONCARD,TYPE_IDENTIFICATION,PHOTO,NAMES,SURNAMES,ADDRESS_USER,PHONE,EMAIL,ORIGINCITY,SESION_NAME,SESION_PASSWORD,IDROLE")] C_USER c_USER)
        {
            HttpPostedFileBase fileBase = Request.Files[0];


            if (fileBase.ContentLength == 0)
            {
                WebImage image = new WebImage("~/Img/default.png");
                c_USER.PHOTO = image.GetBytes();
            }
            else
            {
                if (fileBase.FileName.EndsWith(".jpg"))
                {
                    WebImage image = new WebImage(fileBase.InputStream);
                    c_USER.PHOTO = image.GetBytes();

                }
            }

            if (ModelState.IsValid)
            {
                db.C_USER.Add(c_USER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDROLE = new SelectList(db.ROLE_USER, "ID_ROLE", "NAME_ROLE", c_USER.IDROLE);
            return View(c_USER);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_USER c_USER = db.C_USER.Find(id);
            if (c_USER == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDROLE = new SelectList(db.ROLE_USER, "ID_ROLE", "NAME_ROLE", c_USER.IDROLE);
            return View(c_USER);
        }

        // POST: User/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDUSER,IDENTIFICATIONCARD,TYPE_IDENTIFICATION,PHOTO,NAMES,SURNAMES,ADDRESS_USER,PHONE,EMAIL,ORIGINCITY,SESION_NAME,SESION_PASSWORD,IDROLE")] C_USER c_USER)
        {

            C_USER _user = new C_USER();

            HttpPostedFileBase fileBase = Request.Files[0];

            if (fileBase.ContentLength == 0)
            {
                _user = db.C_USER.Find(c_USER.IDUSER);
                c_USER.PHOTO = _user.PHOTO;
            }
            else
            {
                if (fileBase.FileName.EndsWith(".jpg"))
                {
                    WebImage image = new WebImage(fileBase.InputStream);

                    c_USER.PHOTO = image.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("PHOTO", "El sistema unicamente acepta imagenes con formato JPG.");
                }


            }

            if (ModelState.IsValid)
            {
                db.Entry(_user).State = EntityState.Detached;
                db.Entry(c_USER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDROLE = new SelectList(db.ROLE_USER, "ID_ROLE", "NAME_ROLE", c_USER.IDROLE);
            return View(c_USER);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_USER c_USER = db.C_USER.Find(id);
            if (c_USER == null)
            {
                return HttpNotFound();
            }
            return View(c_USER);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_USER c_USER = db.C_USER.Find(id);
            db.C_USER.Remove(c_USER);
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
            C_USER user = db.C_USER.Find(id);
            byte[] byteImage = user.PHOTO;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");

        }

    }
}
