using MetodWhatsAppDesktop.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodWhatsAppDesktop.Services
{
    public class WAServices
    {
        private static RestClient _client;

        private static RestClient Client
        {
            get
            {
                if (_client == null)
                    _client = new RestClient("http://api.bayramoglu.in/api/");
                //_client = new RestClient("http://localhost:63173/api/");

                return _client;
            }
        }

        public static List<WATelefonRehberi> GetRehber()
        {

            try
            {
                var request = new RestRequest();
                request.Resource = "rehber";
                request.Method = Method.GET;
                request.AddHeader("content-type", "application/json");
                request.AddHeader("username", "test");
                request.AddHeader("password", "test");

                var response = Client.Execute(request);
                return JsonConvert.DeserializeObject<ApiResultModel<List<WATelefonRehberi>>>(response.Content)
                    ?.Data ?? new List<WATelefonRehberi>();
                                

            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new List<WATelefonRehberi>();
            }
        }
        
        public static bool AddUpdateRehber(WATelefonRehberi model)
        {
            try
            {
                var request = new RestRequest();
                request.Resource = "rehber";
                request.Method = Method.POST;
                request.AddHeader("content-type", "application/json");
                request.AddHeader("username", "test");
                request.AddHeader("password", "test");
                request.AddJsonBody(JsonConvert.SerializeObject(model));

                var response = Client.Execute(request);
                var output = response.Content;

                Console.WriteLine(output);

                return true;

            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return false;
            }
        }

        public static bool DeleteRehber(int telefonId)
        {
            try
            {
                var request = new RestRequest();
                request.Resource = "rehber";
                request.Method = Method.DELETE;
                request.AddHeader("content-type", "application/json");
                request.AddHeader("username", "test");
                request.AddHeader("password", "test");
                request.AddParameter("telefonId", telefonId);

                var response = Client.Execute(request);
                var output = response.Content;

                Console.WriteLine(output);

                return true;

            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return false;
            }
        }
        
        public static bool SendMessages(WAMessageRequestService model)
        {
            try
            {
                var request = new RestRequest();
                request.Resource = "wa";
                request.Method = Method.POST;
                request.AddHeader("content-type", "application/json");
                request.AddHeader("username", "test");
                request.AddHeader("password", "test");
                request.AddJsonBody(JsonConvert.SerializeObject(model));

                var response = Client.Execute(request);
                var output = response.Content;

                Console.WriteLine(output);

                return true;

            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return false;
            }
        }
    }
}
