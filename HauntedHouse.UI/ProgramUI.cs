using static System.Console;
using HauntedHouse.Data.Entities.HouseEntities;
using HauntedHouse.Repository.Haunted_HouseRepository;
using HauntedHouse.Data.Utilities;
using HauntedHouse.Data.Entities;
using HauntedHouse.Data.Entities.ChallengeEntities;

namespace HauntedHouse.UI
{
    public class ProgramUI
    {
        private readonly HauntedHouseRepository _hauntedHouseRepo = new HauntedHouseRepository();
        private HauntedHouse.Data.Entities.HouseEntities.HauntedHouse _house;
        private int _challengeCounter;
        private bool IsRunning = true;
        public bool _hasMiddleRoomKey;
        public bool _hasPuzzlePiece;


        public ProgramUI()
        {
            // SeedData();
            _house = _hauntedHouseRepo.GetHauntedHouse();
        }

        private void SeedData()
        {
            // _hauntedHouseRepo.SeedData();
        }

        public void Run()
        {
            RunApplication();
        }

        private void RunApplication()
        {
            while (IsRunning)
            {
                Clear();
                System.Console.WriteLine("Welcome to the Haunted House, Please Make a Selection:\n" +
                                        "1. Start Game\n" +
                                        "2. End Game\n");

                var userInput = ReadLine();

                switch (userInput)
                {
                    case "1":
                        StartGame();
                        break;
                    case "2":
                        IsRunning = CloseGame();
                        break;
                    default:
                        WriteLine("Invalid Selection.");
                        break;
                }

            }
        }

        private bool CloseGame()
        {
            WriteLine("Thanks for playing!");
            PressAnyKeyToContinue();
            return false;
        }

        private void PressAnyKeyToContinue()
        {
            WriteLine("Press any key to continue.");
            ReadKey();
        }

        private void StartGame()
        {
            Clear();

            while (!_house.Player.IsDead && IsRunning)
            {
                GameUtilities.TellTheStory($"You are a Paranormal Investigator,\nand you have to enter a haunted house on {_house.Address}" +
                                          $"You notice... Press any Key to Continue...");

                ReadKey();
                while (_hasMiddleRoomKey == false)
                {
                    LoadFirstChallenge();
                }

                GameUtilities.TellTheStory("You use a key to opent the Middle Room Door!");

                GameUtilities.TellTheStory("You go up the stairs, your on the Next Floor!");

                while (_hasPuzzlePiece == false)
                {
                    LoadSecondChallenge();
                }

                LoadFinalChallenge();

                ReadKey();
            }

            if (_house.Player.IsDead)
            {
                IsRunning = CloseGame();
            }
        }

        private void LoadFinalChallenge()
        {
            Clear();
            ClearChallengCounter();

            //  var currentChallenge = _house.FloorsInHouse[(int)ChallengesIndex.SecondChallenge].Challenges[_challengeCounter];

            GameUtilities.TellTheStory("You place the puzzle piece inside of a missing section of the Puzzle\nDARKNESS SURROUNDS YOU\n" +
                                       "A creepy individual with pins in his head approaches, what will you do?\n" +
                                       "1. Shoot the Damn Demon!\n" +
                                       "2. Ask him what he wants?\n" +
                                       "3. Try to escape!\n");

            var userInput = ReadLine();
            switch (userInput)
            {
                case "1":
                    ShootTheDamnDemon();
                    break;

                case "2":
                    AskWhatHeWants();
                    break;

                case "3":
                    TryToEscape();
                    break;

                default:
                    WriteLine("Invalid Selection, THIS CAN COST YOU YOUR LIFE!!!!");
                    break;
            }
        }

        private void TryToEscape()
        {
            Clear();
            BossChallenge currentChallenge = (BossChallenge)_house.FloorsInHouse[1].Challenges[0];
            GameUtilities.TellTheStory("You try to get away, Fish hooks fly from nowhere and attach to you.\nTHEY RIP YOU APART!!\n" +
                                       "The man with pins in his head laughs, 'HAAAAA,HA,HA!'");
            currentChallenge.Boss!.Attack(_house.Player, 1000, "Fish-Hooks of Destruction!");
            _hasPuzzlePiece = false;
        }


        private void AskWhatHeWants()
        {
            Clear();
            BossChallenge currentChallenge = (BossChallenge)_house.FloorsInHouse[1].Challenges[0];
            GameUtilities.TellTheStory("You ask him what he wants...\nYOUR SOUL!!!\nYou try to get away, Fish hooks fly from nowhere and attach to you.\nTHEY RIP YOU APART!!\n" +
                                       "The man with pins in his head laughs, 'HAAAAA,HA,HA!'");
            currentChallenge.Boss!.Attack(_house.Player, 1000, "Fish-Hooks of Destruction!");
            _hasPuzzlePiece = false;
        }


