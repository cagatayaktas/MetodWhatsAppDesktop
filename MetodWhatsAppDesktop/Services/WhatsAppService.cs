using MetodWhatsAppDesktop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodWhatsAppDesktop.Services
{
    public class WhatsAppService
    {
        static string ImageFolderPath
        {
            get
            {
                var folder = Path.Combine(Application.StartupPath, "Images");

                if (Directory.Exists(folder) == false)
                    Directory.CreateDirectory(folder);

                return folder;
            }
        }

        public static List<WhatsAppMessageModel> GenerateMessages(List<ProductModel> products)
        {
            var result = new List<WhatsAppMessageModel>();

            var modelGroups = products.GroupBy(a => a.Model).OrderBy(a => a.Key).ToList();

            int index = 0;

            foreach (var model in modelGroups)
            {
                var bedenGroup = products.Where(a => a.Model == model.Key).GroupBy(a => a.Beden).OrderBy(a => a.Key).ToList();

                foreach (var beden in bedenGroup)
                {
                    //Model (Beden)
                    result.Add(new WhatsAppMessageModel { Index = index++, MessageType = Enums.WhatsApp.MessageType.Text, Content = $"{model.Key} ({beden.Key})" });

                    var resimler = products.Where(a => a.Model == model.Key && a.Beden == beden.Key).OrderBy(a => a.Renk).ToList();

                    for (int i = 0; i < resimler.Count; i++)
                    {
                        var imagePath = SaveImageFile(resimler[i]);
                        if (imagePath == "")
                            continue;

                        result.Add(new WhatsAppMessageModel { Index = index++, MessageType = Enums.WhatsApp.MessageType.Image, Content = $"{imagePath}" });
                    }
                }
            }

            return result;
        }

        public static string SaveImageFile(ProductModel product)
        {
            var filePath = "";

            try
            {
                var fileName = $"{product.Model}_{product.Beden}_{product.Renk}.jpeg".Replace(" ", "").ToLower();

                filePath = Path.Combine(ImageFolderPath, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (var ms = new MemoryStream(product.Resim))
                {
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
            }

            if (File.Exists(filePath))
                return filePath;
            else
                return "";
        }
    }
}
