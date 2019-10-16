using System;

namespace HeistPartDuex {
    public class LockSpecialist : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }
        public string Specialty {get; set;}

        public LockSpecialist(string name, int skillLevel, int percentageCut) {
            Name = name;
            SkillLevel = skillLevel;
            PercentageCut = percentageCut;
            Specialty = "Safe Cracker";

        }


        public void PerformSkill(Bank bank)
        {
             // Take the Bank parameter and decrement its appropraite security score by the SkillLevel. i.e. A Hacker with a skill level of 50 should decrement the bank's AlarmScore by 50.
            bank.AlarmScore = bank.AlarmScore - SkillLevel;

            // Print to the console the name of the robber and what action they are performing. i.e. "Mr. Pink is hacking the alarm system. Decreased security 50 points"
            Console.WriteLine($"{Name} is cracking the safe's security system. Security has been descreased by {SkillLevel} points.");

            // If the appropriate security score has be reduced to 0 or below, print a message to the console, i.e. "Mr Pink has disabled the alarm system!"
            if (bank.AlarmScore <= 0) {
                Console.WriteLine($"{Name} has disabled the safe's locking mechanism and opened the lock!!");
            }
        }
    }
}