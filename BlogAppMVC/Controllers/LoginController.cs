using BlogAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogAppMVC.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(LoginInfo loginInfo)
        {
            string connection = ConfigurationManager.ConnectionStrings["BlogAppMVCContext"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            string cmd = "Select EmailId,Password from AdminInfo where EmailId=@Emailid and Password=@Password";
            con.Open();
            SqlCommand command = new SqlCommand(cmd, con);
            command.Parameters.AddWithValue("@EmailId", loginInfo.EmailId);
            command.Parameters.AddWithValue("@Password", loginInfo.Password);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Session["EmailId"] = loginInfo.EmailId.ToString();
                return RedirectToAction("Index", "Emp");
            }

            else
            {
                ViewData["Message"] = "Admin Login Details Failed";
            }
            con.Close();
            return View();
        }
        public ActionResult Employee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Employee(LoginInfo loginInfo)
        {
            string connection = ConfigurationManager.ConnectionStrings["BlogAppMVCContext"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            string cmd = "Select EmailId, PassCode from EmpInfo where EmailId=@Emailid and PassCode=@Password";
            con.Open();
            SqlCommand command = new SqlCommand(cmd, con);
            command.Parameters.AddWithValue("@EmailId", loginInfo.EmailId);
            command.Parameters.AddWithValue("@Password", loginInfo.Password);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Session["EmailId"] = loginInfo.EmailId.ToString();
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ViewData["Message"] = "Employee Login Details Failed";
            }
            con.Close();
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("GuestIndex", "Blog");
        }
    }
}