using Kavenegar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Application.Services.Interfaces
{
    public interface ISmsSendersService
    {
        public SendResult SendSms(String mobile, string message);
    }
}
