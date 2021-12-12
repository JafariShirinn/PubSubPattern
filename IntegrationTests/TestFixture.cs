using App;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class TestFixture : IDisposable
    {
        private readonly WebApplicationFactory<Startup> _appFactory;
        private readonly IServiceScope _serviceScope;
        private readonly HttpClient _httpClient;

        public TestFixture()
        {
            _appFactory = new WebApplicationFactory<Startup>();
            _serviceScope = _appFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _httpClient = _appFactory.CreateClient();
        }

        public void Dispose()
        {
            _appFactory.Dispose();
            _serviceScope.Dispose();
            _httpClient.Dispose();
        }
    }
}
