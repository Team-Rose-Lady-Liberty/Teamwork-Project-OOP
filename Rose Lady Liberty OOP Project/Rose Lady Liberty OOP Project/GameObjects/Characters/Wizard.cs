namespace RoseLadyLibertyOOPProject.GameObjects.Characters
{
    public class Wizard : Character
    {
        public Wizard(string id, int x, int y, int health, int attack, int defense)
            : base(id, x, y, health, attack, defense)
        {

        }

        public Wizard(string id, int x, int y, int health, int attack, int defense, int mana)
            : this(id, x, y, health, attack, defense)
        {
            this.Mana = mana;
        }

        public int Mana { get; set; }
    }
}
