using Volo.Abp.Modularity;

namespace XoaiSay;

/* Inherit from this class for your domain layer tests. */
public abstract class XoaiSayDomainTestBase<TStartupModule> : XoaiSayTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
