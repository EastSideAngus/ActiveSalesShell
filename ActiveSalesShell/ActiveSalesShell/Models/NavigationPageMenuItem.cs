using ActiveSalesShell.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveSalesShell.Models
{

    public class NavigationPageMenuItem
    {
        public NavigationPageMenuItem()
        {
            TargetType = typeof(NavigationPageDetail);

            // TargetType = typeof(LoginPage); // <-- Sets the default page... maybe ?
            // TargetType = typeof(ActiveSalesReportPage);
            // ORIGINAL: TargetType = typeof(NavigationPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}