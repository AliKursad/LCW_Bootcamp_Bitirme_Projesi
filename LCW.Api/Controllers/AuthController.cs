using LCW.Core.Repositories;
using LCW.Domain.Dtos;
using LCW.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IGenericRepository<Category> _genericRepository;
        private readonly IGenericRepository<Brand> _genericColorRepository;
        private readonly IGenericRepository<Color> _genericBrandRepository;

        public AuthController(IAuthService authService, IGenericRepository<Category> genericRepository)
        {
            _authService = authService;
            _genericRepository = genericRepository;
        }

        [HttpPost("/register")]
        public IActionResult RegisterUser(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("/login")]
        public IActionResult LoginUser(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("Categories")]
        public IActionResult GetCategory()
        {

            return Ok(_genericRepository.GetAll());
        }

        [HttpGet("Colors")]
        public IActionResult GetColors()
        {

            return Ok(_genericColorRepository.GetAll());
        }

        [HttpGet("Brands")]
        public IActionResult GetBrands()
        {

            return Ok(_genericBrandRepository.GetAll());
        }

    }
}
