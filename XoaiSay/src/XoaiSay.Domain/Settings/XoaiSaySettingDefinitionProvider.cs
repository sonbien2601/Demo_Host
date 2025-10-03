using Volo.Abp.Settings;

namespace XoaiSay.Settings;

public class XoaiSaySettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(XoaiSaySettings.MySetting1));
    }
}
