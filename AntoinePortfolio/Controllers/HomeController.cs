using AntoinePortfolio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AntoinePortfolio.Controllers
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

        public ActionResult Sent()
        {
           

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From: <bold>{0}</bold> ({1})</p><p> Message:</p><p>{2}</p> ";
                    var from = "MyPortfolio<jantoineprojects@gmail.com>";
                    model.Body = "This is a message from your Event App. The name and the email of the contacting person is above.";
                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = "Portfolio Contact Email",
                        Body = string.Format(body, model.FromName, model.FromEmail, model.Body),
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);
                    return RedirectToAction("Sent");
                }
                
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }

        public async Task<ActionResult> ContactHtml(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var from = model.FromEmail;
                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = model.Subject,
                        Body = $"<strong>{model.FromName}</strong> left you a message: {model.Body}. The user's email is <strong>{model.FromEmail}</strong>",
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);
                    return Redirect("~/MessageSent.html#about");
                    //return File("~/ContactEmail1.html", "text/html");
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.FromResult(0);
                }
            }
            return File("~/index.html", "text/html");
        }

        [HttpPost]
        public async Task ContactHtmlAjax(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var from = model.FromEmail;
                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = model.Subject,
                        Body = $"<strong>{model.FromName}</strong> left message: {model.Body}. The user's email is <strong>{model.FromEmail}</strong>",
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);
                    //return RedirectToAction("Sent");
                    //return File("~/ContactEmail1.html", "text/html");
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.FromResult(0);
                }
            }
            //return File("~/ContactEmail1.html", "text/html");
        }
    }
}