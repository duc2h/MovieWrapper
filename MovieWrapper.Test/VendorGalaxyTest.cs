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
    public class VendorGalaxyTest
    {
        private Mock<IVendorGalaxyService> _service;

        [TestInitialize]
        public void Init()
        {
            var galaxyService = new VendorGalaxyService();
            _service = new Mock<IVendorGalaxyService>();
            _service.Setup(m => m.GetShowingMovie()).Returns(galaxyService.GetShowingMovie);
            _service.Setup(m => m.GetDetail(It.IsAny<string>()))
                .Returns(galaxyService.GetDetail("26308b4c-ed99-45b4-8280-ca220e0ff957"));
            _service.Setup(m => m.GetSessionMovie(It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(galaxyService.GetSessionMovie("0e67de8d-8190-45e2-b2aa-aa30468d867f", DateTime.Today));
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

            Assert.IsNotNull(movie.Item);
            Assert.AreEqual("26308b4c-ed99-45b4-8280-ca220e0ff957", movie.Item.Id);
            Assert.AreEqual("joker", movie.Item.Name.ToLower());
            Assert.AreEqual(DateTime.Parse("2019-10-03"), movie.Item.ReleaseDate);
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
