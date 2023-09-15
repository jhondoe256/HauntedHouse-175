using HauntedHouse.Data.Entities.ChallengeEntities;
using HauntedHouse.Data.Entities.HouseEntities;
using HauntedHouse.Repository.Challenge_Repository;

namespace HauntedHouse.Repository.Floors_Repository
{
    public class FloorRepository
    {
        private readonly ChallengeRepository _hHouseChallengeRepo = new ChallengeRepository();

        private readonly List<Floor> _hHouseFloorDb = new List<Floor>();

        int _count = 0;

        public FloorRepository()
        {
            SeedFloors();
        }

        public bool AddFloor(Floor floor)
        {
            if (floor is null)
            {
                return false;
            }
            else
            {
                _count++;
                floor.ID = _count;
                _hHouseFloorDb.Add(floor);
                return true;
            }
        }

        public List<Floor> GetFloors()
        {
            return _hHouseFloorDb;
        }

        public Floor GetFloor(int id)
        {
            foreach (Floor floor in _hHouseFloorDb)
            {
                if (floor.ID == id)
                    return floor;
            }
            return null!;
        }

        private void SeedFloors()
        {
            var floor = new Floor()
            {
                ID = 1,
                Name = "Main Floor",
                Challenges = _hHouseChallengeRepo.GetChallenges()
                             .Where(c => c.GetType() == typeof(FloorChallenge)).ToList()
            };

            var floor2 = new Floor()
            {
                ID = 2,
                Name = "Second Floor",
                Challenges = _hHouseChallengeRepo.GetChallenges()
                             .Where(c => c.GetType() == typeof(BossChallenge)).ToList()
            };

            _hHouseFloorDb.Add(floor);
            _hHouseFloorDb.Add(floor2);
        }

    }
}