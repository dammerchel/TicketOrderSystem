using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TicketOrderAPI.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Jesteśmy systemem zamawiania biletow lotniczych. Wyróżniamy dwie role: administrator i klient. Administrator ma możliwość dodawania oraz usuwania lotów i odpowiadających im zestawów biletów, a także generowania listy" +
                "biletów w pliku .xlsx. Klient może zalogować się przy uzyciu zarejestrowanego konta lokalnego lub poprzez aplikację Facebook'a. Po zalogowaniu może zakupić wybrany bilet, który zostaje przypisany do jego konta. ";
        }
    }
}
