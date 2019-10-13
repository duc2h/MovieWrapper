using System;
using System.Collections.Generic;
using System.Text;
using MovieWrapper.Model;

namespace MovieWrapper.IService
{
    public interface IVendorCGVService
    {
        List<MovieCGV> GetShowingMovie();
        MovieCGV GetDetail(string id);
        SessionMovie GetSessionMovie(string sku, DateTime date);
    }
}
