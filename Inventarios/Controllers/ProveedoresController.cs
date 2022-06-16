using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventarios;

namespace Inventarios.Controllers
{
    public class ProveedoresController : Controller
    {
        private InventarioEntities db = new InventarioEntities();
        //*******************************************************************************************************************************
        // GET: Proveedores
        public ActionResult Index()
        {
            try
            {
                if (db == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var proveedor = db.Proveedores.ToList();
                }
                return View(db.Proveedores.ToList());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        //*******************************************************************************************************************************
        // GET: Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Proveedores proveedores = db.Proveedores.Find(id);
                if (proveedores == null)
                {
                    return HttpNotFound();
                }
                return View(proveedores);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);

            }
            
            
        }
        //*******************************************************************************************************************************
        // GET: Proveedores/Create
        public ActionResult Create()
        {
            return View();
        }
        //*******************************************************************************************************************************
        // POST: Proveedores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoProveedor,Nombre,estado,cedula,direccion,telefono")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                //store procedure to add 
                db.InsertarProveedores(proveedores.Nombre, proveedores.estado, proveedores.cedula, proveedores.direccion, proveedores.telefono);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proveedores);
        }
        //*******************************************************************************************************************************
        // GET: Proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            db.ActualizarProveedores(proveedores.Nombre, proveedores.estado, proveedores.cedula, proveedores.direccion, proveedores.telefono);
            if (proveedores == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }
        //*******************************************************************************************************************************
        // POST: Proveedores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoProveedor,Nombre,estado,cedula,direccion,telefono")] Proveedores proveedores)
        {
            if (ModelState.IsValid)
            {
                db.ActualizarProveedores(proveedores.Nombre, proveedores.estado, proveedores.cedula, proveedores.direccion, proveedores.telefono);
                //SqlParameter pNombre = new SqlParameter("@nombre", proveedores.Nombre);
                //SqlParameter pestado = new SqlParameter("@Estado", proveedores.estado);
                //SqlParameter pcedula = new SqlParameter("@cedula", proveedores.cedula);
                //SqlParameter pdireccion = new SqlParameter("@direccion", proveedores.direccion);
                //SqlParameter ptelefono = new SqlParameter("@telefono", proveedores.telefono);
                //var data = db.Database.SqlQuery<Proveedores>("exec ActualizarProveedores @nombre,@Estado,@cedula,@direccion,@telefono", pNombre,pestado, pcedula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proveedores);
        }
        //*******************************************************************************************************************************
        // GET: Proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedores proveedores = db.Proveedores.Find(id);
            db.DesactivarProveedores(id);
            bool inactive = false;
            db.ActualizarProveedores(proveedores.Nombre, inactive, proveedores.cedula, proveedores.direccion, proveedores.telefono);
            if (db == null)
            {
                return HttpNotFound();
            }
            return View(proveedores);
        }
        //*******************************************************************************************************************************
        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedores proveedores = db.Proveedores.Find(id);
            db.DesactivarProveedores(id);
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
