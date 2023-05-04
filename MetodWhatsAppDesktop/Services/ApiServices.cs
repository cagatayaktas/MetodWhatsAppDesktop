using MetodWhatsAppDesktop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MetodWhatsAppDesktop.Services
{
    public class ApiServices
    {
        public static List<ProductModel> GetProducts()
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.bayramoglu.in/api/urun");
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("username", "test");
                httpWebRequest.Headers.Add("password", "test");

                var apiResult = JsonConvert.DeserializeObject<ApiResultModel<List<ProductModel>>>(new StreamReader(httpWebRequest.GetResponse().GetResponseStream()).ReadToEnd());

                if (apiResult.IsSuccess)
                    return apiResult.Data;
                else
                    return new List<ProductModel>();

            }
            catch (Exception ex)
            {
                var result = ex.ToString();
                return new List<ProductModel>();
            }
        }

        public static List<ProductImageModel> GetImages(List<int> stokIds)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.bayramoglu.in/api/urun");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("username", "test");
                httpWebRequest.Headers.Add("password", "test");

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new Models.ImageApiRequestModel
                {
                    StokIds = stokIds
                });

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                var apiResult = JsonConvert.DeserializeObject<ApiResultModel<List<ProductImageModel>>>(new StreamReader(httpWebRequest.GetResponse().GetResponseStream()).ReadToEnd());

                if (apiResult.IsSuccess)
                    return apiResult.Data;
                else
                    return new List<ProductImageModel>();

            }
            catch (Exception ex)
            {
                var result = ex.ToString();
                return new List<ProductImageModel>();
            }
        }
    }
}
