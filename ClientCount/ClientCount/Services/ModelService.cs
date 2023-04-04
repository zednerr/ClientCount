using ClientCount.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCount.Services
{
    public class ModelService
    {
        public int CreateModel(Model model)
        {
            var conn = App.DataBase.Connection;

            return conn.Insert(model);
        }
        public int UpdateModel(Model model)
        {
            var conn = App.DataBase.Connection;
            return conn.Update(model);
        }
        public int DeleteModel(Model model)
        {
            var conn = App.DataBase.Connection;

            return conn.Delete(model);
        }
        public List<Model> GetAllModels()
        {
            var conn = App.DataBase.Connection;

            return conn.Query<Model>("SELECT * FROM model");
        }
    }
}
