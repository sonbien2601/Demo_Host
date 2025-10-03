using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace XoaiSay.Data;

/* This is used if database provider does't define
 * IXoaiSayDbSchemaMigrator implementation.
 */
public class NullXoaiSayDbSchemaMigrator : IXoaiSayDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
