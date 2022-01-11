using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Butikk.Models
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
