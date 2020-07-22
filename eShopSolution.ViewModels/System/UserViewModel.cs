using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }

        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
