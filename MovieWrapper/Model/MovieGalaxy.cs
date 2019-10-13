using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MovieWrapper.Model
{
    public class MovieGalaxy : Movie
    {

        [JsonProperty("startdate")]
        public DateTime ReleaseDate { get; set; }
        [JsonProperty("point")]
        public double Rating { get; set; }
        public string Description { get; set; }
        public MovieGalaxy Item { get; set; }
    }

    public class ShowingMovieGalaxy
    {
        public List<MovieGalaxy> MovieShowing { get; set; }
    }

    public class SessionMovieDate
    {
        public string ShowDate { get; set; }
    }

    public class SessionMovieGalaxy
    {
        public string Name { get; set; }
        public List<SessionMovieDate> Dates { get; set; }

    }
}
