using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieWrapper.Model;

namespace MovieWrapper.IService
{
    public interface IVendorGalaxyService
    {
        List<MovieGalaxy> GetShowingMovie();
        MovieGalaxy GetDetail(string id);
        SessionMovie GetSessionMovie(string id, DateTime date);
    }
}
