using Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITokenAuthenticationService
    {
        public Task<string> UseRefreshTokenAsync(string refreshToken);

    }
}
