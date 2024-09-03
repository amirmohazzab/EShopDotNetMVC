using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Domain.Shared
{
    public class SuccessMessages
    {
        #region Account
        public static string RegisterSuccessfullyDone = "ثبت نام شما با موفقیت انجام شد";
        public static string ForgotPasswordSuccessfullyDone = "کد تایید برای شماره موبایل شما ارسال شد";
        public static string ResetPasswordSuccessfullyDone = "تغییر کلمه عبور شما با موفقیت انجام شد";
        #endregion

        #region User
        public static string UpdateProfileSuccessfullyDone = "ویرایش حساب کاربری شما با موفقیت انجام شد";

        #endregion

    }

    public class ErrorMessages
    {
        public static string OperationFailed = "خطایی رخ داده است لطفا بعدا تلاش کنید";

        public static string MobileDuplicated = "کاربری با این شماره موبایل ثیت نام کرده است";

        #region User
        public static string UserBanned = "حساب کاربری شما مسدود شده است";

        public static string UserIsNotActive = "حساب کاربری شما غیر فعال است";

        public static string UserNotFound = "کاربر پیدا نشد";
        #endregion
    }


}
