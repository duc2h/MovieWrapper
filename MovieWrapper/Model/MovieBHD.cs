using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MovieWrapper.Model
{
    public class MovieBHD : Movie
    {
        [JsonProperty("title")]
        public override string Name { get; set; }
        [JsonProperty("openingDate")]
        public DateTime ReleaseDate { get; set; }
        [JsonProperty("customerRating.count")]
        public double Rating { get; set; }
        [JsonProperty("synopsis")]
        public string Description { get; set; }
    }

    public class CinemaBHD
    {
        public string CinemaId { get; set; }
        public string Name { get; set; }
    }
}
