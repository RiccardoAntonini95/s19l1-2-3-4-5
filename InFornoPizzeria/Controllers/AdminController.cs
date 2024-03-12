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

        public ActionResult AggiungiProdotto()
        {
            return View();
        }

        public ActionResult OrdiniConclusi()
        {
            return View();
        }
    }
}