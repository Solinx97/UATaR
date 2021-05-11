﻿using System.Net.Http;
using System.Threading.Tasks;

namespace UATaR.Interfaces
{
    public interface IApiClientHelper
    {
        Task<HttpResponseMessage> PostAsync(string requestUrl, object model);

        Task<HttpResponseMessage> GetAsync(string requestUrl);

        Task<HttpResponseMessage> PutAsync(string requestUrl, object model);

        Task<HttpResponseMessage> DeleteAsync(string requestUrl);

        Task<T> ReadAsJsonAsync<T>(HttpResponseMessage httpMessage);
    }
}