using ClientCount.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientCount.Services
{
    public class BrandService
    {
        public int CreateBrand(Brand brand)
        {
            var conn = App.DataBase.Connection;

            return conn.Insert(brand);
        }
        public int UpdateBrand(Brand brand)
        {
            var conn = App.DataBase.Connection;
            return conn.Update(brand);
        }
        public int DeleteBrand(string brand)
        {
            var conn = App.DataBase.Connection;

            return conn.Delete<Brand>(brand);
            
        }
        public List<Brand> GetAllBrands()
        {
            var conn = App.DataBase.Connection;

            return conn.Query<Brand>("SELECT * FROM brand");
        }
    }
}
