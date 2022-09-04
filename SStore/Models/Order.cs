using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SStore.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime FirstName { get; set; }
        public bool Status { get; set; }
        public bool Delivered { get; set; }
        public Nullable<DateTime> DeliveredDate { get; set; }
        [ForeignKey("UserInfo")]
        public string UserId { get; set; }
        public UserInfo UserInfo { get; set; }
        [StringLength(100)]
        public string PaymentType { get; set; }
        public bool PaymentStatus { get; set; }

    }
}