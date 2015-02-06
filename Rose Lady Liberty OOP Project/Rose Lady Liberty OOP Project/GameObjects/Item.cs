namespace RoseLadyLibertyOOPProject.GameObjects
{
    abstract class Item : GameObject
    {
        public Item(string id, int x, int y, int healthEffect, int defenseEffect, int attackEffect)
            : base(id, x, y)
        {
            this.HealthEffect = healthEffect;
            this.DefenseEffect = defenseEffect;
            this.AttackEffect = attackEffect;
        }

        public int HealthEffect { get; set; }

        public int DefenseEffect { get; set; }

        public int AttackEffect { get; set; }
    }
}
