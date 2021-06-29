using _2184587.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Rotativa;

namespace _2184587.Controllers
{
    public class Producto_CompraController : Controller
    {
        // GET: Producto_Compra
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.producto_compra.ToList());
            }
        }

        public ActionResult ListarCompra()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.compra.ToList());
            }
        }
        public ActionResult ListarProducto()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto_compra producto_compra)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {

                using (var db = new inventarioEntities1())
                {
                    db.producto_compra.Add(producto_compra);
                   _ = db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error " + ex);
                return View();

            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var producto_compra = db.producto_compra.Find(id);
                return View(producto_compra);
            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto producto_compraEdit = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(producto_compraEdit);


                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto_compra producto_compraEdit)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto_compra oldproduct = db.producto_compra.Find(producto_compraEdit.id);
                    db.SaveChanges();

                    oldproduct.id_compra = producto_compraEdit.id_compra;
                    oldproduct.id_producto = producto_compraEdit.id_producto;
                    oldproduct.cantidad = producto_compraEdit.cantidad;



                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var producto_compra = db.producto_compra.Find(id);
                    db.producto_compra.Remove(producto_compra);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error " + ex);
                return View();
            }

        }
        public ActionResult Reporte2()
        {
            var db = new inventarioEntities1();
            {
                var query = from tabProducto_Compra in db.producto_compra
                            join tabProducto in db.producto on tabProducto_Compra.id_producto equals tabProducto.id
                            select new Reporte2
                            {
                                id_compra = tabProducto_Compra.id_compra,
                                producto = tabProducto.nombre,
                                Cantidad = tabProducto_Compra.cantidad

                            };
                return View(query);
            }
        }
        public ActionResult ImprimirReporte()
        {
            return new ActionAsPdf("Reporte2") { FileName = "Reporte.pdf" };
        }

    }
}