using Dapper.Contrib.Extensions;

namespace CupOnlineAPI.Models
{
    [Table("td_order")]
    public class OrderRegistration
    {
        [Key]
        public int id { get; set; }
        public int cup_id { get; set; }
        public string message { get; set; }
        public string invoiceAddress { get; set; }
        public string registrationDate { get; set; }
        public string payDate { get; set; }
        public string orderStatus { get; set; }
        public string foundType { get; set; }
        public string regIp { get; set; }
        public string status { get; set; }
        public string payAmount { get; set; }
    }
}
