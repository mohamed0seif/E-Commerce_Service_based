using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamati.DAL.Models
{
    [Table("Chat_Message")]
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Sender_ID { get; set; }
        public int Receiver_ID { get; set; }

        [ForeignKey("Receiver_ID")]
        [InverseProperty("Chat_MessageReceivers")]
        public virtual SiteUser? Receiver { get; set; }
        [ForeignKey("Sender_ID")]
        [InverseProperty("Chat_MessageSenders")]
        public virtual SiteUser? Sender { get; set; }
    }
}
