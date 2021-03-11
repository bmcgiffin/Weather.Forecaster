using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Weather.Forecaster
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            Console.WriteLine("Calling the Open Weather API to retrive weather update...");
            var responseTask1 = client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=omaha&appid=a6bf3dc24cf54af353aa915b839e088a&units=imperial");
            var responseTask2 = client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=chicago&appid=a6bf3dc24cf54af353aa915b839e088a&units=imperial");
            
            responseTask1.Wait();
            responseTask2.Wait();

            if (responseTask1.IsCompleted && responseTask2.IsCompleted)
            {
                var result1 = responseTask1.Result;
                var result2 = responseTask2.Result;

                if (result1.IsSuccessStatusCode && result2.IsSuccessStatusCode)
                {
                    var messageTask1 = result1.Content.ReadAsStringAsync();
                    messageTask1.Wait();

                    var messageTask2 = result2.Content.ReadAsStringAsync();
                    messageTask2.Wait();

                    JObject joResponse1 = JObject.Parse(messageTask1.Result);
                    JObject joResponse2 = JObject.Parse(messageTask2.Result);

                    // To retrieve temperature information
                    //var joWrite1 = (joResponse1["main"]["temp"]);              
                    //var joWrite2 = (joResponse2["main"]["temp"]);

                    var joWrite1 = (joResponse1["wind"]["speed"]);              
                    var joWrite2 = (joResponse2["wind"]["speed"]);

                    Console.WriteLine("The wind speed in Omaha is: " + joWrite1);
                    Console.WriteLine("The wind speed in Chicago is: " + joWrite2);

                    // To use with the temperature information
                    //Console.WriteLine("The current temperature in Omaha is " + joWrite1);
                    //Console.WriteLine("The current temperature in Tokyo is " + joWrite2);
                    
                    // Compare Omaha to Tokyo

                    //Console.WriteLine("Message from Open Weather API  : " + messageTask.Result);
                    Console.ReadLine();

                }

                else

                {
                    Console.WriteLine("Unable to contact Open Weather API. Please contact your administrator...");
                    Console.ReadLine();
                }                                
            }
        }
    }
}
