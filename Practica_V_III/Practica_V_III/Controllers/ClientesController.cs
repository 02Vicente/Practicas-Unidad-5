using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practica_V_III_Model.Models;

namespace Practica_V_III.Controllers
{
    public class ClientesController : Controller
    {
        private CatalogoDBEntities db = new CatalogoDBEntities();
        
        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file)
        {
            if (file != null)
            {
                CatalogoDBEntities db = new CatalogoDBEntities();
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalpath = Server.MapPath("~/Images/" + ImageName);
                file.SaveAs(physicalpath);
                Clientes clientes = new Clientes();
                clientes.Nombres = Request.Form["Nombres"];
                clientes.Apellidos = Request.Form["Apellidos"];
                clientes.Direccion = Request.Form["Direccion"];
                clientes.Email = Request.Form["Email"];
                clientes.Telefono = Request.Form["Telefono"];
                clientes.Movil = Request.Form["Movil"];
                clientes.ImageUrl = ImageName;
                db.Clientes.Add(clientes);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, Clientes cliente)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string imageName = Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Images/" + imageName);
                    file.SaveAs(physicalPath);
                    cliente.ImageUrl = imageName;
                }

                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            db.Clientes.Remove(clientes);
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
