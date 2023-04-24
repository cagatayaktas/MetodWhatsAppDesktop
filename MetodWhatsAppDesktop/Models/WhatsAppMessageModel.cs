using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodWhatsAppDesktop.Models
{
    public class WhatsAppMessageModel
    {
        public Enums.WhatsApp.MessageType MessageType { get; set; }
        public string Content { get; set; } 
    }
}
