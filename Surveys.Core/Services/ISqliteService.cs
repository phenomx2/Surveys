using SQLite;

namespace Surveys.Core.Services
{
    public interface ISqliteService
    {
        SQLiteConnection GetConnection();
    }
}