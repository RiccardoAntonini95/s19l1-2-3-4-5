using InFornoPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InFornoPizzeria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        [Authorize (Roles = "User")]
        public ActionResult Acquista()
        {
            var db = new ModelDBContext();
            try
            {
               List<Articoli> listaArticoli = db.Articoli.ToList();
                return View(listaArticoli);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        [Authorize (Roles = "User")]
        [ValidateAntiForgeryToken]
        public ActionResult AggiungiAlCarrello(Carrello nuovoArticolo)
        {
            decimal importoIniziale = 0;
            Carrello ArticoloDaAggiungere = new Carrello
            {
                ArticoloId = nuovoArticolo.ArticoloId,
                Nome = nuovoArticolo.Nome,
                Foto = nuovoArticolo.Foto,
                PrezzoVendita = (nuovoArticolo.PrezzoVendita * nuovoArticolo.Quantita),
                Quantita = nuovoArticolo.Quantita,
                Totale = (importoIniziale + nuovoArticolo.PrezzoVendita)
            };
            importoIniziale = ArticoloDaAggiungere.Totale;

            // Recupera la lista del carrello dalla sessione, o crea una nuova lista se non esiste
            List<Carrello> listaCarrello = Session["Carrello"] as List<Carrello>;
            if (listaCarrello == null)
            {
                listaCarrello = new List<Carrello>();
                Session["Carrello"] = listaCarrello;
            }

            // Aggiungi l'articolo al listaCarrello
            listaCarrello.Add(ArticoloDaAggiungere);

            // Aggiorna la lista del listaCarrello nella sessione
            Session["Carrello"] = listaCarrello;
            TempData["Message"] = "Prodotto aggiunto al carrello";

            // Redirect alla pagina del carrello o a qualsiasi altra pagina
            return RedirectToAction("Acquista", "Home");
        }

        [Authorize(Roles = "User")]
        public ActionResult RiepilogoOrdine()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfermaOrdine([Bind(Include = "indirizzo, noteCliente")]string indirizzo, string noteCliente)
        {
            ModelDBContext db = new ModelDBContext();
            var ordine = new Ordini
            {
                UtenteId = Convert.ToInt32(User.Identity.Name),
                StatoOrdine = "In lavorazione",
                DataOrdine = DateTime.Now,
                Totale = (decimal)Session["Totale"]
            };
            db.Ordini.Add(ordine);
            db.SaveChanges();

            int OrdineId = ordine.OrdineId;

            foreach (var dettaglio in Session["Carrello"] as List<Carrello>)
            {
                var dettaglioOrdine = new DettagliOrdine
                {
                    ArticoloId = dettaglio.ArticoloId,
                    OrdineId = OrdineId,
                    Quantita = dettaglio.Quantita,
                    Indirizzo = indirizzo,
                    NoteCliente = noteCliente
                };
                db.DettagliOrdine.Add(dettaglioOrdine);
            }

            db.SaveChanges();

            TempData["Message"] = "Ordine effettuato con successo!";

            return RedirectToAction("Acquista", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SvuotaCarrello()
        {
            Session.Clear();
            TempData["Message"] = "Carrello svuotato!";
            return RedirectToAction("RiepilogoOrdine", "Home");
        }


    }
}
