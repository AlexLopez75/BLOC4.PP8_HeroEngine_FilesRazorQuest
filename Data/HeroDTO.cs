namespace HeroEngine.Core.Data
{
    public class HeroDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public int MaxHp { get; set; }
        public int Power { get; set; }
        public int Defense { get; set; }

        public int Armor { get; set; }
        public string BattleCry { get; set; }

        public int MaxMana { get; set; }
        public int ArchLevel { get; set; }
        public List<AbilityDTO> Abilities { get; set; }

        public int MultDamage { get; set; }
        public int NumDaggers { get; set; }
    }
}