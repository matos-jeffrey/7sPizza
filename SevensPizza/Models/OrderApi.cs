using Newtonsoft.Json;
using SevensPizzaEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SevensPizza.Models
{
    public class OrderApi
    {
        private string _url = "http://localhost:58548/";

        public async Task<Order> GetOrder(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(_url);
                var apiUrl = "api/Orders/" + id;
                var res = await client.GetStringAsync(apiUrl);
                //deserialize to a order
                var order = JsonConvert.DeserializeObject<Order>(res);



                return order;
            }
        }
    }
}
