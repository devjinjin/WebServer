using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Data.Users
{
    public interface ITokenBuilder
    {
        string BuildToken(string username);
    }
}
