﻿using Studio.API.Business.Domain.Entities.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studio.API.Business.Domain.Entities.Locations
{
    [Table("Country")]
    public class Country : BaseEntity
    {
        [Column(TypeName = "nvarchar(255)")]
        public string Country_Name { get; set; }

        public virtual IList<City> Cities { get; set; }
    }
}