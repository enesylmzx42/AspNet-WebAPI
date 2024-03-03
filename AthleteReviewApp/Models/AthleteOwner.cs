namespace AthleteReviewApp.Models
{
    public class AthleteOwner
    {
        public int AthleteId { get; set; }
        public int OwnerId { get; set; }
        public Athlete Athlete { get; set; }
        public Owner Owner { get; set; }
    }
}
