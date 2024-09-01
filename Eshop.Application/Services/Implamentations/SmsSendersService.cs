using Eshop.Application.Services.Interfaces;
using Eshop.Application.Statics;
using Kavenegar;
using Kavenegar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Application.Services.Implamentations
{
    public class SmsSendersService : ISmsSendersService
    {
        private readonly KavenegarApi kavenegarApi;

        public SmsSendersService()
        {
            string apiKey = KavenegarStatics.ApiKey;
            kavenegarApi = new KavenegarApi(apiKey);
        }


        public SendResult SendSms(string mobile, string message)
        {
            return kavenegarApi.Send(KavenegarStatics.Sender, mobile, message);
        }
    }
}
