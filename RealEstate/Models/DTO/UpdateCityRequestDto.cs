﻿using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.DTO
{
    public class UpdateCityRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maxinum of 100 characters")]
        public string Name { get; set; }
        [Required]
        public int NationId { get; set; }
    }
}
