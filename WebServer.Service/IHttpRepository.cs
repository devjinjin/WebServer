using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Service
{
    public interface IHttpRepository<T>
    {
        Task Create(T item);
    }
}
