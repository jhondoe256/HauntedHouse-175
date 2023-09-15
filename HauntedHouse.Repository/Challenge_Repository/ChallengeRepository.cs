using HauntedHouse.Data.Entities.ChallengeEntities;
using HauntedHouse.Repository.Boss_Repository;

namespace HauntedHouse.Repository.Challenge_Repository
{
    public class ChallengeRepository
    {
        //* Give challenge repository access to BossRepository
        private readonly BossRepository _hHouseBossRepo = new BossRepository();

        //* fake db
        private readonly List<Challenge> _hHouseChallengeDb = new List<Challenge>();

        int _count = 0;

        public ChallengeRepository()
        {
            SeedChallenges();
        }
        //* Create
        public bool AddChallenge(Challenge challenge)
        {
            if (challenge is null)
            {
                return false;
            }
            else
            {
                _count++;
                challenge.ID = _count;
                _hHouseChallengeDb.Add(challenge);
                return true;
            }
        }

        //* Read all-data-in-db
        public List<Challenge> GetChallenges()
        {
            return _hHouseChallengeDb;
        }

        public Challenge GetChallenge(int challengeID)
        {
            //* SQL LINK QUERY SYNTAX (optional)
            //* return (from challenge in _hHouseChallengeDb
            //*         where challenge.ID == challengeID
            //*         select challenge).FirstOrDefault()!;

            //* SQL LINK Method SYNTAX (optional)
            //* return _hHouseChallengeDb.FirstOrDefault(challenge => challenge.ID == challengeID)!;

            foreach (var challenge in _hHouseChallengeDb)
            {
                if (challenge.ID == challengeID)
                    return challenge;
                else
                    return null!;
            }
            return null!;
        }


        private void SeedChallenges()
        {
            var floor1 = new FloorChallenge
            {
                ID =1,
                ChallengeDescription = 
                "There are three Rooms\n"+
                "The Left and Right ones are unlocked\n"+
                "Find Middle Room Key\n",
                ChallengeTasks = new List<string>
                {
                    "Find Middle Room Key\n"
                }
            };

            var floor2 = new FloorChallenge
            {
                ID =2,
                ChallengeDescription = "Find the missing Puzzle Piece and put it back in its place",
                ChallengeTasks = new List<string>
                {
                    "Find the missing Puzzle Piece\n",
                    "Put it back in its place\n"
                }
            };

             var floor3 = new BossChallenge
            {
                ID =3,
                ChallengeDescription = "Defeat the Demon with Pins in his Head!!!!",
                ChallengeTasks = new List<string>
                {
                    "Defeat the Demon with Pins in his Head!!!!"
                }
            };

            //* Add these to the Database
            _hHouseChallengeDb.Add(floor1);
            _hHouseChallengeDb.Add(floor2);
            _hHouseChallengeDb.Add(floor3);
        }
    }
}