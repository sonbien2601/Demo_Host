using Xunit;

namespace XoaiSay.EntityFrameworkCore;

[CollectionDefinition(XoaiSayTestConsts.CollectionDefinitionName)]
public class XoaiSayEntityFrameworkCoreCollection : ICollectionFixture<XoaiSayEntityFrameworkCoreFixture>
{

}
