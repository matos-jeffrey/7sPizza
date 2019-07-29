using Newtonsoft.Json;
using SevensPizzaEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SevensPizza.Models
{
    public class OrderApi
    {
        //private string _url = "http://localhost:58548/";
        //private string _url = "http://localhost:64474/";
        private string _url = "https://7spizzaapi.azurewebsites.net/";

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
        public async Task<bool> Checkout(int custId,Order order)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(_url);
                var apiUrl = "api/Orders/"+custId;
                if (order.PaymentType == "Cash")
                    //if yes, set card to null
                    //otherwise will get 400 bad request
                    order.Card = null;
                //var message = JsonConvert.SerializeObject(order);
                //var context = new StringContent(message, UnicodeEncoding.UTF8, "application/json");
                // var res = await client.PutAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(order), UnicodeEncoding.UTF8, "application/json"));
                var res = await client.PutAsJsonAsync(apiUrl, order);
                //var res = await client.PutAsync(apiUrl, context);



                return true;
            }
        }
    }
}
