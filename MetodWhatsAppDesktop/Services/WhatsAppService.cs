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
                var folder = Path.Combine(Application.StartupPath, "images").ToLower();

                if (Directory.Exists(folder) == false)
                    Directory.CreateDirectory(folder);

                return folder;
            }
        }

        public static List<WhatsAppMessageModel> GenerateMessages(List<ProductModel> products)
        {
            var result = new List<WhatsAppMessageModel>();
            List<ProductImageModel> productImages = new List<ProductImageModel>();

            #region  Resmi olmayan stokları tespit et ve indir

            var indirilecekStokIds = new List<int>();

            for (int i = 0; i < products.Count; i++)
            {
                if (File.Exists(GetImageFilePath(products[i].StokId)) == false)
                    indirilecekStokIds.Add(products[i].StokId);
            }

            productImages = Services.ApiServices.GetImages(indirilecekStokIds);

            #endregion

            var modelGroups = products.GroupBy(a => a.Model).OrderBy(a => a.Key).ToList();

            foreach (var model in modelGroups)
            {
                var bedenGroup = products.Where(a => a.Model == model.Key).GroupBy(a => a.Beden).OrderBy(a => a.Key).ToList();

                foreach (var beden in bedenGroup)
                {
                    var resimler = products.Where(a => a.Model == model.Key && a.Beden == beden.Key).OrderBy(a => a.Renk).ToList();

                    for (int i = 0; i < resimler.Count; i++)
                    {
                        var imagePath = SaveImageFile(resimler[i], productImages);
                        if (imagePath == "")
                            continue;

                        result.Add(new WhatsAppMessageModel
                        {
                            StokId = resimler[i].StokId,
                            Model = model.Key,
                            Beden = beden.Key,
                            MessageType = Enums.WhatsApp.MessageType.Image,
                            Content = $"{imagePath}",
                            ResimAdi = Path.GetFileName(imagePath)
                        });
                    }
                }
            }

            return result;
        }

        static string GetImageFilePath(int stokId)
        {
            return Path.Combine(ImageFolderPath, $"{stokId}.jpeg");
        }

        public static string SaveImageFile(ProductModel product, List<ProductImageModel> productImages)
        {
            var filePath = "";

            try
            {
                filePath = GetImageFilePath(product.StokId);

                if (File.Exists(filePath))
                    return filePath;

                var image = productImages.FirstOrDefault(a => a.StokId == product.StokId);

                if (image == null)
                    return "";
                else
                    using (var ms = new MemoryStream(image.Resim))
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