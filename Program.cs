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
            LockSpecialist lockpicker1 = new LockSpecialist("Kevin", 85, 30);
            LockSpecialist lockpicker2 = new LockSpecialist("Kevin", 70, 20);

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
                Console.WriteLine("1) Muscle (Disables guards)");
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


              






        }
    }
}
