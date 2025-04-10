﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SET_Management.Models.Entity
{
    [Table("mstUsers")]
    public class mstUser
    {
        [Key]
        public int mstUserId { get; set; }
        [Required]
        [MaxLength(255)]
        public string userName { get; set; }
        public string? euid { get; set; }
        public int userTypeId { get; set; } = 0;
        public string? userType { get; set; }
        [Required]
        [MaxLength(255)]
        public string userPassword { get; set; }
        [Required]
        [MaxLength(255)]
        public string userEmail { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public string? Experience { get; set; }
        public string? usersId { get; set; }
        public int? createdBy { get; set; }
        public DateTime createdDate { get; set; } = DateTime.Now;
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
        public bool isActive { get; set; } = true;
    }
}

