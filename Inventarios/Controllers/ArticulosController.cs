using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventarios;

namespace Inventarios.Controllers
{
    public class ArticulosController : Controller
    {
        private InventarioEntities db = new InventarioEntities();
        //*******************************************************************************************************************************
        // GET: Articulos
        public ActionResult Index()
        {
            var articulos = db.Articulos.Include(a => a.Proveedores);
            return View(articulos.ToList());
        }
        //*******************************************************************************************************************************
        // GET: Articulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }
        //*******************************************************************************************************************************
        // GET: Articulos/Create
        public ActionResult Create()
        {
            ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre");
            return View();
        }
        //*******************************************************************************************************************************
        // POST: Articulos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArticulo,CodigoArticulos,Nombre,Estado,costo,CodigoProveedor")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                db.InsertarArticulos(articulos.CodigoArticulos,articulos.Nombre,articulos.Estado,articulos.costo,articulos.CodigoProveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(articulos);
        }
        //*******************************************************************************************************************************
        // GET: Articulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            db.ActualizarArticulos(articulos.CodigoArticulos, articulos.Nombre, articulos.Estado, articulos.costo, articulos.CodigoProveedor);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(articulos);
        }
        //*******************************************************************************************************************************
        // POST: Articulos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArticulo,CodigoArticulos,Nombre,Estado,costo,CodigoProveedor")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                db.ActualizarArticulos(articulos.CodigoArticulos, articulos.Nombre, articulos.Estado, articulos.costo, articulos.CodigoProveedor);
                //db.Entry(articulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(articulos);
        }

        // GET: Articulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulos articulos = db.Articulos.Find(id);
            db.DesactivarArticulos(id);
            bool inactive = false;
            db.ActualizarArticulos(articulos.CodigoArticulos, articulos.Nombre, inactive, articulos.costo, articulos.CodigoProveedor);
            if (articulos == null)
            {
                return HttpNotFound();
            }
            return View(articulos);
        }
        //*******************************************************************************************************************************
        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articulos articulos = db.Articulos.Find(id);
            db.DesactivarArticulos(id);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //*******************************************************************************************************************************
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //*******************************************************************************************************************************
    }
}
