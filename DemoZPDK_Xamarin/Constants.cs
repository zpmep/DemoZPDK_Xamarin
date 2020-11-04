using System;
using System.IO;

namespace DemoZPDK_Xamarin
{
    public class Constants
    {
        public const string DatabaseFilename = "DemoZPDK.db3";
        public const string APP_ID = "120987";
        public const string KEY1 = "YdgrAPA0sgfdLQb8xMKCiIFvhxfE2VQg";
        public const string KEY2 = "D0nMOedS93svuxlk0787Elqeaoog9xjh";
        public const string EndpointUri = "https://sb-openapi.zalopay.vn";


        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}