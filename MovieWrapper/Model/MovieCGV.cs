using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieWrapper.Model
{
    public class MovieCGV : Movie
    {
        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        [JsonProperty("full_description")]
        public string Description { get; set; }
        public MovieCGV Data { get; set; }
    }

    public class ShowingMovieCGV
    {
        public List<MovieCGV> Data { get; set; }
    }

    public class Cinema
    {
        public string Name { get; set; }
    }

    public class Location
    {
        public List<Cinema> Cinemas { get; set; }
    }

    public class SessionMovieCGV
    {
        public DateTime Date { get; set; }
        public List<Location> Locations { get; set; }
        public List<SessionMovieCGV> Data { get; set; }
    }
}
