using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XoaiSay.Data;
using Volo.Abp.DependencyInjection;

namespace XoaiSay.EntityFrameworkCore;

public class EntityFrameworkCoreXoaiSayDbSchemaMigrator
    : IXoaiSayDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreXoaiSayDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the XoaiSayDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<XoaiSayDbContext>()
            .Database
            .MigrateAsync();
    }
}
