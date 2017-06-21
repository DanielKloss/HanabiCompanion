namespace HanabiCompanion.UI.Models
{
    public class SuitColour
    {
        public string name { get; set; }
        public int score { get; set; }
        public string image { get; set; }

        public SuitColour(string Name)
        {
            name = Name;
            score = 0;
            image = "Assets/Colours/" + Name;
        }
    }
}
