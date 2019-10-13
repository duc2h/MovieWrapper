using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MovieWrapper.Model
{
    public class Movie
    {
        public string Id { get; set; }
        public virtual string Name { get; set; }
    }
}
