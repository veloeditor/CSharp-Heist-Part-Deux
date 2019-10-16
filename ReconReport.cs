using System;

namespace HeistPartDuex {
    public class ReconReport {
        public string BankSystemName {get; set;}
        public int BankSystemScore {get; set;}

        public ReconReport(string bankSystemName, int bankSystemScore)  {
            BankSystemName = bankSystemName;
            BankSystemScore = bankSystemScore;
        }

    }
}