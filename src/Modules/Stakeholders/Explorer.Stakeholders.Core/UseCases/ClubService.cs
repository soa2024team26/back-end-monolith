﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        public ClubService(ICrudRepository<Club> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<ClubDto> Kick(ClubDto club)
        {
            throw new NotImplementedException();
        }

        public Result<ClubDto> GetClubById(int id)
        {
            try
            {
                var club = CrudRepository.Get(id);

                if (club == null)
                {
                    return Result.Fail<ClubDto>(FailureCode.NotFound).WithError("Request not found");
                }

                var clubDto = MapToDto(club);
                return Result.Ok(clubDto);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
