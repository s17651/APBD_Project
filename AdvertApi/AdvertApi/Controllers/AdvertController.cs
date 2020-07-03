using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.DTOs;
using AdvertApi.Exceptions;
using AdvertApi.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class AdvertController : ControllerBase
    {

        private readonly IDbAdvertService _context;

        public AdvertController(IDbAdvertService context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("new_client")]
        public IActionResult AddClient(AddClientRequest request) {
            try
            {
                var result = _context.AddClient(request);
                return Ok(result);
            }
            catch (ClientAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (LoginAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("refresh_token/{token}")]
        public IActionResult RefRefreshToken(string token)
        {
            try
            {
                var result = _context.RefreshToken(token);
                return Ok(result);
            }
            catch (RefreshTokenException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                var result = _context.Login(request);
                return Ok(result);
            }
            catch (LoginOrPasswordException e) 
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("campaign")]
        //[Authorize(Roles = "client")]
        //[Authorize(Roles = "admin")]
        public IActionResult GetCampaigns()
        {
            var result = _context.GetCampaigns();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddCampaign(AddCampaignRequest request)
        {
            try
            {
                var result = _context.AddCampaign(request);
                return Ok(result);
            }
            catch (ClientDoesntExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (BuildingsStreetException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}