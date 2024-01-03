﻿using Raythos.DTOs.Aircrafts;
using System.ComponentModel.DataAnnotations;

namespace Raythos.DTOs.Private
{
    public class CartDto
    {
        public long Id { get; set; }

        [Required]
        public long? AircraftId { get; set; }

        [Required]
        public long? UserId { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Required]
        public decimal? TotalPrice { get; set; }
        public string? Customizations { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CartAircraftDto Aircraft { get; set; } = null!;
    }
}
