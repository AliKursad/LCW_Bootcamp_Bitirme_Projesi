using LCW.Core.Repositories;
using LCW.Domain.Context;
using LCW.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCW.Services.Repositories
{
    public class ColorRepository: GenericRepository<Color>, IColorRepository
    {
        public ColorRepository(storedbContext context) : base(context)
        {

        }
    }
}
