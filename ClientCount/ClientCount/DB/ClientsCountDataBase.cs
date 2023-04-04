using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using Android.Media.TV;
using ClientCount.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using SQLite;
namespace ClientCount.DB
{
    public class ClientsCountDataBase
    {
        public SQLiteConnection Connection { get; set; }
        public ClientsCountDataBase()
        {
            Initialize();
        }
        void Initialize()
        {
            Connection = new SQLiteConnection(DbConstants.DatabasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
            Connection.CreateTable<Client>(CreateFlags.None);
            Connection.CreateTable<Employee>(CreateFlags.None);
            Connection.CreateTable<LivingPlace>(CreateFlags.None);
            Connection.CreateTable<Actions>(CreateFlags.None);
            var info = Connection.GetTableInfo("Brand");
            if (!info.Any())
            {
                List<string> BrandList = new List<string>() { "Nova Florida", "Fondital" };
                Connection.CreateTable<Brand>(CreateFlags.None);
                for(int i = 0; i < BrandList.Count(); i++)
                {
                    Connection.Query<Brand>($"Insert into brand (BrandName) values('{BrandList[i]}')");
                }
            }
            info = Connection.GetTableInfo("Model");
            if (!info.Any())
            {
                List<string> ModelList = new List<string>() {
                "Altair","Antea","Antea mono","Antea monotermica","Bali","Delfis","Delfis condesing"
                ,"Phoenix","Formentera","Itaca","Leo","Libra","Minorka","Nibir","Nibir condensing",
                "Orion","Orion condensing","Panarea","Pictor condensing","Vela Compact","Victoria","Virgo","Virgo condensing" };
                Connection.CreateTable<Model>(CreateFlags.None);
                for (int i = 0; i < ModelList.Count(); i++)
                {
                    Connection.Query<Brand>($"Insert into model (ModelName) values('{ModelList[i]}')");
                }
            }
        }
    }
}
