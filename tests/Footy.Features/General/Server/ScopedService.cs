using System;
using Microsoft.Extensions.DependencyInjection;

namespace Footy.Features.General.Server
{
    public class ScopedService<T> : IDisposable
    {
        private readonly IServiceScope _scope;

        public T Service { get; }

        public ScopedService(IServiceScope scope)
        {
            _scope = scope;
            Service = _scope.ServiceProvider.GetRequiredService<T>();
        }

        public void Dispose()
        {
            _scope?.Dispose();
            if (Service is IDisposable disposable)
                disposable.Dispose();
        }
    }
}