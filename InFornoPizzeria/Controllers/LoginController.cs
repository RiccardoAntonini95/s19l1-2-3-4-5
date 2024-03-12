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
        public ActionResult Index(Utenti utente)
        {
            var db = new ModelDBContext();
            if (ModelState.IsValid)
            {
                try
                {
                    var utenteLoggato = db.Utenti.Where(model => model.Username == utente.Username && model.Password == utente.Password).FirstOrDefault();
                    if(utenteLoggato != null)
                    {
                        FormsAuthentication.SetAuthCookie(utenteLoggato.Username, true);
                        return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home"); //sei già loggato, torna alla home
            }
            return View();
        }

        [HttpPost]
        public ActionResult Registrati(Utenti newUser)
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
            return View() ;
        }
    }
}