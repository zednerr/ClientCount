using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClientCount.DB
{
    public class DbConstants
    {
        private static string DBFileName = "ClientsCount.db3";
        public static string DatabasePath
        {
            get
            {
                string baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(baseFolder, DBFileName);
            }

        }
    }
}
