using InFornoPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InFornoPizzeria.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize (Roles = "Admin")]
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

        [Authorize (Roles = "User")]
        public ActionResult RiepilogoOrdine()
        {
            return View();
        }

    }
}
