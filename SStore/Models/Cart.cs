using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SStore.Models
{
    public class Cart
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CardId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("UserInfo")]
        public string UserId { get; set; }
        public UserInfo UserInfo { get; set; }

    }
}