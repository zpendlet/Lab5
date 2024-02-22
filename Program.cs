using System;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json; 


class Program
{
    static async Task Main()
    {
        Console.WriteLine("Welcome to the Cocktail Console App!");
        //Key is 1 for testing
        string apiKey = "1";


       while(true)
       {
        //Menu options
        Console.WriteLine("Menu:");
        Console.WriteLine("1: Look up a drink by ID: ");
        Console.WriteLine("2. fetch a random drink: ");
        Console.WriteLine("3. Exit");


        Console.WriteLine("Enter a choice: ");
        string userInput = Console.ReadLine();

        switch(userInput)
        {
            //cases for each option
            case "1": 
                Console.Write("Enter ID: ");
                string cocktailId = Console.ReadLine();
                await GetCocktailById(apiKey, cocktailId);
                break;
            case "2":
                await GetCocktailRandom(apiKey);
                break;
            case "3":
                Console.WriteLine("Bye!");
                break;

        }

       }
    }

    static async Task GetCocktailById(string apiKey, string id)
    {
        using (HttpClient client = new HttpClient())
        {
            //endpoint
            string endpoint = "https://www.thecocktaildb.com/api/json/v1/{apiKey}/lookup.php?i={id}";
            HttpResponseMessage response = await client.GetAsync(endpoint);

            //response handling
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var cocktailData = JsonConvert.DeserializeObject<CocktailData>(responseData);

                if (cocktailData.Drinks != null && cocktailData.Drinks.Count > 0)
                {
                    var firstCocktail = cocktailData.Drinks[0];
                    Console.WriteLine($"Cocktail Name: {firstCocktail.StrDrink}");
                    Console.WriteLine($"Category: {firstCocktail.StrCategory}");
                    Console.WriteLine($"Glass Type: {firstCocktail.StrGlass}");
                    Console.WriteLine($"Instructions: {firstCocktail.StrInstructions}");
                }
                else
                {
                    Console.WriteLine($"No information found for cocktail ID {id}.");
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
           

    }

    static async Task GetCocktailRandom(string apiKey)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiEndpoint = $"https://www.thecocktaildb.com/api/json/v1/{apiKey}/random.php";

            HttpResponseMessage response = await client.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var cocktailData = JsonConvert.DeserializeObject<CocktailData>(responseData);

                if (cocktailData.Drinks != null && cocktailData.Drinks.Count > 0)
                {
                    var randomCocktail = cocktailData.Drinks[0];
                    Console.WriteLine($"Random Cocktail Name: {randomCocktail.StrDrink}");
                    Console.WriteLine($"Category: {randomCocktail.StrCategory}");
                    Console.WriteLine($"Glass Type: {randomCocktail.StrGlass}");
                    Console.WriteLine($"Instructions: {randomCocktail.StrInstructions}");
                }
                else
                {
                    Console.WriteLine($"No information found for a random cocktail.");
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }

}



