using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MovieWrapper.Model;
using Newtonsoft.Json;

namespace MovieWrapper.Helpers
{
    public class MovieWrapperHelper
    {
        public static async Task<string> GetAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(url).Result;
                    if (!response.IsSuccessStatusCode) return null;

                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
