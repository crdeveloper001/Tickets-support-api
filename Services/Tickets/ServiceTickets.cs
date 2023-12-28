using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ticket_support_api.Interfaces;
using ticket_support_api.Models;
using ticket_support_api.Services.DatabaseAccess;

namespace ticket_support_api.Services.Tickets
{
    public class ServiceTickets : ITickets
    {

        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<TicketRequestModel> CollectionTickets;

        public ServiceTickets()
        {
            CollectionTickets = accessDB.database.GetCollection<TicketRequestModel>("CurrentTickets");
        }

        public async Task CreateTicket(TicketRequestModel newTickets,string emailSys,string passSys)
        {
            await CollectionTickets.InsertOneAsync(newTickets);

            await Task.Run(() =>
            {

                MailMessage notificacion = new MailMessage();
                SmtpClient servicioSMTP = new SmtpClient();
                notificacion.From = new MailAddress(emailSys, "Technical Support");
                notificacion.To.Add(new MailAddress(newTickets.Email!));
                notificacion.Subject = "Technical Support Request Notification: " + newTickets.TicketNumber;
                notificacion.IsBodyHtml = true;
                notificacion.Body =
                "<div style='border-style: solid; border-color: black';>" +
                "<h2 style='margin-left: 1cm;'>Your request for technical support has been received.</h2>" +
                "<hr style='margin-left: 0.5cm;'> " +
                "<h2 style='margin-left: 0.5cm;'>Details:</h2>" +
                "<ul>" +
                "<li> Ticket Number:" + newTickets.TicketNumber + "</li>" +
                "<hr>" +
                "<li> Request Details:" + "<br>" + newTickets.Details + "</li>" +
                "</ul>" +
                "<h3>PLEASE DO NOT REPLY THIS AUTOMATIC EMAIL!</h3>"+
                "</div>";
                servicioSMTP.Port = 587;
                servicioSMTP.Host = "smtp.gmail.com";
                servicioSMTP.EnableSsl = true;
                servicioSMTP.UseDefaultCredentials = false;
                servicioSMTP.Credentials = new NetworkCredential(emailSys, passSys);
                servicioSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                servicioSMTP.Send(notificacion);

            });
        }
      

        public async Task DeleteTicket(string id)
        {
            var FiltroConsulta = Builders<TicketRequestModel>.Filter.Eq(X => X._id, id);

            await CollectionTickets.DeleteOneAsync(FiltroConsulta);
        }

        public async Task<List<TicketRequestModel>> GetCurrentTickets()
        {
            return await CollectionTickets.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task UpdateTicket(string id, TicketRequestModel updateTicket)
        {
            var FiltroConsulta = Builders<TicketRequestModel>.Filter.Eq(X => X._id, updateTicket._id);

            await CollectionTickets.ReplaceOneAsync(FiltroConsulta, updateTicket);
        }

        public async Task<List<TicketRequestModel>> GetUserProfileTickets(string emailClient)
        {
            var FiltroConsulta = Builders<TicketRequestModel>.Filter.Eq("Email", emailClient);

            return await CollectionTickets.FindAsync(FiltroConsulta).Result.ToListAsync();
        }
    }
}
