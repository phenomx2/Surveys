using System;
using System.IO;
using SQLite;
using Surveys.Core.Services;

namespace Surveys.iOS.Services
{
    // ReSharper disable once InconsistentNaming
    public class SqliteServiceiOS : ISqliteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDbFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "surveys.db");
            return new SQLiteConnection(localDbFile);
        }
    }
}