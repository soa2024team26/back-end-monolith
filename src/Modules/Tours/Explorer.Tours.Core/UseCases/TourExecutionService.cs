﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;

namespace Explorer.Tours.Core.UseCases
{
    public class TourExecutionService : CrudService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private readonly ICheckpointVisitedRepository _checkpointVisitedRepository;
        public TourExecutionService(ITourExecutionRepository tourExecutionRepository, ICheckpointVisitedRepository checkpointVisitedRepository, ICrudRepository<TourExecution> repository, IMapper mapper) : base(repository, mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
            _checkpointVisitedRepository = checkpointVisitedRepository;
        }
        public Result StartTour(TourExecutionDto dto)
        {
            TourExecution tourExecution = new TourExecution() {TouristId = dto.TouristId, TourId = dto.TourId, StartTime = dto.StartTime, 
                                                               EndTime = dto.EndTime, Completed = dto.Completed, Abandoned = dto.Abandoned,
                                                               CurrentLatitude = dto.CurrentLatitude, CurrentLongitude = dto.CurrentLongitude, LastActivity = dto.LastActivity, VisitedCheckpoints = dto.VisitedCheckpoints};
            tourExecution = _tourExecutionRepository.Create(tourExecution);
            CheckpointVisited cp = new CheckpointVisited(tourExecution.TouristId, tourExecution.VisitedCheckpoints[0], DateTime.UtcNow);
            _checkpointVisitedRepository.Add(cp);

            return Result.Ok();
        }

        public Result<TourExecutionDto> CompleteTour(int tourExecutionId)
        {
            TourExecution tourExecution = _tourExecutionRepository.Get(tourExecutionId);
            tourExecution.Completed = true;
            tourExecution.EndTime = DateTime.UtcNow;
            _tourExecutionRepository.Update(tourExecution);
            return Result.Ok();
        }

        public Result<TourExecutionDto> AbandonTour(int tourExecutionId)
        {
            TourExecution tourExecution = _tourExecutionRepository.Get(tourExecutionId);
            tourExecution.Abandoned = true;
            tourExecution.EndTime = DateTime.UtcNow;
            _tourExecutionRepository.Update(tourExecution);
            return Result.Ok();
        }

        public Result<TourExecutionDto> GetTourExecution(int userId)
        {
            Result<PagedResult<TourExecutionDto>> pagedResult = GetPaged(1, int.MaxValue);
            TourExecutionDto foundItem;
            if (pagedResult.IsSuccess)
            {
                PagedResult<TourExecutionDto> pagedData = pagedResult.Value;
                List<TourExecutionDto> allItems = pagedData.Results;
                foundItem = allItems.FirstOrDefault(dto => dto.TouristId == userId && dto.EndTime == null);
            }
            else
            {
                return new Result<TourExecutionDto>();
            }

            return foundItem;
        }

        public Result<TourExecutionDto> CompleteCheckpoint(int userId, List<CheckPointDto> checkpoints)
        {
            Result<PagedResult<TourExecutionDto>> pagedResult = GetPaged(1, int.MaxValue);
            TourExecutionDto foundItem;
            if (pagedResult.IsSuccess)
            {
                PagedResult<TourExecutionDto> pagedData = pagedResult.Value;
                List<TourExecutionDto> allItems = pagedData.Results;
                foundItem = allItems.FirstOrDefault(dto => dto.TouristId == userId && dto.EndTime == null);
            }
            else
            {
                return new Result<TourExecutionDto>();
            }

            foreach(CheckPointDto checkPoint in checkpoints)
            {
                double dLat = ToRadians(checkPoint.Latitude - foundItem.CurrentLatitude);
                double dLon = ToRadians(checkPoint.Longitude - foundItem.CurrentLongitude);

                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                           Math.Cos(ToRadians(foundItem.CurrentLatitude)) * Math.Cos(ToRadians(checkPoint.Latitude)) *
                           Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                
                if( c * 6371.0 < 0.15) // 150 metara
                {               
                    if(_checkpointVisitedRepository.GetVisitedCheckpoint(userId, checkPoint.Id) == null ) 
                    {
                        foundItem.VisitedCheckpoints.Add(checkPoint.Id);
                        CheckpointVisited cp = new CheckpointVisited(userId, checkPoint.Id, DateTime.UtcNow);
                        _checkpointVisitedRepository.Add(cp);
                        
                    }                   
                }
            }
            foundItem.LastActivity = DateTime.UtcNow;
            //Update(foundItem);
            return foundItem;
        }

        private double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }
    }

}