using LCW.Core.Repositories;
using LCW.Core.Results;
using LCW.Domain.Context;
using LCW.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCW.Services.Repositories
{
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        public UserRepository(storedbContext context) : base(context)
        {

        }

        public User GetByMail(string mail)
        {
            return _context.Users.FirstOrDefault(u => u.Email == mail);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new storedbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }
}
