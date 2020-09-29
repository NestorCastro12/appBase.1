using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;




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
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
                var respuesta = new { Resultado = "OK", Date = date, Id = id };
                return respuesta.Id + " " + respuesta.Date + " " + respuesta.Resultado;
            }
            catch (Exception ex)
            {
                return "ERROR" + ex.Message;
            }
        }
    }
}