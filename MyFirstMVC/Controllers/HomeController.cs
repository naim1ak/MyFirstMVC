using MyFirstMVC.Data;
using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Aşağıdaki formu doldurarak bizimle iletişim kurabilirsiniz.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {                
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                    mailMessage.From = new System.Net.Mail.MailAddress("mdemirci01@gmail.com", "Murat Demirci");
                    mailMessage.Subject = "İletişim Formu: " + model.FirstName + " " + model.LastName;

                    mailMessage.To.Add("mdemirci01@gmail.com");

                    string body;
                    body = "Ad Soyad: " + model.FirstName + " " + model.LastName + "<br />";
                    body += "Telefon: " + model.Telephone + "<br />";
                    body += "E-posta: " + model.Email + "<br />";
                    body += "Mesaj: " + model.Message + "<br />";
                    body += "Tarih: " + DateTime.Now.ToString("dd MMMM yyyy HH:mm") + "<br />";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = body;

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new System.Net.NetworkCredential("mdemirci01@gmail.com", "hesabinmailsifresi");
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);
                ViewBag.Message = "Mesajınız gönderildi. Teşekkür ederiz.";
                } catch(Exception ex)
                {
                    ViewBag.Error = "Form gönderimi başarısız oldu. Lütfen daha sonra tekrar deneyiniz.";
                }
            }
            return View(model);
        }

        public ActionResult Projects()
        {
            using (var db = new ApplicationDbContext())
            {
                var projects = db.Projects.ToList();
                return View(projects);
            }            
        }

        public ActionResult Kvkk()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}