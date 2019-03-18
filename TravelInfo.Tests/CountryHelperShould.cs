using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Moq;
using TravelInfo.Web.Helpers;
using Xunit;

namespace TravelInfo.Tests
{
    public class CountryHelperShould
    {
        [Fact]
        public void InitializeCountries()
        {
            //Get working dirctory
            var dir = Directory.GetCurrentDirectory();
            var index = dir.IndexOf("TravelInfo.Tests", StringComparison.CurrentCulture);
            var sub = dir.Substring(0, index);
            var root = Path.Combine(sub, "TravelInfo.Web/");
            //Arrange

            var mockEnvironment = new Mock<IHostingEnvironment>();
            mockEnvironment
                .Setup(m => m.ContentRootPath)
                .Returns(root);

            //Act
            var countryHelper = new CountryHelper(mockEnvironment.Object); //Initialize should be called from ctor
            var countries = countryHelper.GetCountries();

            //Assert
            Assert.True(countries.Count == 250); //magic number because I knowhow many countries are contained in list

        }
    }
}
