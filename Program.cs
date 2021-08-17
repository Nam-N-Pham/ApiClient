using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the joke machine!");

            bool makeJoke = true;
            while (makeJoke)
            {
                Console.WriteLine("What kind of joke(s) would you like? Random or Type, or would you like to Quit?");
                string typeOfJokeChoice = Console.ReadLine().ToLower();

                HttpClient client = new HttpClient();

                switch (typeOfJokeChoice)
                {
                    case "quit":
                        makeJoke = false;
                        break;
                    case "random":
                        Console.WriteLine("How many jokes would you like? 1 or 10?");
                        string numJokesChoice = Console.ReadLine();

                        switch (numJokesChoice)
                        {
                            case "1":
                                var oneJokeResponseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/random_joke");

                                Joke oneJoke = await JsonSerializer.DeserializeAsync<Joke>(oneJokeResponseAsStream);

                                Console.WriteLine(oneJoke.Setup);
                                Console.WriteLine("\n");
                                Console.WriteLine(oneJoke.Punchline);

                                break;
                            case "10":
                                var tenJokesResponseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/random_ten");

                                List<Joke> jokes = await JsonSerializer.DeserializeAsync<List<Joke>>(tenJokesResponseAsStream);

                                for (int i = 0; i < 10; i++)
                                {
                                    Console.WriteLine(i + 1);
                                    Console.WriteLine(jokes[i].Setup);
                                    Console.WriteLine(jokes[i].Punchline);
                                }

                                break;
                            case "default":
                                Console.WriteLine("Invalid number of jokes.");
                                break;
                        }
                        break;
                    case "type":
                        Console.WriteLine("What type of joke would you like? General or Programming?");
                        string jokeTypeChoice = Console.ReadLine().ToLower();

                        switch (jokeTypeChoice)
                        {
                            case "general":
                                Console.WriteLine("How many jokes would you like? 1 or 10?");
                                string numGeneralJokesChoice = Console.ReadLine();

                                switch (numGeneralJokesChoice)
                                {
                                    case "1":
                                        var oneGeneralJokeResponseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/jokes/general/random");

                                        List<Joke> oneGeneralJoke = await JsonSerializer.DeserializeAsync<List<Joke>>(oneGeneralJokeResponseAsStream);

                                        Console.WriteLine(oneGeneralJoke[0].Setup);
                                        Console.WriteLine("\n");
                                        Console.WriteLine(oneGeneralJoke[0].Punchline);

                                        break;
                                    case "10":
                                        var tenGeneralJokesResponseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/jokes/general/ten");

                                        List<Joke> generalJokes = await JsonSerializer.DeserializeAsync<List<Joke>>(tenGeneralJokesResponseAsStream);

                                        for (int i = 0; i < 10; i++)
                                        {
                                            Console.WriteLine(i + 1);
                                            Console.WriteLine(generalJokes[i].Setup);
                                            Console.WriteLine(generalJokes[i].Punchline);
                                        }

                                        break;
                                    case "default":
                                        Console.WriteLine("Invalid number of jokes.");
                                        break;
                                }
                                break;
                            case "programming":

                                break;
                            default:
                                Console.WriteLine("Invalid type of joke.");
                                break;
                        }
                        break;
                    case "default":
                        Console.WriteLine("Enter the 3 choices correctly.");
                        break;
                }
            }
        }
    }
}
