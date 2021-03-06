﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.API.InputModels
{
    public class AuctionInputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000000000000)]
        public decimal? InitialValue { get; set; }

        [Required]
        public bool? IsUsed { get; set; }

        [Required]
        [StringLength(200)]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? InitialDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
