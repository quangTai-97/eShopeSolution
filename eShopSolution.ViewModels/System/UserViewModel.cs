﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopSolution.ViewModels.System
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        [Display(Name ="Tên")]
        public string FirstName { get; set; }
        [Display(Name = "Họ")]
        public string LasttName { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
