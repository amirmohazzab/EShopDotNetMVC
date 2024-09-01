using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Application.Generators
{
    public class CodeGenerator
    {
        public static string GenerateCode()
        {
            Random rnd = new Random();
            int rndNumber = rnd.Next(100000, 999999);

            return rndNumber.ToString();
        }
    }
}
