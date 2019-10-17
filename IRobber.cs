using System;

namespace HeistPartDuex {
    public interface IRobber
    {
        public string Name {get; set;}
        public int SkillLevel {get; set;}
        public int PercentageCut {get; set;}
        string Specialty {get; set;}
        void PerformSkill(Bank bank);
    }
}