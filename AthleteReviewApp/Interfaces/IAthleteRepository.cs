using AthleteReviewApp.Dto;
using AthleteReviewApp.Models;

namespace AthleteReviewApp.Interfaces
{
    public interface IAthleteRepository
    {
        ICollection<Athlete> GetAthletes();
        Athlete GetAthlete(int id);
        Athlete GetAthlete(string name);
        Athlete GetAthleteTrimToUpper(AthleteDto AthleteCreate);
        decimal GetAthleteRating(int pokeId);
        bool AthleteExists(int pokeId);
        bool CreateAthlete(int ownerId, int categoryId, Athlete Athlete);
        bool UpdateAthlete(int ownerId, int categoryId, Athlete Athlete);
        bool DeleteAthlete(Athlete Athlete);
        bool Save();
    }
}
