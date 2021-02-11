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
    public class ViajerosController : Controller
    {
        public ActionResult Index()
        {


            IEnumerable<ViajerosViewModel> viajeros = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8092/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tm_via_viajeros");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ViajerosViewModel>>();
                    readTask.Wait();

                    viajeros = readTask.Result;
                }
                else
                {


                    viajeros = Enumerable.Empty<ViajerosViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(viajeros);
        }

        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ViajerosViewModel viajero)
        {
            using (var client = new HttpClient())

                try
                {
                    string pruebaValor;
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("icSessionId", icSessionId);

                    string message = System.Text.Json.JsonSerializer.Serialize(viajero);
                    message = message.Insert(1, "\"@type\": \"job\",");
                    byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
                    var content = new ByteArrayContent(messageBytes);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var response = client.PostAsync("http://localhost:8092/api/tm_via_viajeros", content).Result;
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
            return View(viajero);

        }



        public ActionResult Edit(int id)
        {
            ViajerosViewModel viajero = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8092/api/");
                //HTTP GET
                var responseTask = client.GetAsync("tm_via_viajeros?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ViajerosViewModel>();
                    readTask.Wait();

                    viajero = readTask.Result;
                }
            }

            return View(viajero);
        }
        
        [HttpPost]
        public ActionResult Edit(ViajerosViewModel viajero)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8092/api/tm_via_viajeros");

                
                var putTask = client.PutAsJsonAsync<ViajerosViewModel>("tm_via_viajeros?id=" + viajero.id.ToString(), viajero);
                putTask.Wait();

                var result = putTask.Result;
                
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(viajero);

        
       

        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8092/api/tm_via_viajeros");

                
                var deleteTask = client.DeleteAsync("tm_via_viajeros/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}


