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
    public class ListadoProductosController : Controller
    {
        private InventarioEntities db = new InventarioEntities();
        //*******************************************************************************************************************************
        // GET: ListadoProductos
        public ActionResult Index()
        {
            try
            {
                if (db == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Articulos articulos = new Articulos();
                var list = db.Articulos.ToList().Where(x => x.Estado == true);
                //return View(db.ObtenerListado().ToList());
                return View(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        //*******************************************************************************************************************************
        // GET: ListadoProductos/Details/5
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
        // GET: ListadoProductos/Create
        public ActionResult Create()
        {
            ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre");
            return View();
        }
        //*******************************************************************************************************************************
        // POST: ListadoProductos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArticulo,CodigoArticulos,Nombre,Estado,costo,CodigoProveedor")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                db.Articulos.Add(articulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(articulos);
        }
        //*******************************************************************************************************************************
        // GET: ListadoProductos/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(articulos);
        }
        //*******************************************************************************************************************************
        // POST: ListadoProductos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArticulo,CodigoArticulos,Nombre,Estado,costo,CodigoProveedor")] Articulos articulos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoProveedor = new SelectList(db.Proveedores, "CodigoProveedor", "Nombre", articulos.CodigoProveedor);
            return View(articulos);
        }
        //*******************************************************************************************************************************
        // GET: ListadoProductos/Delete/5
        public ActionResult Delete(int? id)
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
        // POST: ListadoProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articulos articulos = db.Articulos.Find(id);
            db.Articulos.Remove(articulos);
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
