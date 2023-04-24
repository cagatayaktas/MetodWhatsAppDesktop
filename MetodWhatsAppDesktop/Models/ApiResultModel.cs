using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodWhatsAppDesktop.Models
{
    public class ApiResultModel
    {
        public bool IsSuccess { get; set; }
        public List<ProductModel> Data { get; set; } 
    }
}
