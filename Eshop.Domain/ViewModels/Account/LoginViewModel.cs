﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Domain.ViewModels.Account
{
    public class LoginViewModel
    {

        #region Properties

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(15, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است")]
        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است")]
        public string Password { get; set; }

        #endregion

        public enum LoginResult
        {
            Success,
            UserNotFound,
            UserBanned,
            UserIsNotActive
        }
    }
}
