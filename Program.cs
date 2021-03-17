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

                        var joWrite1 = (int)joResponse1["main"]["temp"];
                        var joWrite2 = joResponse1["wind"]["speed"];
                        var joWrite3 = (string)joResponse1["weather"][0]["main"];

                        Console.WriteLine("The current air temperature in " + userCity + " is: " + joWrite1 + "F" + " and the wind speed is " + joWrite2 + " mph.");

                        string airConditions = joWrite3;

                        switch (airConditions)
                        {

                            case "Clear":
                                Console.WriteLine("Currently, in " + userCity + ", it is clear.");
                                break;

                            case "Haze":
                                Console.WriteLine("Currently, in " + userCity + ", it is hazey.");
                                break;

                            case "Clouds":
                                Console.WriteLine("Currently, in " + userCity + ", it is cloudy.");
                                break;

                            case "Rain":
                                Console.WriteLine("Currently, in " + userCity + ", it is rainy.");
                                break;

                            case "Mist":
                                Console.WriteLine("Currently, in " + userCity + ", it is misty.");
                                break;

                            default:
                                Console.WriteLine("Currently, the air conditions in " + userCity + " is: ");
                                break;
                                
                        }

                        if (joWrite1 < 40)
                        {
                            Console.WriteLine("It's pretty chilly today! Better pack a jacket!");
                                
                        }

                        else if (joWrite1 > 90)
                        {

                            Console.WriteLine("It's quite the scorcher today in " + userCity + "! Don't forget your sunscreen!");

                        }

                        else
                        {
                            Console.WriteLine("Seems like a pretty nice day today. Not too hot, not too cold!");
                        }

                            

                        
                        

                        


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
