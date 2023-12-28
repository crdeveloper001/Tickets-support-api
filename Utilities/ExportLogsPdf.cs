using System.Text;
using ticket_support_api.Services.LogsTickets;

namespace ticket_support_api.Utilities
{
    public class ExportLogsPdf
    {
      

        public static string CreateLogsPdf()
        {
             LogsTickets LogsTicketsService = new LogsTickets();

            var logs = LogsTicketsService.GetCurrentTicketsLogs();

            var TemplateReport = new StringBuilder();

            TemplateReport.Append(@"
             <style>
        #MessageContainer {
            border-style: solid;
            border-color: black;

        }

        #ContainerSolution {
            margin-left: 50px;
            margin-right: 50px;
        }

        hr {
            color: black;
        }

        table {
            width: 100%;
            border: 1px solid black;
            border-color: black;
            border-style: solid;
            border-collapse: collapse;
        }

        td {
            text-align: center;
            border: 1px solid black;
        }

        h2 {
            margin-left: 20px;
        }

        .button {
            display: block;
            width: 500px;
            height: 25px;
            background: #29b330;
            padding: 10px;
            text-align: center;
            border-radius: 5px;
            color: white;
            font-weight: bold;
            line-height: 25px;
        }
    </style>

          <h2>CLOSED TICKET HISTORY REPORT</h2>
        <hr>

            <div id='ContainerSolution'>

                <table>

                <thead>
                    <tr>
                        <th> TICKET NUMBER </th>
                        <th> CLIENT NAME </th>
                        <th> TYPE OF SUPPORT </th>
                        <th> DETAILS ABOUT THS TICKET </th>
                        <th> SOLUTION DETAILS </th>
                    </tr>
                </thead>
               
            ");

            foreach (var item in logs.Result)
            {
                TemplateReport.AppendFormat(@"
                    <tbody>
                        <td>{0}</td>
                        <td>{1}</td>
                        <td>{2}</td>
                        <td>{3}</td>
                        <td>{4}</td>
                    </tbody>
 
                    ",item.TicketNumber,item.Name,item.TypeRequest,item.Details,item.SolutionDetails);
            }

            TemplateReport.Append(@"
                    </table>
                </div>");

            return TemplateReport.ToString();
        }
    }
}
