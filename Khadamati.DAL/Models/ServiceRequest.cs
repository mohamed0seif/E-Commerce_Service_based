﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Khadamati.DAL
{
    [PrimaryKey("Id")]
    public class ServiceRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public SiteUser? User { get; set; }
        public int? ServiceId { get; set; }
        public Service? Service { get; set; }
        public string? ProviderId { get; set; }
        public SiteUser? Provider { get; set; }
        public string RequestText { get; set; } = string.Empty;
        public DateTime date { get; set; }   
        public string Status { get; set; }
}
}
