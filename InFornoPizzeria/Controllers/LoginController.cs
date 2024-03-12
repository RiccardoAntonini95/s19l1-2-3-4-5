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
            string connectionString = ConfigurationManager.ConnectionStrings["ModelDbContext"].ToString();
            var conn = new SqlConnection(connectionString);
            if (ModelState.IsValid)
            {
                try
                {
                    conn.Open();
                    var command = new SqlCommand("SELECT * FROM Utenti WHERE Username = @username AND Pass = @password", conn);
                    command.Parameters.AddWithValue("@username", utente.Username);
                    command.Parameters.AddWithValue("@password", utente.Password);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        FormsAuthentication.SetAuthCookie(reader["UtenteId"].ToString(), true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("error");
                    }
                }
                catch (Exception ex)
                {
                    return View("error");
                }
                finally { conn.Close(); }
            }
            return View("LoggedIn");
        }
    }
}