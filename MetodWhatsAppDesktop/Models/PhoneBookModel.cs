﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodWhatsAppDesktop.Models
{
    public class PhoneBookModel
    {
        public PhoneBookModel()
        {
            Name = "";
            UlkeKodu = "";
            Gsm = "";
        }
        public string Name { get; set; }
        public string UlkeKodu { get; set; }
        public string Gsm { get; set; }
    }
}
