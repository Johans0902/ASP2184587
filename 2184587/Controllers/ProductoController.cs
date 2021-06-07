using _2184587.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace _2184587.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.producto.ToList());
            }
        }

        public static string NombreProveedor(int? idProveedor)
        {
            using (var db = new inventarioEntities1())
            {
                return db.proveedor.Find(idProveedor).nombre;
            }
        }

        public ActionResult ListarProveedores()
        {
            using (var db = new inventarioEntities1())
            {
                return PartialView(db.proveedor.ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {

                using (var db = new inventarioEntities1())
                {
                    db.producto.Add(producto);
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

        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var producto = db.producto.Find(id);
                return View(producto);
            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto productoEdit = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(productoEdit);


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
        public ActionResult Edit(producto productoEdit)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    producto oldproduct = db.producto.Find(productoEdit.id);

                    oldproduct.nombre = productoEdit.nombre;
                    oldproduct.cantidad = productoEdit.cantidad;
                    oldproduct.descripcion = productoEdit.descripcion;
                    oldproduct.percio_unitario = productoEdit.percio_unitario;
                    oldproduct.id_proveedor = productoEdit.id_proveedor;


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

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var producto = db.producto.Find(id);
                    db.producto.Remove(producto);
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

        public ActionResult Reporte()
        {
            var db = new inventarioEntities1();
            {
                var query = from tabProveedor in db.proveedor
                            join tabProducto in db.producto on tabProveedor.id equals tabProducto.id_proveedor
                            select new Reporte
                            {
                                nombreProveedor = tabProveedor.nombre,
                                telefonoProveedor = tabProveedor.telefono,
                                direccionProveedor = tabProveedor.direccion,
                                nombreProducto = tabProducto.nombre,
                                precioProducto = tabProducto.percio_unitario
                            };
                return View(query);
            }
        }
    }
}