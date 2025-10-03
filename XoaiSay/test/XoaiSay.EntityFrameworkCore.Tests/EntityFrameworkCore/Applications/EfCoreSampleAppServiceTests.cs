using XoaiSay.Samples;
using Xunit;

namespace XoaiSay.EntityFrameworkCore.Applications;

[Collection(XoaiSayTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<XoaiSayEntityFrameworkCoreTestModule>
{

}
