namespace CupOnlineAPI.Models
{
    public class OrderCup
    {
        public int id { get; set; }
        public string name { get; set; }
        public Organizer organizer { get; set; }
        public Sport sport { get; set; }
        public string age { get; set; }
        public string city { get; set; }
        public string date { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int cupType { get; set; }






    }
}
