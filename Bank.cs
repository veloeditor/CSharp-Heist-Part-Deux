using System;

namespace HeistPartDuex {
    public class Bank {
        public int CashOnHand {get; set;}
        public int AlarmScore {get; set;}
        public int VaultScore {get; set;}
        public int SecurityGuardScore {get; set;}
        public bool IsSecure {get; set;}

        public void BankSecurity() {
            if(AlarmScore <= 0 && VaultScore <= 0 && SecurityGuardScore <= 0) {
                IsSecure = false;
            } else if (AlarmScore >= 0 || VaultScore >= 0 || SecurityGuardScore >= 0) {
                IsSecure = true;
            };
        }

        //The program should create a new bank object and randomly assign values for these properties:
        // AlarmScore (between 0 and 100)
        // VaultScore (between 0 and 100)
        // SecurityGuardScore (between 0 and 100)
        // CashOnHand (between 50,000 and 1 million)
        public Bank() {
            Random random = new Random();

            AlarmScore = random.Next(1, 101);
            VaultScore = random.Next(1, 101);
            SecurityGuardScore = random.Next(1, 101);
            CashOnHand = random.Next(50_000, 1_000_000);
        }
    }
}