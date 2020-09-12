using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// Added
using System.IO;
using System.Net;
using System.Net.Mail;
using José_finLogistics.Models;
using System.Web.Helpers;

namespace José_finLogistics.Controllers
{
    public class LogisticsController : Controller
    {

        private readonly string senderPass = Constants.senderPass;
        private readonly string senderEmail = Constants.senderEmail;
        private readonly string senderName = Constants.senderName;

        private readonly string mailSubject = "Message from ASP.NET MVC App";



        // GET: Logistics
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }



        //get
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(EmailModel model)
        {
            // If all the parameters required in the form is filled correctly
            if (ModelState.IsValid)
            {
                using (MailMessage client = new MailMessage())
                {
                    client.From = new MailAddress(senderEmail, senderName);
                    client.To.Add(model.UserEmail);
                    client.Subject = mailSubject;
                    client.Body = "You have a message from  <br><b>Fullname:</b>" +
                        model.FullName + "<br><b>Email:</b>" + model.UserEmail + "<br><b>Message</b>" + model.Message;

                    client.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(senderEmail, senderPass);
                        smtp.EnableSsl = true;
                        smtp.Send(client);
                    }
                }// end using
            }

            ViewBag.Feedback = "Message has been sent.";
            // Clear Controls
            ModelState.Clear();

            return View();
        }

        //get
        public ActionResult Services ()
        {
            return View();
        }


 

    }
}