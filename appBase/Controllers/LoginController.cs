using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appBase.Models;
using AppBase.Models;

namespace AppBase.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult newAcc(User user)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    using(appBaseEntities db = new appBaseEntities())
                    {
                        var dbUser = db.Usuarios.Where(s => s.Correo.Contains(user.correo)).FirstOrDefault();
                        if (dbUser != null)
                        {
                            return Redirect("~/Login/Index#signup");
                        }


                            var usr = new Usuarios();
                        usr.Nombre = user.nombre;
                        usr.Correo = user.correo;
                        usr.Rol = "Estudiante";
                        usr.TipoDocumento = Request.Form["tipoDoc"];
                        usr.Password = user.password;
                        
                        usr.NumeroDocumento = user.numeroDocumento;

                        db.Usuarios.Add(usr);
                        db.SaveChanges();
                    }

                    return Redirect("~/Login/Index");
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Redirect("~/Login/Index#signup");
            }

            return View();
        }

        public ActionResult Login(User user)
        {
            try
            {
                using (appBaseEntities db = new appBaseEntities())
                {
                    var dbUser = db.Usuarios.Where(s => s.Correo.Contains(user.correo)).FirstOrDefault();
                    if (dbUser != null && dbUser.Password == user.password)
                        return Redirect("~/Home/Index");
                }
                return Redirect("~/Login/Index");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Redirect("~/Login/Index");
            }
        }






    }


}