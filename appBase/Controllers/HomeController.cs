using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appBase.Models;
using AppBase.Models;
using Microsoft.Ajax.Utilities;

namespace AppBase.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult tableUser()
        {
            var listUsers = new ListUserView();
            listUsers.listUser = new List<User>();

            using(appBaseEntities db = new appBaseEntities())
            {
                foreach (var a in db.Usuarios)
                {
                    listUsers.listUser.Add(new User
                    {
                        id = a.ID,
                        nombre = a.Nombre,
                        correo = a.Correo,
                        tipoDocumento = a.TipoDocumento,
                        numeroDocumento = a.NumeroDocumento,
                        rol = a.Rol
                    });
                }
            }

            return View(listUsers);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                User user = new User();
                using (appBaseEntities db = new appBaseEntities())
                {
                    var dbUser = db.Usuarios.Find(id);
                    user.nombre = dbUser.Nombre;
                    user.correo = dbUser.Correo;
                    user.tipoDocumento = dbUser.TipoDocumento;
                    user.numeroDocumento = dbUser.NumeroDocumento;
                    user.rol = dbUser.Rol;
                }
                return View(user);

            }catch(Exception ex)
            {
                return Redirect("~/Home/tableUser");
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                using (appBaseEntities db = new appBaseEntities())
                {
                    var usr = db.Usuarios.Find(user.id);
                    usr.Nombre = user.nombre;
                    usr.Correo = user.correo;
                    usr.Rol = "Estudiante";
                    usr.TipoDocumento = user.tipoDocumento;
                    usr.NumeroDocumento = user.numeroDocumento;
                    usr.Rol = user.rol;
                    db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Redirect("~/Home/tableUser");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Redirect("~/Home/Edit");
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (appBaseEntities db = new appBaseEntities())
                {
                    var dbUser = db.Usuarios.Find(id);
                    db.Usuarios.Remove(dbUser);
                    db.SaveChanges();
                }

            }
            catch(Exception ex)
            {
                return Redirect("~/Home/tableUser");
                throw new Exception(ex.Message);
            }
            return Redirect("~/Home/tableUser");
        }
    }



}