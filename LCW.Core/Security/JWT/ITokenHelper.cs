using LCW.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCW.Core.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
