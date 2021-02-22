using System;
using System.Collections.Generic;
using System.IO;
using NLog.Web;

namespace MovieLibrary
{
    
    class MovieManager
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        List<Movie> movies = new List<Movie>();
        string csvHeader = "";
        string movieFile;
        public MovieManager(string movieFile)
        {
            this.movieFile = movieFile;
            this.loadMovies();
        }

        public void addMovieToList(Movie movie) {
            this.movies.Add(movie);
        }

        public void userAddMovie() {
            //ask user for certain things
            int id = this.movies[this.movies.Count-1].getId() + 1;
            Console.WriteLine("Please enter the movie title");
            string title = Console.ReadLine();

            // Ask for genre ( can be multiple so ask if they want to add more)
            Boolean done = false;
            List<string> genre = new List<string>();
            while(!done) {
                Console.WriteLine("Please enter a genre");
                genre.Add(Console.ReadLine());
                Console.WriteLine("would you like to add more? yes/no");
                string userResponse = Console.ReadLine();
                // ask user if they want to add more
                if (userResponse != "yes") {
                    done = true;
                }

            }

            this.addMovieToList(new Movie(id, title, genre));
        }

        public void loadMovies() {
            Boolean firstLine = true;
            try
            {
                StreamReader sr1 = new StreamReader(this.movieFile);
                if (File.Exists(this.movieFile)) {

                    while (!sr1.EndOfStream)
                    {
                        if(!firstLine) {
                            string line = sr1.ReadLine();
                            String[] movieData = line.Split(",");
                            String[] genreData = movieData[2].Split("|");
                            List<string> genreList = new List<string>();
                            foreach (string genre in genreData) {
                                genreList.Add(genre);
                            }
                            this.addMovieToList(new Movie(Int32.Parse(movieData[0]), movieData[1], genreList));
                        } else {
                            this.csvHeader = sr1.ReadLine();
                            firstLine = false;
                        }
                       
                    }
                } else {
                    logger.Error("File does not exist: " + this.movieFile);
                }
                    
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                logger.Error("There has been an Exception " + e.Message);
            }
        }

        public void printMovies() {
            foreach (Movie movie in this.movies) {
                Console.WriteLine(movie.getTitle() + " genre: " + movie.getGenres());
            }
        }

        public void writeToFile() {
            try {
                if (File.Exists(this.movieFile)) {
                    StreamWriter sw = new StreamWriter(this.movieFile);
                    sw.WriteLine(this.csvHeader);
                    foreach (Movie movie in this.movies) {
                        sw.WriteLine(movie.getId() + "," + movie.getTitle() + "," + movie.getGenres());
                    }
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