        private void ShootTheDamnDemon()
        {
            Clear();
            BossChallenge currentChallenge = (BossChallenge)_house.FloorsInHouse[1].Challenges[0];
            GameUtilities.TellTheStory("You shoot the Damn Demon!");
            _house.Player.ShootPlasmaPistol(currentChallenge.Boss!, 50);
            if (_house.Player.IsDead == false)
            {
                while (currentChallenge.Boss!.HealthPoints > 0)
                {
                    GameUtilities.TellTheStory("Will you shoot again y/n");
                    var userInput = ReadLine();
                    if (userInput != "Y".ToLower())
                    {
                        Clear();
                        GameUtilities.TellTheStory("You ask him what he wants...\nYOUR SOUL!!!\nYou try to get away, Fish hooks fly from nowhere and attach to you.\nTHEY RIP YOU APART!!\n" +
                                         "The man with pins in his head laughs, 'HAAAAA,HA,HA!'");
                        currentChallenge.Boss!.Attack(_house.Player, 1000, "Fish-Hooks of Destruction!");
                        _hasPuzzlePiece = false;
                        break;
                    }
                    else
                    {
                        _house.Player.ShootPlasmaPistol(currentChallenge.Boss, 20);
                    }
                }
                if (currentChallenge.Boss.IsDead)
                {

                    WriteLine("You killed the Demon With Pins In His Head!!!...or so you thought...");
                    IsRunning = CloseGame();
                }
            }
            else
            {
                IsRunning = CloseGame();
            }
        }

        private void LoadSecondChallenge()
        {
            Clear();
            ClearChallengCounter();
            var currentChallenge = _house.FloorsInHouse[(int)HauntedHouse.Data.Entities.ChallengesIndex.FirstChallenge].Challenges[++_challengeCounter];
            GameUtilities.TellTheStory("There is a Large Puzzle in the middle of the hall.");
            GameUtilities.TellTheStory(currentChallenge.ChallengeDescription);
            GameUtilities.TellTheStory("Which room will you select this time?\n" +
                                       "1. The Room down the hall and to the Left?\n" +
                                       "2. The Room down the hall and to the Right?\n");

            var userInput = ReadLine();
            switch (userInput)
            {
                case "1":
                    LoadTheRoomDownTheHall_ToTheLeft();
                    break;
                case "2":
                    LoadTheRoomDownTheHall_ToTheRight();
                    break;
                default:
                    System.Console.WriteLine("Invalid Selection");
                    break;
            }


        }

        private void LoadTheRoomDownTheHall_ToTheLeft()
        {
            bool hasLeftRoom = false;
            while (!hasLeftRoom)
            {
                Clear();
                GameUtilities.TellTheStory("You entered the room. Its some sort of Theater of Lost Souls, Lets investigate further.\n" +
                                        "1. Inside the Broken Globe in the middle of the room.\n" +
                                        "2. A Random Purse on the floor\n" +
                                        "3. A Dead body that's stapled to the wall\n" +
                                        "4. Leave the room.");
                var userInput = ReadLine();
                switch (userInput)
                {
                    case "1":
                        Clear();
                        GameUtilities.TellTheStory("You look inside...NOTHING...");
                        PressAnyKeyToContinue();
                        break;

                    case "2":
                        Clear();
                        GameUtilities.TellTheStory("You look inside...Random stuff...");
                        PressAnyKeyToContinue();
                        break;

                    case "3":
                        GameUtilities.TellTheStory("You move it around and .... Yuck! Its head falls off!");
                        PressAnyKeyToContinue();
                        break;

                    case "4":
                        Clear();
                        GameUtilities.TellTheStory("You exit the room.");
                        PressAnyKeyToContinue();
                        hasLeftRoom = true;
                        LoadSecondChallenge();
                        break;

                    default:
                        WriteLine("Invalid Selection.");
                        break;
                }
            }
        }


