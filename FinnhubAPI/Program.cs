// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace finnhubStock
{
    public class finnhubAPI
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a stock symbol as a command-line parameter.");
                return;
            }
            else
            {
                string symbol = args[0];
                string ApiKey = "cop89jhr01qikslb9sogcop89jhr01qikslb9sp0";
                string url = $"https://finnhub.io/api/v1/search?q={symbol}";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("X-Finnhub-Token", ApiKey);

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        Result getResult = JsonConvert.DeserializeObject<Result>(responseString);

                        if (getResult != null && getResult.count > 0)
                        {
                            foreach (StockLine stock in getResult.result)
                            {
                                Console.WriteLine($"{stock.symbol} {stock.description}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No results found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error fetching data. Please try again later.");
                    }
                }
            }
        }
    }

    public class Result
    {
        public int count { get; set; }
        public StockLine[] result { get; set; }
    }

    public class StockLine
    {
        public string symbol { get; set; }
        public string description { get; set; }
    }

}

