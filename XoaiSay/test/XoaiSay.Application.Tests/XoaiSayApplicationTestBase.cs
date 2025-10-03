using Volo.Abp.Modularity;

namespace XoaiSay;

public abstract class XoaiSayApplicationTestBase<TStartupModule> : XoaiSayTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
