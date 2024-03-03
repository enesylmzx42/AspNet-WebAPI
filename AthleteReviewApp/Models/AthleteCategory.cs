namespace AthleteReviewApp.Models
{
    public class AthleteCategory
    {
        public int AthleteId { get; set; }
        public int CategoryId { get; set; }
        public Athlete Athlete { get; set; }
        public Category Category { get; set; }
    }
}
