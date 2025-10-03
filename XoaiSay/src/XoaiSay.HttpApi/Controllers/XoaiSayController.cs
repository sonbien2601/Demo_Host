using XoaiSay.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace XoaiSay.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class XoaiSayController : AbpControllerBase
{
    protected XoaiSayController()
    {
        LocalizationResource = typeof(XoaiSayResource);
    }
}
