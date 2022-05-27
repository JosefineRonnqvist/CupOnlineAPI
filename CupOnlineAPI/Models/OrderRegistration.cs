using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace CupOnlineAPI.Models
{
    [Table("td_order")]
    public class OrderRegistration
    {
        [Key]
        public int id { get; set; }
        public int? cup_id { get; set; }
        [Required]
        public string? message { get; set; } = "";
        public string? invoiceAddress { get; set; } = "";
        public DateTime? registrationDate { get; set; } = DateTime.Now;
        public DateTime? payDate { get; set; }
        public int? orderStatus { get; set; } = 3;
        public int? foundType { get; set; } = 0;
        public string? regIp { get; set; } = "";
        public int? status { get; set; } = 0;
        public float? payAmount { get; set; } 
    }
}
