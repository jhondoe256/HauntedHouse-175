using HauntedHouse.Data.Entities.ChallengeEntities;

namespace HauntedHouse.Data.Entities.HouseEntities
{
    public class Floor
    {
       public int ID { get; set; }
       public string Name {get;set;} = string.Empty;
       public List<Challenge> Challenges {get;set;} = new List<Challenge>();  
    }
}