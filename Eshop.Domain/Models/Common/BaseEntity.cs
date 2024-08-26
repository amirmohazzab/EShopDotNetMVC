using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Domain.Models.Common
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
