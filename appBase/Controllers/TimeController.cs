using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using appBase.Models;
using Newtonsoft.Json;




namespace appBase.Controllers
{
    public class TimeController : Controller
    {
        // GET: Time
        public string getTime(int id)
        {
            try
            {
                var autorizedID = new List<int> { 1, 2, 3 };
                if (!autorizedID.Exists(X => X == id)) throw new Exception("ID no autorizado");
                return DateTime.Now.AddHours(-5).ToString("yyyy-MM-dd HH:mm:ss");
                
            }
            catch (Exception ex)
            {
                return "ERROR" + ex.Message;
            }
        }
        [HttpPost]
        public string setTime(int id, string date)
        {
            try
            {
                string Hora_Result = date;

                using (appBaseEntities DataBase = new appBaseEntities())
                {
                    var Hora = new tablaTiempoDB();
                    Hora.hora = TimeSpan.Parse(Hora_Result);
                    DataBase.tablaTiempoDB.Add(Hora);
                    DataBase.SaveChanges();
                }
                return "Hora registrada correctamente";
            }
            catch (Exception ex)
            {
                return "ERROR" + ex.Message;
            }
        }

        public ActionResult View_Hora()
        {
            try
            {

                var Tem_List = new Lista_Horas();
                Tem_List.List_Horas = new List<Date_ID>();
                using (appBaseEntities DataBase = new appBaseEntities())
                {
                    foreach (var Elementos in DataBase.tablaTiempoDB)
                    {
                        Tem_List.List_Horas.Add(new Date_ID
                        {
                            ID = Elementos.ID,
                            Hora = Elementos.hora
                        });
                    }
                }
                return View(Tem_List);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            return Redirect("~/Home/Index");

        }
        public ActionResult Delete(int id)
        {
            try
            {
                using (appBaseEntities db = new appBaseEntities())
                {
                    var dbUser = db.tablaTiempoDB.Find(id);
                    db.tablaTiempoDB.Remove(dbUser);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Redirect("~/Time/View_Hora");
            }
            return Redirect("~/Time/View_Hora");
        }
    }
}