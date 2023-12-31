﻿using System.ComponentModel.DataAnnotations.Schema;

namespace OptimunLegalServices.Entities
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string? Value { get; set; }

        [NotMapped]

        public IFormFile? Image { get; set; }
    }
}
