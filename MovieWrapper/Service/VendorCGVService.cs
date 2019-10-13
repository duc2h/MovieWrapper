using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MovieWrapper.Helpers;
using MovieWrapper.IService;
using MovieWrapper.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MovieWrapper.Service
{
    public class VendorCGVService : IVendorCGVService
    {
        private readonly string _baseUrl = "https://www.cgv.vn/default/api/movie";

        public List<MovieCGV> GetShowingMovie()
        {
            var result = MovieWrapperHelper.GetAsync($"{_baseUrl}/listSneakShow").Result;
            var showingMovie = JsonConvert.DeserializeObject<ShowingMovieCGV>(result);
            return showingMovie?.Data?.Where(m => DateTime.Compare(m.ReleaseDate, DateTime.Today.AddDays(1)) <= 0)
                .ToList();
        }

        public MovieCGV GetDetail(string id)
        {
            var result = MovieWrapperHelper.GetAsync($"{_baseUrl}/movie/id/{id}").Result;
            return JsonConvert.DeserializeObject<MovieCGV>(result);
        }

        public SessionMovie GetSessionMovie(string sku, DateTime date)
        {
            var result = MovieWrapperHelper.GetAsync($"{_baseUrl}/showtimes/sku/{sku}/date/{date.ToString("ddMMyyyy")}").Result;
            var data = JsonConvert.DeserializeObject<SessionMovieCGV>(result);
            var sessionMovieCGV = data?.Data?.FirstOrDefault();
            if (sessionMovieCGV == null) return null;

            var locations = (from location in sessionMovieCGV.Locations
                             from cinema in location.Cinemas
                             select cinema.Name).ToList();
            if (locations.Count == 0) return null;

            return new SessionMovie
            {
                Date = sessionMovieCGV.Date,
                Locations = locations
            };
        }
    }
}
