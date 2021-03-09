using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace apiCall
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            Console.WriteLine("Calling Open Weather API to retrive weather update...");
            var responseTask = client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=omaha&appid=a6bf3dc24cf54af353aa915b839e088a");
            responseTask.Wait();
            if (responseTask.IsCompleted);
            {
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode);
                {
                    var messageTask = result.Content.ReadAsStringAsync();
                    messageTask.Wait();

                    //JObject joResponse = JObject.Parse(result);
                    //JObject ojObject = (JObject)joResponse["response"];
                    //JArray array = (JArray)ojObject["chats"];
                    //int id = Convert.ToInt32(array[0].ToString());

                    Console.WriteLine("Message from Open Weather API  : " + messageTask.Result);
                    Console.ReadLine();
                    

                }
            }
        }
    }
}
