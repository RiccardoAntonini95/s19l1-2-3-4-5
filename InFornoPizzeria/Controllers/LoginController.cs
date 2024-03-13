using InFornoPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InFornoPizzeria.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home"); //sei già loggato, torna alla home
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Username, Password ")]Utenti utente)
        {
            var db = new ModelDBContext();
            if (ModelState.IsValid)
            {
                try
                {
                    //query select che ottiene il record giusto 
                    var utenteLoggato = db.Utenti.Where(model => model.Username == utente.Username && model.Password == utente.Password).FirstOrDefault();
                    if(utenteLoggato != null)
                    {
                        FormsAuthentication.SetAuthCookie(utenteLoggato.UtenteId.ToString(), true);//salvo l'id ottenuto dalla select e lo passo al rolemanager
                        if(utenteLoggato.Role == "Admin")
                        {
                            return RedirectToAction("Index", "Admin"); //qua pagina admin che avrà nel controller Authorize admin
                        }
                        return RedirectToAction("Index", "Home"); //qua pagina utente per aggiungere prodotti 
                    }
                }
                catch (Exception ex)
                {
                    return View("error");
                }
            }
            return View("Index");
        }
        public ActionResult Registrati()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrati([Bind(Include = "Username, Password")] Utenti newUser)
        {
            var db = new ModelDBContext();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Utenti.Add(newUser);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return View("error");
                }
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            //SVUOTA LA SESSION SE ESISTE
            FormsAuthentication.SignOut();
            TempData["Message"] = "Logout effettuato con successo.";
            return RedirectToAction("Index", "Home");
        }
    }
}