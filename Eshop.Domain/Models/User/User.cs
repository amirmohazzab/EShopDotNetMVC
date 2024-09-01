﻿using Eshop.Domain.Enums.User;
using Eshop.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Domain.Models.User
{
    public class User : BaseEntity<int>
    {

        #region Properties

        [MaxLength(100, ErrorMessage ="تعداد کاراکتر وارد شده بیش از حد مجاز است")]
        public string? FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است")]
        public string? LastName { get; set; }

        [Display(Name ="موبایل")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(15, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است")]
        public string Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است")]
        public string? Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است")]
        public string Password { get; set; }

        [Display(Name = "کد تایید")]
        [MaxLength(12, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است")]
        public string? ConfirmCode { get; set; }

        public UserStatus Status { get; set; }

        #endregion

    }
}
