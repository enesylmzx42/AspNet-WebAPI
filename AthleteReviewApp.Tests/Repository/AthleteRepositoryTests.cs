using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using AthleteReviewApp.Data;
using AthleteReviewApp.Models;
using AthleteReviewApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AthleteReviewApp.Tests.Repository
{
    public class AthleteRepositoryTests
    {

        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Athlete.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Athlete.Add(
                    new Athlete()
                    {
                        Name = "a",
                        BirthDate = new DateTime(1903, 1, 1),
                        AthleteCategories = new List<AthleteCategory>()
                            {
                                new AthleteCategory { Category = new Category() { Name = "a"}}
                            },
                        Reviews = new List<Review>()
                            {
                                new Review { Title="a",Text = "a", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "a", LastName = "a" } },
                                new Review { Title="a", Text = "a", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "a", LastName = "a" } },
                                new Review { Title="a",Text = "a", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "a", LastName = "a" } },
                            }
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
        [Fact]
        public async void AthleteRepository_GetAthlete_ReturnsAthlete()
        {
            //Arrange
            var name = "a";
            var dbContext = await GetDatabaseContext();
            var AthleteRepository = new AthleteRepository(dbContext);

            //Act
            var result = AthleteRepository.GetAthlete(name);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Athlete>();
        }

        [Fact]
        public async void AthleteRepository_GetAthleteRating_ReturnDecimalBetweenOneAndTen()
        {
            //Arrange
            var pokeId = 1;
            var dbContext = await GetDatabaseContext();
            var AthleteRepository = new AthleteRepository(dbContext);

            //Act
            var result = AthleteRepository.GetAthleteRating(pokeId);

            //Assert
            result.Should().NotBe(0);
            result.Should().BeInRange(1, 10);
        }
    }
}
