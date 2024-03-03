using AthleteReviewApp.Data;
using AthleteReviewApp.Models;

namespace AthleteReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.AthleteOwners.Any())
            {
                var AthleteOwners = new List<AthleteOwner>()
                {
                    new AthleteOwner()
                    {
                        Athlete = new Athlete()
                        {
                            Name = "Mike Tyson",
                            BirthDate = new DateTime(1959,1,1),
                            AthleteCategories = new List<AthleteCategory>()
                            {
                                new AthleteCategory { Category = new Category() { Name = "Box"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Mike Tyson",Text = "Mike Tyson is the best Athlete", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "İlyas", LastName = "Turalı" } },
                                new Review { Title="Mike Tyson", Text = "Mike Tyson is the most feared man ever exist", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "Mustafa", LastName = "Öz" } },
                                new Review { Title="Mike Tyson",Text = "Mike Tyson . Gives me chills", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Furkan", LastName = "Kılavs" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "John",
                            LastName = "Reacher",                            
                            Country = new Country()
                            {
                                Name = "USA"
                            }
                        }
                    },

                    new AthleteOwner()
                    {
                        Athlete = new Athlete()
                        {
                            Name = "Conor Mcregor",
                            BirthDate = new DateTime(1982,7,8),
                            AthleteCategories = new List<AthleteCategory>()
                            {
                                new AthleteCategory { Category = new Category() { Name = "MMA"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Conor Mcregor", Text = "Conor Mcregor is the best", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Ahmet", LastName = "Ak" } },
                                new Review { Title= "Conor Mcregor",Text = "He is perfect", Rating = 3,
                                Reviewer = new Reviewer(){ FirstName = "Kemal", LastName = "Er" } },
                                new Review { Title= "Conor Mcregor", Text = "Undisputed", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "Muammer", LastName = "Yılmaz" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Harry",
                            LastName = "Bale",
                            Country = new Country()
                            {
                                Name = "Ireland"
                            }
                        }
                    },

                    new AthleteOwner()
                    {
                        Athlete = new Athlete()
                        {
                            Name = "Lionel Messi",
                            BirthDate = new DateTime(1985,3,10),
                            AthleteCategories = new List<AthleteCategory>()
                            {
                                new AthleteCategory { Category = new Category() { Name = "Football"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Lionel Messi",Text = "best ever", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "Rıfat", LastName = "Simit" } },
                                new Review { Title="Lionel Messi",Text = "goat", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "Taylan", LastName = "KayA" } },
                                new Review { Title="Lionel Messi",Text = "I have not seen anything like this", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "Serhat", LastName = "Mansız" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Kevın",
                            LastName = "Maranio",
                            Country = new Country()
                            {
                                Name = "Argentina"
                            }
                        }
                    }
                };
                dataContext.AthleteOwners.AddRange(AthleteOwners);
                dataContext.SaveChanges();
            }
        }
    }
}
