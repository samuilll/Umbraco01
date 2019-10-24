using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoTest_03.Models;

namespace UmbracoTest_03.Controllers
{
    public class ContactController:SurfaceController
    {
        private const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Contact/";

        public ActionResult RenderForm()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Contact.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(Contact model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                TempData["success"] = true;
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }

        private void SendEmail(Contact model)
        {
            MailMessage message = new MailMessage(model.EmailAddress, model.EmailAddress);
            message.Subject="Labanda";
            message.Body = model.Message;
            SmtpClient client = new SmtpClient("127.0.0.1",25);
            client.Send(message);
        }
    }
}