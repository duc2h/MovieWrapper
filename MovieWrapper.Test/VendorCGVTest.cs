using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieWrapper.IService;
using MovieWrapper.Service;

namespace MovieWrapper.Test
{
    [TestClass]
    public class VendorCGVTest
    {
        private Mock<IVendorCGVService> _service;

        [TestInitialize]
        public void Init()
        {
            var cgvService = new VendorCGVService();
            _service = new Mock<IVendorCGVService>();
            _service.Setup(m => m.GetShowingMovie()).Returns(cgvService.GetShowingMovie);
            _service.Setup(m => m.GetDetail(It.IsAny<string>())).Returns(cgvService.GetDetail("1982"));
            _service.Setup(m => m.GetSessionMovie(It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(cgvService.GetSessionMovie("19016400", DateTime.Today));
        }

        [TestMethod]
        public void GetShowingMovie()
        {
            var showingMovies = _service.Object.GetShowingMovie();
            Assert.IsNotNull(showingMovies);
            Assert.AreNotEqual(0, showingMovies.Count);
        }

        [TestMethod]
        public void GetDetail()
        {
            var movie = _service.Object.GetDetail(It.IsAny<string>());

            Assert.IsNotNull(movie.Data);
            Assert.AreEqual("1982", movie.Data.Id);
            Assert.AreEqual("joker", movie.Data.Name.ToLower());
            Assert.AreEqual(DateTime.Parse("2019-10-04"), movie.Data.ReleaseDate);
        }

        [TestMethod]
        public void GetSessionMovie()
        {
            var sessionMovie = _service.Object.GetSessionMovie(It.IsAny<string>(), It.IsAny<DateTime>());

            Assert.IsNotNull(sessionMovie);
            Assert.AreEqual(DateTime.Today, sessionMovie.Date);
        }
    }
}
