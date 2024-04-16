﻿using Studio.API.Business.Domain.CQRS.Commands.Base;
using Studio.API.Business.Domain.Entities.Locations;
using Studio.API.Business.Domain.Models.Locations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studio.API.Business.Domain.CQRS.Commands.Locations
{
    public class LocationUpdateCommand : UpdateCommand<LocationView>
    {
        public string Location_Name { get; set; }
        public Guid City_Id { get; set; }

        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
