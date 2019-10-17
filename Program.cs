using System;
using System.Collections.Generic;
using System.Linq;

namespace HeistPartDuex
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create the team options here
            LockSpecialist lockpicker1 = new LockSpecialist("George", 85, 30);
            LockSpecialist lockpicker2 = new LockSpecialist("Tammy", 70, 20);

            Hacker hacker1 = new Hacker("Will", 89, 30);
            Hacker hacker2 = new Hacker("Curtis", 67, 20);

            Muscle muscle1 = new Muscle("Kevin", 84, 25);
            Muscle muscle2 = new Muscle("Michelle", 72, 20);
            
            //List of all of our potential team members to rob the bank
            List<IRobber> rolodex = new List<IRobber>() {
              lockpicker1, lockpicker2, hacker1, hacker2, muscle1, muscle2  
            };

            //Start with printing out the number of current operatives in the roladex
            Console.WriteLine($"Current number of operatives available: {rolodex.Count}");
            
            // Enter a new contact's name
            Console.WriteLine("Enter the name of a new contact here> ");
            string newContact = Console.ReadLine();
            Console.WriteLine();

            //Let them enter contacts until they enter a blank name
            while(newContact != "") {
                Console.WriteLine("Select the number for their skillset> ");
                Console.WriteLine("1) Hacker (Disables alarms)");
                Console.WriteLine("2) Lock Specialist (Cracks safes)");
                Console.WriteLine("3) Muscle (Disables guards)");
                Console.WriteLine();

                //store user input in to variable
                string userChoice = Console.ReadLine();

                //prompt user to enter a skill level and store that response as an int
                Console.WriteLine("Enter the contact's skill level here (a number between 1-100)");
                int skillLevel = int.Parse(Console.ReadLine());

                //prompt user to enter the percentage cut of contact and store that as an int
                Console.WriteLine("Enter the contact's perecentage cut they require to participate (a number between 1-100)");
                int percentageCut = int.Parse(Console.ReadLine());

                //we must figure out what the user entered and create that entry
                if (userChoice == "1") {
                    rolodex.Add(new Hacker(newContact, skillLevel, percentageCut));
                } else if (userChoice == "2") {
                    rolodex.Add(new LockSpecialist(newContact, skillLevel, percentageCut));
                } else if (userChoice == "3")  {
                    rolodex.Add(new Muscle(newContact, skillLevel, percentageCut));
                }

                //prompt them to enter another contact (if this is left blank the while loop ends)
                Console.WriteLine("Enter the name of another contact> ");
                newContact = Console.ReadLine();
            }

            //create a new bank object (this will create a new bank with random assigned values)
            Bank newBankToHit = new Bank();

            // Let's do a little recon next. Print out a Recon Report to the user. This should tell the user what the bank's most secure system is, and what its least secure system is (don't print the actual integer scores--just the name, i.e. Most Secure: Alarm Least Secure: Vault
            
            Dictionary<string, int> bankReconReport = new Dictionary<string, int>(); 

            bankReconReport.Add("Alarm System:", newBankToHit.AlarmScore);
            bankReconReport.Add("Vault:", newBankToHit.VaultScore);
            bankReconReport.Add("Security Guards:", newBankToHit.SecurityGuardScore);
            

            //most secure system
            int highestScore = bankReconReport.Max(kvp => kvp.Value);
            string mostSecure = bankReconReport.First(kvp => kvp.Value == highestScore).Key;

            //least secure system
            int lowestScore = bankReconReport.Min(kvp => kvp.Value);
            string leastSecure = bankReconReport.First(kvp => kvp.Value == lowestScore).Key;

            //Print out report to console
            Console.WriteLine("----Bank Recon Report----");
            Console.WriteLine($"Most Secure Security: {mostSecure}");
            Console.WriteLine($"Least Secure System: {leastSecure}");

            //Create a crew list
            List<IRobber> crew = new List<IRobber>();

            //Print out the list of crew with their specialty, skill level and percentage of cut. Include index number so user can select them in next step.
            Console.WriteLine("Your options for crew:");
            for (int i = 0; i < rolodex.Count(); i++) {
                Console.WriteLine($"{i}. {rolodex[i].Name}");
                Console.WriteLine($"Specialty: {rolodex[i].Specialty}");
                Console.WriteLine($"Skill Level: {rolodex[i].SkillLevel}");
                Console.WriteLine($"Percentage Cut Required: {rolodex[i].PercentageCut}");
                Console.WriteLine(); 
            }

            //User selects the first member of the team
            Console.WriteLine("Enter the number for the operative you want to add> ");
            string operativeChoice = Console.ReadLine();

            //establish variable for percentage of take remaining
            int percentageLeft = 100;

            //while loop
            while (operativeChoice != "" && percentageLeft > 0) {
                int crewIndex = int.Parse(operativeChoice);
                percentageLeft = percentageLeft - rolodex[crewIndex].PercentageCut;
                crew.Add(rolodex[crewIndex]);
                Console.WriteLine($"Remaining Cut: {percentageLeft}");
                Console.WriteLine();

                //Print out the report after each crew member is selected
                for (int i = 0; i < rolodex.Count(); i++) {
                    if (percentageLeft >= rolodex[i].PercentageCut && crew.Contains(rolodex[i]) == false) {
                        Console.WriteLine($"{i}. {rolodex[i].Name}");
                        Console.WriteLine($"Specialty: {rolodex[i].Specialty}");
                        Console.WriteLine($"Skill Level: {rolodex[i].SkillLevel}");
                        Console.WriteLine($"Percentage Cut Required: {rolodex[i].PercentageCut}");
                        Console.WriteLine(); 
                    }
                }

                //Select another operative
                Console.WriteLine("Enter the number to select your next member of the crew");
                operativeChoice = Console.ReadLine();
            }

            //Begin the heist
            //Crew member should perform their skill so foreach through the crew

            foreach(IRobber robber in crew) {
                robber.PerformSkill(newBankToHit);
            }
            if (newBankToHit.IsSecure) {
                Console.WriteLine("Unfortunately you failed!");
            } else {
                //if the bank reports back with 0 or less left we won
                Console.WriteLine("Awesome, you pulled it off and are rich!");
                // If the heist was a success, print out a report of each members' take, along with how much money is left for yourself.
                Console.WriteLine("FINAL REPORT:");
                Console.WriteLine($"Total Haul: {newBankToHit.CashOnHand}");
                int totalCash = newBankToHit.CashOnHand;
                //this will foreach through each crew taking out their cut to see what's left
                foreach(IRobber robber in crew) {
                    int crewTake = newBankToHit.CashOnHand * robber.PercentageCut/100;
                    totalCash -= crewTake;
                    Console.WriteLine($"{robber.Name} got {crewTake}");
                }
                //this is outside of the above foreach so the amount of cash left is our take
                Console.WriteLine($"You're taking home {totalCash}");
            }


 
              






        }
    }
}
