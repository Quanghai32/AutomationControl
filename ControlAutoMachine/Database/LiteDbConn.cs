using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismTest
{
    public class LiteDbConn
    {
        static string DbPath = @"./LOG/History.db";
        public static LiteDatabase db = new LiteDatabase(DbPath);

        public LiteDbConn()
        {
           
        }

        public static void Open()
        {
            if (db != null)
            {
                db = new LiteDatabase(DbPath);
            }
        }
        public static void Close()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }
    }
}
