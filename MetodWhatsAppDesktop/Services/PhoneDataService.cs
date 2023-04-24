using MetodWhatsAppDesktop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MetodWhatsAppDesktop.Services
{
    public class PhoneDataService
    {
        static string PhoneDataPath
        {
            get
            {
                return Path.Combine(Application.StartupPath, "PhoneData.xml");
            }
        }

        public static List<PhoneBookModel> PhoneData;

        public static void Savedata()
        {
            try
            {
                if (PhoneData == null)
                    PhoneData = new List<PhoneBookModel>();

                XmlSerializer ser = new XmlSerializer(typeof(List<PhoneBookModel>));
                StringWriter sw = new StringWriter();
                ser.Serialize(sw, PhoneData);

                XDocument xDoc = XDocument.Parse(sw.ToString());

                xDoc.Save(PhoneDataPath);
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
            }
        }

        public static void LoadData()
        {
            try
            {
                if (!File.Exists(PhoneDataPath))
                {
                    if (PhoneData == null)
                        PhoneData = new List<PhoneBookModel>();

                    return;
                }

                XmlSerializer ser = new XmlSerializer(typeof(List<PhoneBookModel>));

                XDocument xDoc = XDocument.Load(PhoneDataPath);
                StringReader sr = new StringReader(xDoc.ToString());

                XmlReader XRdr = new XmlTextReader(sr);

                if (ser.CanDeserialize(XRdr))
                    PhoneData = (List<PhoneBookModel>)ser.Deserialize(XRdr);
                else
                    PhoneData = new List<PhoneBookModel>();

            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                PhoneData = new List<PhoneBookModel>();
            }
        }
    }
}
