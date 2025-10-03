using Volo.Abp.Modularity;

namespace XoaiSay;

[DependsOn(
    typeof(XoaiSayApplicationModule),
    typeof(XoaiSayDomainTestModule)
)]
public class XoaiSayApplicationTestModule : AbpModule
{

}
