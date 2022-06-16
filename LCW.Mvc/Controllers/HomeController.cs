using LCW.Core.Repositories;
using LCW.Domain.Models;
using LCW.Mvc.Dtos;
using LCW.Mvc.Models;
using LCW.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LCW.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Categories = await _httpClient.GetFromJsonAsync<List<Category>>(_configuration.GetSection("ApiUrl").Value + "Api/Category/Categories");
            pvm.Products = await _httpClient.GetFromJsonAsync<List<Product>>(_configuration.GetSection("ApiUrl").Value + "Api/Product/Products");
            return View(pvm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterDto userRegisterDto)
        {
            using (var response = await _httpClient.PostAsJsonAsync(_configuration.GetSection("ApiUrl").Value + "register", userRegisterDto))
            {
                if (response.IsSuccessStatusCode)
                {
                    UserForLoginDto userForLoginDto = new UserForLoginDto
                    {
                        Email = userRegisterDto.Email,
                        Password = userRegisterDto.Password
                    };

                    return RedirectToAction("LoginPost", "Home", userForLoginDto);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPost(UserForLoginDto userLoginDto)
        {
            using (var response = await _httpClient.PostAsJsonAsync(_configuration.GetSection("ApiUrl").Value + "login", userLoginDto))
            {
                if (response.IsSuccessStatusCode)
                {

                    var contents = response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject(contents.Result);
                    return View();

                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProductPage()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Categories = await _httpClient.GetFromJsonAsync<List<Category>>(_configuration.GetSection("ApiUrl").Value + "Api/Category/Categories");
            pvm.Brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_configuration.GetSection("ApiUrl").Value + "Api/Product/Brands");
            pvm.Colors = await _httpClient.GetFromJsonAsync<List<Color>>(_configuration.GetSection("ApiUrl").Value + "Api/Product/Colors");

            return View(pvm);
        }

        [HttpPost]
        public async Task <IActionResult> SaveProduct(ProductDto productDto)
        {
            foreach (var file in productDto.files)
            {
                if (file.Length > 0)
                {
                    productDto.FileName = CreateTempfilePath(file.FileName);


                    using (var stream = new FileStream(productDto.FileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            Product product = new Product
            {
                BrandId = productDto.BrandId,
                CategoryId = productDto.CategoryId,
                ColorId = productDto.ColorId,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                Description = productDto.Description,
                IsOfferable = true,
                IsSold = true,
                UserId = 1,
                Picture = productDto.FileName,
                StartPrice = productDto.StartPrice,
                Name = productDto.Name
            };

            using (var response = await _httpClient.PostAsJsonAsync(_configuration.GetSection("ApiUrl").Value + "Api/Product", product))
            {
                if (response.IsSuccessStatusCode)
                {

                    var contents = response.Content.ReadAsStringAsync();
                   
                }
            }

            return View("Index");
        }
        public string CreateTempfilePath(string fullName)
        {
            var newGuidName = $"{Guid.NewGuid()}";
            var pathArray = fullName.Split('.');
            newGuidName += "." + pathArray[1];


            var directoryPath = Path.Combine("temp", "uploads");
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            return Path.Combine(directoryPath, newGuidName);

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
