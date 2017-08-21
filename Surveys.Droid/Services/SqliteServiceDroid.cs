using System;
using System.IO;
using SQLite;
using Surveys.Core.Services;
using Surveys.Droid.Services;

[assembly:Xamarin.Forms.Dependency(typeof(SqliteServiceDroid))]
namespace Surveys.Droid.Services
{
    public class SqliteServiceDroid : ISqliteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDbFile =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    "surveys.db");
            return new SQLiteConnection(localDbFile);
        }
    }
}