using LCW.Core.Results;
using LCW.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCW.Core.Repositories
{
    public interface IUserRepository:IGenericRepository<User>
    {
        User GetByMail(string email);
        List<OperationClaim> GetClaims(User user);
    }
}
