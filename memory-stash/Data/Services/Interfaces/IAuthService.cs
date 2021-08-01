using memory_stash.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memory_stash.Data.Services.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(UserAuth userAuth);
    }
}
