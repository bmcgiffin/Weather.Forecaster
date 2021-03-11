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
            Console.WriteLine("Welcome to the Weather Forecasting Information Kiosk!");
            Console.WriteLine("Please enter the name of your city to check its weather!");

            bool done = false;

            while (!done)
            {
                string userCity = Console.ReadLine().ToUpper();
                Console.WriteLine("Great! Sounds like you picked: " + userCity + "!");

                Console.WriteLine("Calling the Open Weather API to retrive weather update...");
                var apiCall = "https://api.openweathermap.org/data/2.5/weather?q=" + userCity + "&appid=a6bf3dc24cf54af353aa915b839e088a&units=imperial";
                var responseTask = client.GetAsync(apiCall);

                responseTask.Wait();

                if (responseTask.IsCompleted)
                {
                    var result1 = responseTask.Result;

                    if (result1.IsSuccessStatusCode)
                    {
                        done = true;
                        var messageTask = result1.Content.ReadAsStringAsync();

                        messageTask.Wait();

                        JObject joResponse1 = JObject.Parse(messageTask.Result);

                        var joWrite1 = (joResponse1["main"]["temp"]);
                        var joWrite2 = (joResponse1["wind"]["speed"]);

                        Console.WriteLine("The current air temperature in " + userCity + " is: " + joWrite1 + "F" + " and the wind speed is " + joWrite2 + " mph.");
                        
                    }
                    else
                    {
                        Console.WriteLine("Oops! You've entered an invalid city. Please enter a valid city name.");                        
                    }
                }
            }

            Console.ReadLine();
        }

        

    }
}
