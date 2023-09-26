using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BlogAppMVC.Data;
using BlogAppMVC.Models;
using Newtonsoft.Json;

namespace BlogAppMVC.Controllers
{
    public class EmpController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7239/api");
        HttpClient client;

        public EmpController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()
        {
            List<EmpInfo> emps = new List<EmpInfo>();
            //client.DefaultRequestHeaders.ConnectionClose = true;
            //request.ServicePoint.Expect100Continue = false;
            //System.Net.ServicePointManager.Expect100Continue = false;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/EmpInfoes").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                emps = JsonConvert.DeserializeObject<List<EmpInfo>>(data);
            }

            return View(emps);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmpInfo emps)
        {
            string data = JsonConvert.SerializeObject(emps);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responce = client.PostAsync(client.BaseAddress + "/EmpInfoes", content).Result;
            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmpInfo emps = new EmpInfo();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/EmpInfoes/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                emps = JsonConvert.DeserializeObject<EmpInfo>(data);
            }
            return View(emps);
        }

        [HttpPost]
        public ActionResult Edit(EmpInfo emp)
        {
            try
            {
                string data = JsonConvert.SerializeObject(emp);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/EmpInfoes/" + emp.Id, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating emp.");
                    return View(emp);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View(emp);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                EmpInfo emps = new EmpInfo();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/EmpInfoes/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    emps = JsonConvert.DeserializeObject<EmpInfo>(data);
                }
                return View(emps);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/EmpInfoes/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return View();
                throw;
            }
            return View();
        }
    }
}