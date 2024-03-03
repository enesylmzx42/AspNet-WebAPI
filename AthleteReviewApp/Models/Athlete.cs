using System.ComponentModel.DataAnnotations;

namespace AthleteReviewApp.Models
{
    public class Athlete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<AthleteOwner> AthleteOwners { get; set; }
        public ICollection<AthleteCategory> AthleteCategories { get; set; }
    }
}
