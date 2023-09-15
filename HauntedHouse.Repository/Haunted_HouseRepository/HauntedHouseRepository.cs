using HauntedHouse.Data.Entities.HouseEntities;
using HauntedHouse.Repository.Floors_Repository;

namespace HauntedHouse.Repository.Haunted_HouseRepository
{
    public class HauntedHouseRepository
    {
        private readonly FloorRepository _hHouseFloorRepo = new FloorRepository();

        private readonly List<HauntedHouse.Data.Entities.HouseEntities.HauntedHouse> _hHouseDb = 
                    new List<HauntedHouse.Data.Entities.HouseEntities.HauntedHouse>();

        int _count =0;

        public HauntedHouseRepository()
        {
            SeedData();
        }

        public bool AddHauntedHouse(HauntedHouse.Data.Entities.HouseEntities.HauntedHouse house)
        {
            if(house is null)
            {
                return false;
            }
            else
            {
                _count++;
                house.ID = _count;
                _hHouseDb.Add(house);
                return true;
            }
        }

        public List<HauntedHouse.Data.Entities.HouseEntities.HauntedHouse> GetHauntedHouses()
        {
            return _hHouseDb;
        }

        public HauntedHouse.Data.Entities.HouseEntities.HauntedHouse GetHauntedHouse()
        {
            return _hHouseDb.FirstOrDefault()!;
        }

        public bool HasCompletedGame(List<Floor> rooms)
        {
            if(rooms.Count == 0)
            {
                return true;
            }
            return false;
        }

        private void SeedData()
        {
            var hauntedHouse = new HauntedHouse.Data.Entities.HouseEntities.HauntedHouse();
            hauntedHouse.FloorsInHouse = _hHouseFloorRepo.GetFloors();
            AddHauntedHouse(hauntedHouse);
        }

    }
}