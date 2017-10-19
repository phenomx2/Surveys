using SQLite;

namespace Surveys.Core.Services
{
    public interface ISqliteService //TODO Mover a su namespace correspondiente
    {
        SQLiteConnection GetConnection();
    }
}