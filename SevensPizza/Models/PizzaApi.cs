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
    public class PizzaApi
    {
        //private string _url = "http://localhost:58548/";
        private string _url = "http://localhost:64474/";

        public async Task<List<Topping>> GetTopping()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var apiUrl = "api/Toppings";
                var res = await client.GetStringAsync(_url + apiUrl);
                var list = JsonConvert.DeserializeObject<List<Topping>>(res);
                return list;
            }

        }

        //get pizza
        public async Task<Pizza> GetPizza(int id,int custId)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(_url);

            var apiUrl = "api/Pizzas/"+custId+"/" + id;
            var res = await client.GetStringAsync(apiUrl);
            //deserialize to a pizza
            var pizza = JsonConvert.DeserializeObject<Pizza>(res);
            return pizza;
        }

        //submit custom pizza
        public async Task<bool> PostPizza(int custId,Pizza pizza)
        {
            using (HttpClient client = new HttpClient())
            {
                //client basic setting
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(_url);
                //1 is customer id
                var apiUrl = "api/Pizzas/"+custId;
                var message = JsonConvert.SerializeObject(pizza);
                var res = await client.PostAsJsonAsync(apiUrl, pizza);
                return true;
            }

        }

        public async Task<bool> UpdatePizza(int oldQuantity, Pizza pizza)
        {
            using (HttpClient client = new HttpClient())
            {
                //client basic setting
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(_url);
                //1 is customer id
                var apiUrl = "api/Pizzas/" + oldQuantity;
                var res = await client.PutAsJsonAsync(apiUrl, pizza);
                return true;
            }
        }
    }
}
