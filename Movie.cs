using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    class Movie
    {
        int id;
        string title;

        List<string> genre = new List<string>();

        public Movie(int id, string title, List<string> genre)
        {
            this.id = id;
            this.title = title;
            this.genre = genre;
        }

        public int getId() {
            return this.id;
        }

        public string getTitle() {
            return title;
        }

        public string getGenres() {
            return String.Join("|", this.genre.ToArray());
        }
    }
}
