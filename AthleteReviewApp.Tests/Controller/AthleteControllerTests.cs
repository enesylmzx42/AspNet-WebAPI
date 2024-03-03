using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using AthleteReviewApp.Controllers;
using AthleteReviewApp.Dto;
using AthleteReviewApp.Interfaces;
using AthleteReviewApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AthleteReviewApp.Tests.Controller
{
    public class AthleteControllerTests
    {
        private readonly IAthleteRepository _AthleteRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public AthleteControllerTests()
        {
            _AthleteRepository = A.Fake<IAthleteRepository>();
            _reviewRepository = A.Fake<IReviewRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void AthleteController_GetAthletes_ReturnOK()
        {
            //Arrange
            var Athletes = A.Fake<ICollection<AthleteDto>>();
            var AthleteList = A.Fake<List<AthleteDto>>();
            A.CallTo(() => _mapper.Map<List<AthleteDto>>(Athletes)).Returns(AthleteList);
            var controller = new AthleteController(_AthleteRepository, _reviewRepository, _mapper);

            //Act
            var result = controller.GetAthletes();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void AthleteController_CreateAthlete_ReturnOK()
        {
            //Arrange
            int ownerId = 1;
            int catId = 2;
            var AthleteMap = A.Fake<Athlete>();
            var Athlete = A.Fake<Athlete>();
            var AthleteCreate = A.Fake<AthleteDto>();
            var Athletes = A.Fake<ICollection<AthleteDto>>();
            var pokmonList = A.Fake<IList<AthleteDto>>();
            A.CallTo(() => _AthleteRepository.GetAthleteTrimToUpper(AthleteCreate)).Returns(Athlete);
            A.CallTo(() => _mapper.Map<Athlete>(AthleteCreate)).Returns(Athlete);
            A.CallTo(() => _AthleteRepository.CreateAthlete(ownerId, catId, AthleteMap)).Returns(true);
            var controller = new AthleteController(_AthleteRepository, _reviewRepository, _mapper);

            //Act
            var result = controller.CreateAthlete(ownerId, catId, AthleteCreate);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
