﻿using Studio.API.Business.Domain.CQRS.Commands.Base;
using Studio.API.Business.Domain.Models.Locations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studio.API.Business.Domain.CQRS.Commands.Locations
{
    public class CityCreateCommand : CreateCommand<CityView>
    {
        public string CityName { get; set; }

        public Guid CountryId { get; set; }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}