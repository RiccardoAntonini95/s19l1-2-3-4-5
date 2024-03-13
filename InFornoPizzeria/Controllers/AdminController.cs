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

        [HttpGet]
        public ActionResult OrdiniConclusi()
        {
            var db = new ModelDBContext();
            try
            {
                var ordini = db.Ordini.Include("DettagliOrdine").Include("Utenti").ToList();
                return View(ordini);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult EvadiOrdine(int ordineId)
        {
            var db = new ModelDBContext();
            var ordineDaEvadere = db.Ordini.Find(ordineId);
            ordineDaEvadere.StatoOrdine = "Ordine evaso";
            db.Entry(ordineDaEvadere).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("OrdiniConclusi", "Admin");
        }
    }
}