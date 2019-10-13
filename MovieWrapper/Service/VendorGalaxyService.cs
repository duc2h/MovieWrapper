using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MovieWrapper.Helpers;
using MovieWrapper.IService;
using MovieWrapper.Model;
using Newtonsoft.Json;

namespace MovieWrapper.Service
{
    public class VendorGalaxyService : IVendorGalaxyService
    {
        private readonly string _baseUrl = "https://www.galaxycine.vn/api";

        public List<MovieGalaxy> GetShowingMovie()
        {
            var result = MovieWrapperHelper.GetAsync($"{_baseUrl}/movie/showAndComming").Result;
            return JsonConvert.DeserializeObject<ShowingMovieGalaxy>(result)?
              .MovieShowing?.Where(m => DateTime.Compare(m.ReleaseDate, DateTime.Today.AddDays(1)) <= 0).ToList();
        }

        public MovieGalaxy GetDetail(string id)
        {
            var result = MovieWrapperHelper.GetAsync($"{_baseUrl}/content/movieDetail/{id}").Result;
            return JsonConvert.DeserializeObject<MovieGalaxy>(result);
        }

        public SessionMovie GetSessionMovie(string id, DateTime date)
        {
            var result = MovieWrapperHelper.GetAsync($"{_baseUrl}/session/movie/{id}").Result;
            var sessionMovieGalaxys = JsonConvert.DeserializeObject<List<SessionMovieGalaxy>>(result);
            var locations = (from sessionMovie in sessionMovieGalaxys
                             where sessionMovie.Dates != null &&
                                   sessionMovie.Dates.Any(d => d.ShowDate.Equals(date.ToString("dd/MM/yyyy")))
                             select sessionMovie.Name).ToList();

            if (locations.Count == 0) return null;

            return new SessionMovie
            {
                Date = date,
                Locations = locations
            };
        }
    }
}
