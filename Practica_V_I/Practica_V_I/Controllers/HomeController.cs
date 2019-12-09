using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practica_V_I_Model.Models;

namespace Practica_V_I.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                Estudiantes1DBEntities1 db = new Estudiantes1DBEntities1();
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalpath = Server.MapPath("~/Images/" + ImageName);
                file.SaveAs(physicalpath);
                tblEstudiante estudiante = new tblEstudiante();
                estudiante.Nombres = Request.Form["Nombres"];
                estudiante.Apellidos = Request.Form["Apellidos"];
                estudiante.Direccion = Request.Form["Direccion"];
                estudiante.Telefono = Request.Form["Telefono"];
                estudiante.Cedula = Request.Form["Cedula"];
                estudiante.ImageUrl = ImageName;
                db.tblEstudiante.Add(estudiante);
                db.SaveChanges();
            }

            return RedirectToAction("Detalle");
        }

        public ActionResult Detalle()
        {
            return View();
        }

        public ActionResult Estudiantes()
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