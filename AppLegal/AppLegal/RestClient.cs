using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppLegal
{
    public class RestClient<T>
    {
        public async Task<T> GetRestServicieDataAsync(string serviceAddres)
        {
           
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(serviceAddres);
                var response = await client.GetAsync(client.BaseAddress);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    //var jsonModel = JsonConvert.DeserializeObject<Customer>(jsonString, settings);
                    var result = JsonConvert.DeserializeObject<T>(jsonResult, settings);
                    return result;
                }
                
            }
            catch(Exception e)
            {
                Debug.WriteLine("" + e);
            }
            return default(T);
        }
    }
}
