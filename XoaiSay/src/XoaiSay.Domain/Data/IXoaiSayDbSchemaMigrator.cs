using System.Threading.Tasks;

namespace XoaiSay.Data;

public interface IXoaiSayDbSchemaMigrator
{
    Task MigrateAsync();
}
