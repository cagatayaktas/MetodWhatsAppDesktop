using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodWhatsAppDesktop.Models
{
    public class ProductModel
    {
        public string Model { get; set; }
        public string Renk { get; set; }
        public string Beden { get; set; }        
        public byte[] Resim { get; set; }
        public int Bakiye { get; set; }
        public int Bakiye2 { get; set; }
    }
}
