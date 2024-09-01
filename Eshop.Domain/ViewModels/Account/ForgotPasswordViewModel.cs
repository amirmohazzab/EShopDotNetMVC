﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Domain.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        #region Properties

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(15, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است")]
        public string Mobile { get; set; }

        #endregion

        public enum ForgotPasswordResult
        {
            Success,
            MobileNotFound,
            Error
        }
    }
}
