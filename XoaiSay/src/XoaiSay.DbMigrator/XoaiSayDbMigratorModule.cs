using XoaiSay.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace XoaiSay.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(XoaiSayEntityFrameworkCoreModule),
    typeof(XoaiSayApplicationContractsModule)
)]
public class XoaiSayDbMigratorModule : AbpModule
{
}
