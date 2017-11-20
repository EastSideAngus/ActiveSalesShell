using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveSalesShell.Models
{
    public class UserModel
    {
        public int Id { get; set; } // user_id
        public int ClubId { get; set; } // club_id
        public string FirstName { get; set; } // user_first_name
        public string LastName { get; set; } // user_last_name
        public string EmailAddress { get; set; } // user_email


    }
}
