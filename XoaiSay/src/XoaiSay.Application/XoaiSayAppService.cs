using XoaiSay.Localization;
using Volo.Abp.Application.Services;

namespace XoaiSay;

/* Inherit your application services from this class.
 */
public abstract class XoaiSayAppService : ApplicationService
{
    protected XoaiSayAppService()
    {
        LocalizationResource = typeof(XoaiSayResource);
    }
}
