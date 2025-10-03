using Volo.Abp.Modularity;

namespace XoaiSay;

[DependsOn(
    typeof(XoaiSayDomainModule),
    typeof(XoaiSayTestBaseModule)
)]
public class XoaiSayDomainTestModule : AbpModule
{

}
