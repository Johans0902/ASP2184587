using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2184587.Models;

namespace _2184587.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.compra.ToList());
            }
        }

        public static string nombreUsuario(int? idUsuario)
        {
            using (var db = new inventarioEntities1())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }

        public ActionResult listarUsuarios()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public static string nombreCliente(int? idCliente)
        {
            using (var db = new inventarioEntities1())
            {
                return db.cliente.Find(idCliente).nombre;
            }
        }

        public ActionResult listarClientes()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.cliente.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(compra purchase)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {
                    db.compra.Add(purchase);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", $"error {Ex}");
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var productoEdit = db.compra.Where(a => a.id == id).FirstOrDefault();
                return View(productoEdit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(compra purchaseEdit)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var oldPurchase = db.compra.Find(purchaseEdit.id);
                    oldPurchase.fecha = purchaseEdit.fecha;
                    oldPurchase.total = purchaseEdit.total;
                    oldPurchase.id_cliente = purchaseEdit.id_cliente;
                    oldPurchase.id_usuario = purchaseEdit.id_usuario;

                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", $"error{Ex}");
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.compra.Find(id));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var compra = db.compra.Find(id);
                    db.compra.Remove(compra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError("", $"error {Ex}");
                return View();
            }
        }
    }
}
