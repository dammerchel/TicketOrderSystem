using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TicketOrderAPI.Model;

namespace TicketOrderAPI.Pages.Flights
{
    [Authorize(Roles = "Administrator")]
    public class ExcelExportModel : PageModel                       
    {
        private IHostingEnvironment _hostingEnvironment;

        public ExcelExportModel(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;            
        }       

        public async Task<IActionResult> OnPostExport()             //Metoda umo¿liwiajaca wygenerowanie pliku xlsx zawierajacego wykaz wszystkich biletów zapisanych w bazie danych Ticket.
        {          
            
            var optionsBuilder = new DbContextOptionsBuilder<TicketContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Ticket-1;Trusted_Connection=True;MultipleActiveResultSets=true");
            TicketContext services = new TicketContext(optionsBuilder.Options);

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"Flight.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("FlightTickets");
                IRow row = excelSheet.CreateRow(0);              

                row.CreateCell(0).SetCellValue("Typ biletu");
                row.CreateCell(1).SetCellValue("Cena biletu");
                row.CreateCell(2).SetCellValue("Opis biletu");
                row.CreateCell(3).SetCellValue("ID lotu");
                row.CreateCell(4).SetCellValue("ID klienta");
                row.CreateCell(5).SetCellValue("Numer siedzenia");                
                var TableRow = 1;
                foreach (var item in services.Ticket)
                {
                    
                        row = excelSheet.CreateRow(TableRow);
                        row.CreateCell(0).SetCellValue(item.TicketName.ToString());
                        row.CreateCell(1).SetCellValue(item.TicketPrice.ToString());
                        row.CreateCell(2).SetCellValue(item.TicketDescription);
                        row.CreateCell(3).SetCellValue(item.FlightID);
                        row.CreateCell(4).SetCellValue(item.ClientID);
                        row.CreateCell(5).SetCellValue(item.TicketSeat);
                        TableRow++;
                    
                }
                
                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
    }
}