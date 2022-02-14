using Dapper.Contrib.Extensions;
using System.Globalization;

namespace CupOnlineAPI.Models
{
    [Table("td_cups")]
    public class Cup
    {
        private string _startdate;
        private string _enddate;

        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string age { get; set; }
        public string city { get; set; }
        internal string cup_startdate
        {
            get { return _startdate; }
            set
            {
                _startdate = value;
                try
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    startdate = DateTime.ParseExact(value, "yyyy-MM-dd", provider).Date;
                }
                catch(FormatException)
                {
                    startdate = DateTime.MinValue;
                }
            }
        }

        public DateTime startdate { get; set; }

        internal string cup_enddate { 
          get { return _enddate; }
            set
            {
                _enddate = value;
                try
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    enddate = DateTime.ParseExact(value, "yyyy-MM-dd", provider).Date;
                }
                catch(FormatException)
                {
                    enddate = DateTime.MinValue;
                }
            }
        }

        public DateTime enddate { get; set; }
        public string date { get; set; }
        public string sport_name { get; set; }
        public string organizer { get; set; } 
    }
}
