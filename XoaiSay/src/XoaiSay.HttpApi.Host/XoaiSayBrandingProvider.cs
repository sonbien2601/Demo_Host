using Microsoft.Extensions.Localization;
using XoaiSay.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace XoaiSay;

[Dependency(ReplaceServices = true)]
public class XoaiSayBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<XoaiSayResource> _localizer;

    public XoaiSayBrandingProvider(IStringLocalizer<XoaiSayResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
