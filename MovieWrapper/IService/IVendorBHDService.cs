using System;
using System.Collections.Generic;
using System.Text;
using MovieWrapper.Model;

namespace MovieWrapper.IService
{
    public interface IVendorBHDService
    {
        List<MovieBHD> GetShowingMovie();
        MovieBHD GetDetail(string id);
        SessionMovie GetSessionMovie(string id, DateTime date);
    }
}
