using AlmacenRopa.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AlmacenRopa.Controllers
{
    public class HomeController : Controller
    {
        private CLOTHINGSTOREEntities db = new CLOTHINGSTOREEntities();


        [HttpPost]
        public string Login(string txtUsuario, string txtPassword)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            settings.Converters.Add(new StringEnumConverter());


            var datosUser = db.C_USER.Select(a => a).Where(x => x.SESION_NAME == txtUsuario && x.SESION_PASSWORD == txtPassword).FirstOrDefault();

            if (datosUser != null)
            {
                FormsAuthentication.SetAuthCookie(datosUser.SESION_NAME, false);
                HttpContext.Session.Add("usuario", datosUser.SESION_NAME);
                HttpContext.Session.Add("rol", datosUser.ROLE_USER.NAME_ROLE);
                HttpContext.Session.Add("id", datosUser.IDUSER);

                var json = JsonConvert.SerializeObject(datosUser, settings);

                //return Newtonsoft.Json.JsonConvert.DeserializeObject(datosUser);
                return Newtonsoft.Json.JsonConvert.SerializeObject(json);

            }
            else

                return "{\"error\":\"Datos Erroneos\"}";

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("index");
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}