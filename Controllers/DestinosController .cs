using LeninTravel.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Web.Mvc;

namespace LeninTravel.Controllers
{
    public class DestinosController : Controller
    {

    public ActionResult Index()
        {
            //consume Web API Get method here.. 

            return View();
        }

        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DestinosViewModel destino)
        {
            using (var client = new HttpClient())

                try
                {
                    string pruebaValor;
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("icSessionId", icSessionId);

                    string message = System.Text.Json.JsonSerializer.Serialize(destino);
                    message = message.Insert(1, "\"@type\": \"job\",");
                    byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
                    var content = new ByteArrayContent(messageBytes);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var response = client.PostAsync("http://localhost:8092/api/tm_des_destinos", content).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        pruebaValor = response.RequestMessage.ToString();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            return View(destino);

        }
    }
}
