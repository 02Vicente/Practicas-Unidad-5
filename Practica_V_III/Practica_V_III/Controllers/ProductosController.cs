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
    public class ProductosController : Controller
    {
        private CatalogoDBEntities db = new CatalogoDBEntities();

        // GET: Productos
        public ActionResult Index()
        {
            return View(db.Productos.ToList());
        }

       

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
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
                Productos productos = new Productos();
                productos.Nombre = Request.Form["Nombre"];
                productos.Descripcion = Request.Form["Descripcion"];
                productos.Precio = double.Parse(Request.Form["Precio"]);
                productos.CantExistente = int.Parse(Request.Form["CantExistente"]);
                productos.ImageUrl = ImageName;
                db.Productos.Add(productos);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Productos producto = db.Productos.Find(id);

            return View(producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
            public ActionResult Edit(HttpPostedFileBase file, Productos producto)
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string imageName = Path.GetFileName(file.FileName);
                        string physicalPath = Server.MapPath("~/Images/" + imageName);
                        file.SaveAs(physicalPath);
                        producto.ImageUrl = imageName;
                    }

                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(producto);
            }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
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