        private void LoadTheRoomDownTheHall_ToTheRight()
        {
            bool hasLeftRoom = false;
            while (!hasLeftRoom)
            {
                Clear();
                GameUtilities.TellTheStory("You enterd the room. Its just a basic room.\nLets investigate further...\n" +
                "1. Inside a coffie cup?\n" +
                "2. A Shiny Box (it looks like a puzzle box)\n" +
                "3. A dead body that's slumped over the fireplace.\n" +
                "4. Leave the room.");

                var userInput = ReadLine();

                switch (userInput)
                {
                    case "1":
                        Clear();
                        GameUtilities.TellTheStory("You look inside...NOTHING.");
                        PressAnyKeyToContinue();
                        break;

                    case "2":
                        Clear();
                        GameUtilities.TellTheStory("You rub the box... It reconfigures itself..I looks like what we have been looking for!");
                        _hasPuzzlePiece = true;
                        hasLeftRoom = true;
                        PressAnyKeyToContinue();
                        break;

                    case "3":
                        Clear();
                        GameUtilities.TellTheStory("You move it around and... Yuck its head falls off!");
                        PressAnyKeyToContinue();
                        break;

                    case "4":
                        Clear();
                        GameUtilities.TellTheStory("You exit the room!");
                        PressAnyKeyToContinue();
                        hasLeftRoom = true;
                        LoadSecondChallenge();
                        break;

                    default:
                        WriteLine("Invalid Selection");
                        break;
                }
            }
        }


        private void ClearChallengCounter()
        {
            _challengeCounter = 0;
        }

        private void LoadFirstChallenge()
        {
            ClearChallengCounter();
            Clear();

            var currentChallenge = _house.FloorsInHouse[(int)HauntedHouse.Data.Entities.ChallengesIndex.FirstChallenge].Challenges[++_challengeCounter];

            GameUtilities.TellTheStory(currentChallenge.ChallengeDescription);

            GameUtilities.TellTheStory("Which Room will you select?\n" +
                                         "1. Room on the Left\n" +
                                         "2. Room on the Right\n");

            var userInput = ReadLine();
            switch (userInput)
            {
                case "1":
                    YouChoseTheLeftRoom();
                    break;
                case "2":
                    YouChoseTheRightRoom();
                    break;
                default:
                    WriteLine("Invalid Selection.");
                    break;
            }
        }

        private void YouChoseTheRightRoom()
        {
            bool hasLeftRoom = false;
            while (!hasLeftRoom)
            {
                Clear();
                GameUtilities.TellTheStory("You Entered the Right Room. Its the Kitchen, and its a mess. But, lets investigate further. Where do you want to look?\n" +
                "1. In the Refrigerator\n" +
                "2. On top of the Kitchen Island\n" +
                "3. In the Lower Cabinets\n" +
                "4. Leave The Room");

                var userInput = ReadLine();
                switch (userInput)
                {
                    case "1":
                        Clear();
                        GameUtilities.TellTheStory("You open the refrigerator door...NOTHING...");
                        PressAnyKeyToContinue();
                        break;
                    case "2":
                        Clear();
                        GameUtilities.TellTheStory("You look on top of the Kitchen Island. Its completely covered with random stuff..");
                        PressAnyKeyToContinue();
                        break;
                    case "3":
                        Clear();
                        GameUtilities.TellTheStory("You check the Lower Cabinets...Again Theres NOTHING...");
                        PressAnyKeyToContinue();
                        break;
                    case "4":
                        Clear();
                        GameUtilities.TellTheStory("You Exit the Room.");
                        PressAnyKeyToContinue();
                        hasLeftRoom = true;
                        LoadFirstChallenge();
                        break;
                    default:
                        WriteLine("Invalid Selection");
                        break;
                }
            }
        }


        private void YouChoseTheLeftRoom()
        {
            bool hasLeftRoom = false;

            while (!hasLeftRoom)
            {
                Clear();
                GameUtilities.TellTheStory("You Entered the left room\nIts the Living Room, and its a mess.\nBut lets investigate further.\nWhere do you want to look.\n" +
                 "1. On the couch\n" +
                 "2. On the coffie table\n" +
                 "3. Inside the broken television\n" +
                 "4. Leave the room\n");

                var userInput = ReadLine();

                switch (userInput)
                {
                    case "1":
                        GameUtilities.TellTheStory("You check the couch...Nothing!");
                        PressAnyKeyToContinue();
                        break;

                    case "2":
                        GameUtilities.TellTheStory("You check the Coffie Table...Nothing! You take a look at the couch and see a shiny object!");
                        PressAnyKeyToContinue();
                        break;

                    case "3":
                        GameUtilities.TellTheStory("You check inside the broken Television Screen...You found the Middle Room Key!");
                        _hasMiddleRoomKey = true;
                        GameUtilities.TellTheStory("You Exit the room");
                        hasLeftRoom = true;
                        PressAnyKeyToContinue();
                        break;

                    case "4":
                        GameUtilities.TellTheStory("You exit the Room!");
                        PressAnyKeyToContinue();
                        hasLeftRoom = true;
                        LoadFirstChallenge();
                        break;

                    default:
                        WriteLine("Invalid Selection!");
                        break;
                }
            }
        }
    }
}