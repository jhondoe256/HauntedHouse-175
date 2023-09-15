using HauntedHouse.Data.Entities.EnemyEntities;

namespace HauntedHouse.Repository.Boss_Repository
{
    public class BossRepository
    {
        //* we need our "fake database"
        public List<Boss> _hHouseBossDb = new List<Boss>();

        public int _count = 0;

        public BossRepository()
        {
            SeedBoss();
        }

        //*helper methods for our Create method
        private void AssignId(Boss boss)
        {
            _count++;
            boss.ID = _count;
        }

        //*helper methods for our Create method
        private bool SaveToDatabase(Boss boss)
        {
            AssignId(boss);
            _hHouseBossDb.Add(boss);
            return true;
        }

        public bool AddBoss(Boss boss)
        {
            return (boss is null) ? false: SaveToDatabase(boss);
        }

        public Boss GetBoss()
        {
            return _hHouseBossDb.FirstOrDefault()!;
        }
        public Boss GetBoss(int id)
        {
            return _hHouseBossDb.SingleOrDefault(x => x.ID == id)!;
        }

        private void SeedBoss()
        {
            var boss = new Boss
            {
                ID = 1,
                Name = "Demon with Pins in his Head!"
            };

            _hHouseBossDb.Add(boss);
        }

    }
}