﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NM.Studio.Domain.Contracts.Repositories.Locations;
using NM.Studio.Domain.Contracts.Services.Locations;
using NM.Studio.Domain.Contracts.UnitOfWorks;
using NM.Studio.Domain.Entities.Locations;
using NM.Studio.Domain.Results.Locations;
using NM.Studio.Domain.Results.Messages;
using NM.Studio.Domain.Utilities;
using NM.Studio.Services.Bases;

namespace NM.Studio.Services.Locations
{
    public class CountryService : BaseService<Country>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _countryRepository = unitOfWork.CountryRepository;
        }

        public async Task<MessageResults<CountryResult>> GetAll(CancellationToken cancellationToken = default)
        {
            var countries = await _countryRepository.GetAllWithInclude(cancellationToken);
            // map 
            var content = _mapper.Map<IList<Country>, List<CountryResult>>(countries);
            var msgResults = AppMessage.GetMessageResults<CountryResult>(content);

            return msgResults;
        }
    }
}
