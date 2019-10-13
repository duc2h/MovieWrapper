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
    public class VendorBHDTest
    {
        private Mock<IVendorBHDService> _service;

        [TestInitialize]
        public void Init()
        {
            var bhdService = new VendorBHDService();
            _service = new Mock<IVendorBHDService>();
            _service.Setup(m => m.GetShowingMovie()).Returns(bhdService.GetShowingMovie);
            _service.Setup(m => m.GetDetail(It.IsAny<string>())).Returns(bhdService.GetDetail("HO00001516"));
            _service.Setup(m => m.GetSessionMovie(It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(bhdService.GetSessionMovie("HO00001789", DateTime.Today));
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

            Assert.IsNotNull(movie);
            Assert.AreEqual("HO00001516", movie.Id);
            Assert.AreEqual("joker", movie.Name.ToLower());
            Assert.AreEqual(DateTime.Parse("2019-10-04"), movie.ReleaseDate);
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
