using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using BlogAppMVC.Models;
using Newtonsoft.Json;

namespace BlogAppMVC.Controllers
{
    public class AdminController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7239/api");
        HttpClient client;

        public AdminController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()
        {
            List<AdminInfo> admins = new List<AdminInfo>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/AdminInfoes").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                admins = JsonConvert.DeserializeObject<List<AdminInfo>>(data);
            }
            return View(admins);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdminInfo admins)
        {
            string data = JsonConvert.SerializeObject(admins);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responce = client.PostAsync(client.BaseAddress + "/AdminInfoes", content).Result;
            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AdminInfo admins = new AdminInfo();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/AdminInfoes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                admins = JsonConvert.DeserializeObject<AdminInfo>(data);
            }
            return View(admins);
        }

        [HttpPost]
        public ActionResult Edit(AdminInfo admin)
        {
            try
            {
                string data = JsonConvert.SerializeObject(admin);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/AdminInfoes/" + admin.Id, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating admin.");
                    return View(admin);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View(admin);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            AdminInfo admins = new AdminInfo();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/AdminInfoes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                admins = JsonConvert.DeserializeObject<AdminInfo>(data);
            }
            return View(admins);
        }

        [HttpPost]
        public ActionResult Delete(AdminInfo admins)
        {
            string data = JsonConvert.SerializeObject(admins);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/AdminInfoes/" + admins.Id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
