using AthleteReviewApp.Data;
using AthleteReviewApp.Dto;
using AthleteReviewApp.Interfaces;
using AthleteReviewApp.Models;

namespace AthleteReviewApp.Repository
{
    public class AthleteRepository : IAthleteRepository
    {
        private readonly DataContext _context;

        public AthleteRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateAthlete(int ownerId, int categoryId, Athlete Athlete)
        {
            var AthleteOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var AthleteOwner = new AthleteOwner()
            {
                Owner = AthleteOwnerEntity,
                Athlete = Athlete,
            };

            _context.Add(AthleteOwner);

            var AthleteCategory = new AthleteCategory()
            {
                Category = category,
                Athlete = Athlete,
            };

            _context.Add(AthleteCategory);

            _context.Add(Athlete);

            return Save();
        }

        public bool DeleteAthlete(Athlete Athlete)
        {
            _context.Remove(Athlete);
            return Save();
        }

        public Athlete GetAthlete(int id)
        {
            return _context.Athlete.Where(p => p.Id == id).FirstOrDefault();
        }

        public Athlete GetAthlete(string name)
        {
            return _context.Athlete.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetAthleteRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Athlete.Id == pokeId);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Athlete> GetAthletes()
        {
            return _context.Athlete.OrderBy(p => p.Id).ToList();
        }

        public Athlete GetAthleteTrimToUpper(AthleteDto AthleteCreate)
        {
            return GetAthletes().Where(c => c.Name.Trim().ToUpper() == AthleteCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool AthleteExists(int pokeId)
        {
            return _context.Athlete.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAthlete(int ownerId, int categoryId, Athlete Athlete)
        {
            _context.Update(Athlete);
            return Save();
        }
    }
}
