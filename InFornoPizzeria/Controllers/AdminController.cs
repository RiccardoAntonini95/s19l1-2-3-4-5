using InFornoPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InFornoPizzeria.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AggiungiArticolo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AggiungiArticolo([Bind(Include = "Nome, Foto, PrezzoVendita, TempoConsegna, Ingredienti")]Articoli nuovoArticolo)
        {
            var db = new ModelDBContext();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Articoli.Add(nuovoArticolo);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return View("error");
                }
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        

        public ActionResult OrdiniConclusi()
        {
            return View();
        }
    }
}