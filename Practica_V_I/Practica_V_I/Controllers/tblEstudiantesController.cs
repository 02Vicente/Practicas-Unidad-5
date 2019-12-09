using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practica_V_I_Model.Models;

namespace Practica_V_I.Controllers
{
    public class tblEstudiantesController : Controller
    {
        private Estudiantes1DBEntities1 db = new Estudiantes1DBEntities1();

        // GET: tblEstudiantes
        public ActionResult Index()
        {
            return View(db.tblEstudiante.ToList());
        }

        // GET: tblEstudiantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstudiante tblEstudiante = db.tblEstudiante.Find(id);
            if (tblEstudiante == null)
            {
                return HttpNotFound();
            }
            return View(tblEstudiante);
        }

        // GET: tblEstudiantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblEstudiantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstudianteID,Nombres,Apellidos,Direccion,Telefono,Cedula,ImageUrl")] tblEstudiante tblEstudiante)
        {
            if (ModelState.IsValid)
            {
                db.tblEstudiante.Add(tblEstudiante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEstudiante);
        }

        // GET: tblEstudiantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstudiante tblEstudiante = db.tblEstudiante.Find(id);
            if (tblEstudiante == null)
            {
                return HttpNotFound();
            }
            return View(tblEstudiante);
        }

        // POST: tblEstudiantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstudianteID,Nombres,Apellidos,Direccion,Telefono,Cedula,ImageUrl")] tblEstudiante tblEstudiante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEstudiante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEstudiante);
        }

        // GET: tblEstudiantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEstudiante tblEstudiante = db.tblEstudiante.Find(id);
            if (tblEstudiante == null)
            {
                return HttpNotFound();
            }
            return View(tblEstudiante);
        }

        // POST: tblEstudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEstudiante tblEstudiante = db.tblEstudiante.Find(id);
            db.tblEstudiante.Remove(tblEstudiante);
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
