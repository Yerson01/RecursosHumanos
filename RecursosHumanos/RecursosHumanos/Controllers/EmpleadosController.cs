using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecursosHumanos.Models;

namespace RecursosHumanos.Controllers
{
    public class EmpleadosController : Controller
    {
        private RecursosHumanosEntities db = new RecursosHumanosEntities();

        public ActionResult Totalizar()
        {
            ViewBag.TotalSalario = db.Empleados.Sum(a => a.Salario);
            ViewBag.TotalEmpleados = db.Empleados.Count();

            db.SaveChanges();
            return View();
        }

        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.cargo1).Include(e => e.Departamento1);
            return View(empleados.ToList());
        }

        [HttpPost]
        public ActionResult Index(string buscar)
        {

            if (buscar != "")
            {
                List<Empleados> empleados = new List<Empleados>();
                empleados = db.Empleados.Where(x => x.Nombre.Contains(buscar) || x.Apellido.Contains(buscar)).ToList();
                return View(empleados);
            }

            return View(db.Empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo1");
            ViewBag.Departamento = new SelectList(db.Departamento, "ID", "Nombre");
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Codigo_Empleado,Departamento,Nombre,Apellido,Cargo,Salario,Fecha_De_Ingreso,Telefono,Estatus")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo1", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamento, "ID", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo1", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamento, "ID", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Codigo_Empleado,Departamento,Nombre,Apellido,Cargo,Salario,Fecha_De_Ingreso,Telefono,Estatus")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cargo = new SelectList(db.cargo, "ID", "Cargo1", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamento, "ID", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
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
