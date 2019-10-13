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

namespace MovieWrapper.Service
{
    public class VendorBHDService : IVendorBHDService
    {
        private readonly string _baseUrl = "https://booking.bhdstar.vn/WSVistaWebClient/api/mobile/v1";

        public List<MovieBHD> GetShowingMovie()
        {
            var result = MovieWrapperHelper.GetAsync($"{_baseUrl}/films").Result;
            var movies = JsonConvert.DeserializeObject<List<MovieBHD>>(result);
            return movies?.Where(m => DateTime.Compare(m.ReleaseDate, DateTime.Today.AddDays(1)) <= 0).ToList();
        }

        public MovieBHD GetDetail(string id)
        {
            var result = MovieWrapperHelper.GetAsync($"{_baseUrl}/films/{id}").Result;
            return JsonConvert.DeserializeObject<MovieBHD>(result);
        }

        public SessionMovie GetSessionMovie(string id, DateTime date)
        {
            var resultSession = MovieWrapperHelper.GetAsync($"{_baseUrl}/sessions?filmId={id}&start={date.ToString("yyyy-MM-dd")}").Result;
            var resultCinema = MovieWrapperHelper.GetAsync($"https://booking.bhdstar.vn/WSVistaWebClient/RESTData.svc/cinemas").Result;

            string[] separator = { "\\r" };
            var cinemaXMLs = resultCinema.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            var cinemaBHDs = JsonConvert.DeserializeObject<List<CinemaBHD>>(resultSession);
            var cinemaIds = cinemaBHDs.Select(m => m.CinemaId).Distinct().ToList();
            var locations = new List<string>();

            foreach (var cinemaXML in cinemaXMLs)
            {
                var strList = cinemaXML.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var strDistinct = strList.Distinct().ToList();
                if (strDistinct.Count != 2 ||
                    !cinemaIds.Any(cinemaId => cinemaId.Equals(strDistinct.FirstOrDefault()))) continue;

                locations.Add(strDistinct.ElementAt(1));
            }

            return new SessionMovie
            {
                Date = date,
                Locations = locations
            };
        }
    }
}
