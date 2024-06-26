﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize]
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-problem")]
    public class TourProblemTouristController : BaseApiController
    {
        private readonly ITourProblemService _tourProblemService;
        private readonly ITourProblemResponseService _problemResponseService;

        public TourProblemTouristController(ITourProblemService tourProblemService, ITourProblemResponseService problemResponseService)
        {
            _tourProblemService = tourProblemService;
            _problemResponseService = problemResponseService;
        }

        [HttpGet("by-tourist/{touristId:int}")]
        public ActionResult<PagedResult<TourProblemDto>> GetAll(int touristId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourProblemService.GetByTouristId(touristId, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<TourDto> Get(int id)
        {
            var result = _tourProblemService.Get(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourProblemDto> Create([FromBody] TourProblemDto tourProblem)
        {
            var result = _tourProblemService.Create(tourProblem);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourProblemDto> Update([FromBody] TourProblemDto tourProblem)
        {
            var result = _tourProblemService.Update(tourProblem);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourProblemService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPost("{id}/respond")]
        public ActionResult RespondToProblem(int id, [FromBody] TourProblemResponseDto tourProblemResponse)
        {
            var result = _problemResponseService.Create(tourProblemResponse);
            return CreateResponse(result);
        }

        [HttpGet("{problemId:int}/responses")]
        public ActionResult<IEnumerable<TourProblemResponseDto>> GetProblemResponses(int problemId)
        {
            var result = _problemResponseService.GetProblemResponses(problemId);
            return CreateResponse(result);
        }


        [HttpPost("problemSolved")]
        public ActionResult<TourProblemDto> ProblemSolved([FromBody] TourProblemDto tourProblem)
        {
            var result = _tourProblemService.Update(tourProblem);
            return CreateResponse(result);
        }

        [HttpPost("problemUnsolved")]
        public ActionResult<TourProblemDto> ProblemUnsolved([FromBody] TourProblemDto tourProblem)
        {
            var result = _tourProblemService.Update(tourProblem);
            return CreateResponse(result);
        }

        [HttpGet("tourist/{touristId:int}/responses")]
        public ActionResult<IEnumerable<TourProblemResponseDto>> GetTourProblemResponsesForTourist(int touristId)
        {
            var result = _problemResponseService.GetTourProblemResponsesForUser(touristId);
            return CreateResponse(result);
        }
    }
}