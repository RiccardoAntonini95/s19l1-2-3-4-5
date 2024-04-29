using InFornoPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace InFornoPizzeria.Controllers
{
    //[Authorize (Roles = "Admin")]
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
                var ordini = db.Ordini.Include("DettagliOrdine").Include("Utenti").OrderBy(o => o.StatoOrdine).ToList();
                return View(ordini);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
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

        public ActionResult GetOrdiniEvasi()
        {
            var db = new ModelDBContext();
            int count = db.Ordini.Count(o => o.StatoOrdine == "Ordine evaso");
            var result = new { count = count };
            return Json(result);
        }

        public ActionResult GetIncassiTotaliPerData(DateTime dataDaCercare)
        {
                var dbContext = new ModelDBContext();
            decimal sommaTotali = dbContext.Ordini
                                           .Where(o => o.DataOrdine.Year == dataDaCercare.Year &&
                                                       o.DataOrdine.Month == dataDaCercare.Month &&
                                                       o.DataOrdine.Day == dataDaCercare.Day)
                                           .Select(o => (decimal?)o.Totale) // Cast a decimal? per gestire i valori nulli
                                           .DefaultIfEmpty(0) // Imposta il valore predefinito a 0 se non ci sono elementi
                                           .Sum() ?? 0;
                var result = new { sommaTotali = sommaTotali };
                return Json(result, JsonRequestBehavior.AllowGet);
                //SELECT SUM(Totale)  FROM Ordini WHERE CONVERT(date, DataOrdine) = '2024-03-14'; <----- query da effetuare 

        }
    }
}