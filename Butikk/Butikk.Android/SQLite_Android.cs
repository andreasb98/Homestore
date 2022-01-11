using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Butikk.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly:Dependency(typeof(Butikk.Droid.SQLite_Android))]
namespace Butikk.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFileName = "MyDatabase.db1";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);           
            var path = Path.Combine(documentsPath, sqliteFileName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}