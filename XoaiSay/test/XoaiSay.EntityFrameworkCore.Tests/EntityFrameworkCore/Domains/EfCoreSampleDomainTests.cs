using XoaiSay.Samples;
using Xunit;

namespace XoaiSay.EntityFrameworkCore.Domains;

[Collection(XoaiSayTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<XoaiSayEntityFrameworkCoreTestModule>
{

}
