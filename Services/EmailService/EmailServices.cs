using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ticket_support_api.Interfaces;
using ticket_support_api.Models;
using ticket_support_api.Services.DatabaseAccess;

namespace ticket_support_api.Services.EmailService
{
    public class EmailServices : IEmails
    {
        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<EmailMessageModel> CollectionEmailsHistory;

        public EmailServices()
        {
            CollectionEmailsHistory = accessDB.database.GetCollection<EmailMessageModel>("EmailsHistory");
        }
        public async Task<List<EmailMessageModel>> GetEmails()
        {
            return await CollectionEmailsHistory.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task SendEmail(EmailMessageModel message, string email, string pass)
        {
            try
            {
                await Task.Run(() =>
                {
                    MailMessage notificacion = new MailMessage();
                    SmtpClient servicioSMTP = new SmtpClient();
                    notificacion.From = new MailAddress(email, "Technical Support Admin");
                    notificacion.To.Add(new MailAddress(message.EmailAddress!));
                    notificacion.Subject = message.Subject;
                    notificacion.IsBodyHtml = true;
                    notificacion.Body =
                    "<div style='border-style: solid; border-color: black';>" +
                    "<h2 style='margin-left: 1cm;'>Asunto: " + message.Subject + "</h2>" +
                    "<hr style='margin-left: 0.5cm;'> " +
                    "<h2 style='margin-left: 0.5cm;'>Details:</h2>" +
                    "<h3> " + message.Message + "</h3>";
                    servicioSMTP.Port = 587;
                    servicioSMTP.Host = "smtp.gmail.com";
                    servicioSMTP.EnableSsl = true;
                    servicioSMTP.UseDefaultCredentials = false;
                    servicioSMTP.Credentials = new NetworkCredential(email, pass);
                    servicioSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                    servicioSMTP.Send(notificacion);

                });

                message.DateSended = DateTime.Now;
                await CollectionEmailsHistory.InsertOneAsync(message);
            }
            catch (Exception error)
            {

                throw error.InnerException!;
            }
        }
    }
}
