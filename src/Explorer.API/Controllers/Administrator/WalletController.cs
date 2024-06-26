﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Route("api/administrator/wallet")]
    public class WalletController : BaseApiController
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("{id:int}")]
        public ActionResult<WalletDto> Get(int id)
        {
            var result = _walletService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public ActionResult<PagedResult<TourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _walletService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<WalletDto> AddAC([FromBody] WalletDto wallet)
        {
            var result = _walletService.AddAC(wallet);
            return CreateResponse(result);
        }

        [HttpGet("byUser")]
        public ActionResult<WalletDto> GetByUserId()
        {
            var userIdClaim = HttpContext.User.Claims.First(x => x.Type == "id");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int id))
            {

                var result = _walletService.GetWalletByUserId(id);
                return CreateResponse(result);
            }
            else
            {
                return BadRequest("User ID not found or invalid.");
            }
            
        }
        
            

    }
}
