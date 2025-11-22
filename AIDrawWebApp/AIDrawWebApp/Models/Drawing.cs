namespace AIDrawWebApp.Models
{
    public class Drawing
    {
        public int id { get; set; }
        public string name { get; set; }
        public string strokeData { get; set; }
        public DateTime createdAt { get; set; }
    }
}
