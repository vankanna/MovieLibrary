using System;
using NLog.Web;
using System.IO;

namespace MovieLibrary
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        static void Main(string[] args)
        {
            

            MovieManager manager = new MovieManager("movies.csv");
            logger.Info("Movie Manager loaded.");

             try
            {
                
                Boolean done = false;

                while(!done) {
                    Console.WriteLine("\nWhat would you like to do?");
                    Console.WriteLine("1 - Add New Movie");
                    Console.WriteLine("2 - List Movies");
                    Console.WriteLine("3 - Save Movie List");
                    Console.WriteLine("Q - Quit Program");

                    string choice = Console.ReadLine();
                    logger.Info("Users choice = " + choice);

                    if(choice == "1") {
                        manager.userAddMovie();
                    } else if (choice == "2") {
                        manager.printMovies();
                    } else if (choice == "3"){
                        manager.writeToFile();
                        logger.Info("Wrote to file");
                    } else if (choice == "Q" || choice == "q") {
                        done = true;
                    } else {
                        Console.WriteLine("Not a valid choice.");
                    };
                }
                logger.Info("Program Quitting");

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
