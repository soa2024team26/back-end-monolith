﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Explorer.API.Controllers.Author
{
    [Route("api/author/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly IPublicRequestService _publicRequestService;

        private readonly IWebHostEnvironment _environment;
        public TourController(ITourService tourService, IPublicRequestService publicRequestService, IWebHostEnvironment environment)
        {
            _tourService = tourService;
            _publicRequestService = publicRequestService;
            _environment = environment;
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> Get(int id)
        {
            var result = _tourService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] TourDto tour)
        {
            var result = _tourService.Create(tour);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourDto> Update([FromBody] TourDto tour)
        {
            if(tour.Status == Tours.API.Dtos.AccountStatus.PUBLISHED) 
            {
                tour.PublishTime = DateTime.UtcNow;
            }
            var result = _tourService.Update(tour);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPost("tourEquipment/{tourId:int}/{equipmentId:int}")]
        public ActionResult<TourDto> AddEquipmentToTour([FromBody] TourDto tour, int equipmentId)
        {

            var result = _tourService.AddEquipmentToTour(tour, equipmentId);
            return CreateResponse(result);
        }

        [HttpPut("remove/{tourId:int}/{equipmentId:int}")]
        public ActionResult<TourDto> RemoveEquipmentFromTour([FromBody] TourDto tour, int equipmentId)
        {
            var result = _tourService.RemoveEquipmentFromTour(tour, equipmentId);
            return CreateResponse(result);
        }

        [HttpPut("{tourId:int}/{checkPointId:int}")]
        public ActionResult<TourDto> AddCheckPoint([FromBody] TourDto tour, int checkPointId)
        {
            var result = _tourService.AddCheckPoint(tour, checkPointId);
            return CreateResponse(result);
        }

        [HttpPut("delete/{tourId:int}/{checkPointId:int}")]
        public ActionResult<TourDto> DeleteCheckPoint([FromBody] TourDto tour, int checkPointId)
        {
            var result = _tourService.DeleteCheckPoint(tour, checkPointId);
            return CreateResponse(result);
        }

        [HttpPost("addObject/{tourId:int}/{tourObject:int}")]
        public ActionResult<TourDto> AddObjectToTour([FromBody] TourDto tour, int tourObjectId)
        {

            var result = _tourService.AddObjectToTour(tour, tourObjectId);
            return CreateResponse(result);
        }

        [HttpPost("removeObject/{tourId:int}/{tourObject:int}")]
        public ActionResult<TourDto> RemoveObjectFromTour([FromBody] TourDto tour, int tourObjectId)
        {

            var result = _tourService.RemoveObjectFromTour(tour, tourObjectId);
            return CreateResponse(result);
        }

        [HttpGet("average-grade/{tourId:int}")]
        public ActionResult<AverageGradeDto> GetAverageGrade(int tourId)
        {
            var averageGrade = _tourService.GetAverageGradeForTour(tourId);
            return CreateResponse(averageGrade);
        }

        [HttpGet("average-weekly-grade/{tourId:int}")]
        public ActionResult<AverageGradeDto> GetAverageWeeklyGrade(int tourId)
        {
            var averageGrade = _tourService.GetAverageWeeklyGradeForTour(tourId);
            return CreateResponse(averageGrade);
        }

        [HttpPut("publish/{tourId:int}")]
        public ActionResult<TourDto> PublishTour([FromBody] TourDto tour)
        {
            var result = _tourService.PublishTour(tour);
            return CreateResponse(result);
        }

        [HttpPut("archive/{tourId:int}")]
        public ActionResult<TourDto> ArchiveTour([FromBody] TourDto tour)
        {
            var result = _tourService.ArchiveTour(tour);
            return CreateResponse(result);
        }

        [HttpPost("publicRequest")]
        public ActionResult<PublicRequestDto> SendPublicRequest([FromBody] PublicRequestDto request)
        {
            var result = _publicRequestService.Create(request);
            return CreateResponse(result);
        }

        [HttpGet("byAuthor/{authorId}")]
        public ActionResult<TourBundleDto> GetToursByAuthorId(int authorId)
        {
            var toursDto = _tourService.GetToursByAuthorId(authorId);

            if (toursDto == null || !toursDto.Any())
            {
                return NotFound("No tours found for author");
            }

            var result = Result.Ok(toursDto); 

            return CreateResponse(result); 
        }

        [HttpPut("active-tours")]
        public ActionResult<PagedResult<TourDto>> GetActiveTours([FromBody] List<int> tourIds)
        {
            // Convert List<int> to List<long>
            List<long> convertedTourIds = tourIds.Select(id => (long)id).ToList();

            var result = _tourService.GetActiveTours(convertedTourIds);
            return CreateResponse(result);
        }

        [HttpPost("uploadTourImage")]
        public async Task<string> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string fName = file.FileName;
                string path = Path.Combine(_environment.ContentRootPath, "wwwroot/Images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return $"{file.FileName} successfully uploaded to the Server";
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

