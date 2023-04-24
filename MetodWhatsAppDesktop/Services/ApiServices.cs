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
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.bayramoglu.in/api/urun/getall");
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("username", "test");
                httpWebRequest.Headers.Add("password", "test");

                var apiResult = JsonConvert.DeserializeObject<ApiResultModel>(new StreamReader(httpWebRequest.GetResponse().GetResponseStream()).ReadToEnd());

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
    }
}
