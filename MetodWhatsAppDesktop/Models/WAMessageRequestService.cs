using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodWhatsAppDesktop.Models
{
    public class WAMessageRequestService
    {
        public List<string> Recipients { get; set; }
        public List<int> StokIds { get; set; }

        public WAMessageRequestService()
        {
            Recipients = new List<string>();
            StokIds = new List<int>();
        }
    }
}
