using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ticket_support_api.Interfaces;
using ticket_support_api.Models;
using ticket_support_api.Services.DatabaseAccess;

namespace ticket_support_api.Services.LogsTickets
{
    public class LogsTickets : ILogsTickets
    {
        internal DatabaseConnection connection = new DatabaseConnection();

        private readonly IMongoCollection<LogTicketModel> CollectionLogsTickets;

     

        public LogsTickets()
        {
            CollectionLogsTickets = connection.database.GetCollection<LogTicketModel>("TicketsHistoryLog");
        }

        public async Task CreateLogTicket(LogTicketModel newLogTicket)
        {
            await CollectionLogsTickets.InsertOneAsync(newLogTicket);

            await Task.Run(() =>
            {

                MailMessage notificacion = new MailMessage();
                SmtpClient servicioSMTP = new SmtpClient();
                notificacion.From = new MailAddress("cgonzalez@mbs.ed.cr", "CURRENT TICKET STATUS");
                notificacion.To.Add(new MailAddress(newLogTicket.EmailToNotifitication!));
                notificacion.Subject = "THE TICKET " + newLogTicket.TicketNumber + "HAS BEEN CLOSED BY THE ADMINISTRATOR";
                notificacion.IsBodyHtml = true;

                notificacion.Body = 

        "<div style='border-style: solid;border-color: black;'>" +


         "<h2 style='margin-left: 20px;'> THIS TICKET " + newLogTicket.TicketNumber +" HAS BEEN CLOSED FOR: "+newLogTicket.Name+"</h2>"+
        "<hr style='color: black;'>" +

        "<div style='margin-left: 50px; margin-right: 50px;'>" +

            "<table style='width: 100%;border: 1px solid black;border-color: black;border-style: solid;border-collapse: collapse;'>" +

                "<thead>"+
                    "<tr>"+
                        "<th>TYPE OF SUPPORT</th>"+
                        "<th>DETAILS ABOUT THE PROBLEM</th>"+
                        "<th>SOLUTION REGISTERED</th>"+
                    "</tr>"+
                "</thead>"+
                "<tbody>"+
                    "<tr>"+
                        "<td style='text-align: center; border: 1px solid black;'>" + newLogTicket.TypeRequest+"</td>"+
                        "<td style='text-align: center; border: 1px solid black;'>+" + newLogTicket.Details+"</td>"+
                        "<td style='text-align: center; border: 1px solid black;'>"+newLogTicket.SolutionDetails+"</td>"+
                    "</tr>"+
                "</tbody>"+

           "</table>"+
        "</div>"+
        "<hr>"+
       " <a style='" +
       "display: block; " +
       "width: 500px; " +
       "height: 25px; " +
       "background: #29b330; " +
       "padding: 10px; " +
       "text-align: center; " +
       "border-radius: 5px; " +
       "color: white; " +
       "font-weight: bold; " +
       "line-height: 25px; ' target='blank'" + "href='https://ticketsmbssupport.netlify.app'>"+"IF YOU WANT TO SEND A NEW TICKET, CLICK HERE </ a > "+
    "</div>";

                servicioSMTP.Port = 587;
                servicioSMTP.Host = "smtp.gmail.com";
                servicioSMTP.EnableSsl = true;
                servicioSMTP.UseDefaultCredentials = false;
                servicioSMTP.Credentials = new NetworkCredential("cgonzalez@mbs.ed.cr", "IT.s0p0rt3.MBS1");
                servicioSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                servicioSMTP.Send(notificacion);

            });
        }

        public async Task<List<LogTicketModel>> GetCurrentTicketsLogs()
        {
            return await CollectionLogsTickets.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}
