using ORM_Generics.Models;

namespace ORM_Generics
{
    public class Program
    {
        static void Main(string[] args)
        {
            BannedGameOperations();
        }

        static List<Pokemon> ReadPokemonFromFile()
        {
            string rootPath = FileRoot.GetDefaultDirectory();
            string pokemonCSVPath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}AllPokemon.csv";

            var pokemonList = new List<Pokemon>();
            var pokemonId = 1;
            using (StreamReader sr = new StreamReader(pokemonCSVPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    List<string> elements = line.Split(",").ToList();
                    pokemonList.Add(new Pokemon(
                        pokemonId++, Convert.ToInt32(elements[0]),
                        elements[1], elements[2], elements[3], elements[4],
                        Convert.ToInt32(elements[5]), Convert.ToInt32(elements[6]),
                        Convert.ToInt32(elements[7]), Convert.ToInt32(elements[8]),
                        Convert.ToInt32(elements[9]), Convert.ToInt32(elements[10]),
                        Convert.ToInt32(elements[11]), Convert.ToInt32(elements[12])));
                }
            }
            return pokemonList;
        }

        static List<BannedGame> ReadGamesFromFile()
        {
            string rootPath = FileRoot.GetDefaultDirectory();
            string gamesCSVPath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}BannedGames.csv";

            var gamesList = new List<BannedGame>();
            var gameId = 1;
            using (StreamReader sr = new StreamReader(gamesCSVPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    List<string> elements = line.Split(",").ToList();
                    gamesList.Add(new BannedGame(gameId++, elements[0], elements[1], elements[2], elements[3]));
                }
            }
            return gamesList;
        }

        static void PokemonOperations()
        {
            var pokemonList = ReadPokemonFromFile();

            // create QueryBuilder object for the methods
            string rootPath = FileRoot.GetDefaultDirectory();
            string dbPath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";
            QueryBuilder qb = new QueryBuilder(dbPath);

            using (qb)
            {
                Console.WriteLine("\nClearing all pokemon from database...hang tight.");
                qb.DeleteAll<Pokemon>();

                Console.WriteLine("Database cleared. Now adding all pokemon from data file...");
                foreach (Pokemon p in pokemonList)
                {
                    qb.Insert<Pokemon>(p);
                }
                Console.WriteLine($"{pokemonList.Count} pokemon were added...");

                Console.Write("Please enter the ID of the pokemon you would like to view: ");
                var id = Convert.ToInt32(Console.ReadLine());
                Pokemon readPokemon = qb.Read<Pokemon>(id);
                Console.WriteLine(readPokemon);

                Console.WriteLine("\n\n====================================================");
                Console.WriteLine("Reading all Pokemon from the database...one sec.");
                Console.WriteLine("====================================================");
                var pokemonListFromDB = qb.ReadAll<Pokemon>();
                foreach (Pokemon p in pokemonListFromDB)
                {
                    Console.WriteLine(p);
                }

                Console.Write("\nPlease enter the ID of the pokemon you would like to remove from the database: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Deleting pokemon with ID {id}...");
                qb.Delete<Pokemon>(id);

            }
        }

        static void BannedGameOperations()
        {
            var gamesList = ReadGamesFromFile();

            string rootPath = FileRoot.GetDefaultDirectory();
            string dbPath = rootPath + $"{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}data.db";
            QueryBuilder qb = new QueryBuilder(dbPath);

            using(qb)
            {
                Console.WriteLine("\nClearing all games from database...hang tight.");
                qb.DeleteAll<BannedGame>();

                Console.WriteLine("Database cleared. Now adding all games from data file...");
                foreach (BannedGame g in gamesList)
                {
                    qb.Insert<BannedGame>(g);
                }
                Console.WriteLine($"{gamesList.Count} games were added...");

                Console.Write("Please enter the ID of the game you would like to view: ");
                var id = Convert.ToInt32(Console.ReadLine());
                BannedGame readGame = qb.Read<BannedGame>(id);
                Console.WriteLine(readGame);

                Console.WriteLine("\n\n====================================================");
                Console.WriteLine("Reading all Games from the database...one sec.");
                Console.WriteLine("====================================================");
                var gameListFromDB = qb.ReadAll<BannedGame>();
                foreach (BannedGame g in gameListFromDB)
                {
                    Console.WriteLine(g);
                }

                Console.Write("\nPlease enter the ID of the game you would like to remove from the database: ");
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Deleting game with ID {id}...");
                qb.Delete<BannedGame>(id);

            }
        }
    }
}