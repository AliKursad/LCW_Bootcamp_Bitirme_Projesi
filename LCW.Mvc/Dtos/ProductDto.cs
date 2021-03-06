using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCW.Mvc.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IFormFile> files { get; set; }
        public string FileName { get; set; }
        public decimal StartPrice { get; set; }
        public string Picture { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string Description { get; set; }
    }
}
