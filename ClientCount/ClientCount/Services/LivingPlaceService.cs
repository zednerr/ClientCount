using ClientCount.Models;
using ClientCount.DB;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using ClientCount.MvvM.ViewModels.client;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ClientCount.Services
{
    public class LivingPlaceService
    {

        public int CreateLivingPlace(LivingPlace livingPlace)
        {
            var conn = App.DataBase.Connection;
            return conn.Insert(livingPlace);
        }
        public int CreateLivingPlace(List<LivingPlace> livingPlaces)
        {
            var conn = App.DataBase.Connection;

            return conn.InsertAll(livingPlaces);
        }

        public int UpdateLivingPlace(LivingPlace livingPlace)
        {
            var conn = App.DataBase.Connection;
            return conn.Update(livingPlace);
        }


        public int DeleteLivingPlace(LivingPlace livingPlace)
        {
            var conn = App.DataBase.Connection;
            //return conn.Delete(client.Id,new SQLite.TableMapping(typeof(Client)));
            conn.Query<Actions>($"DELETE FROM Actions WHERE LivingPlace_id = {livingPlace.Id}");
            return conn.Delete<LivingPlace>(livingPlace.Id);
        }
        public List<LivingPlace> ReadAll()
        {
            var conn = App.DataBase.Connection;
            //return conn.Table<Client>().ToList();

            return conn.Query<LivingPlace>("SELECT * FROM LivingPlace");
        }
        public List<LivingPlace> ReadById(int id)
        {
            var conn = App.DataBase.Connection;
            //return conn.Table<Client>().ToList();
            return conn.Query<LivingPlace>($"SELECT * FROM LivingPlace WHERE Client_id = {id}");
        }
        // return conn.Query<Client>($"SELECT * FROM Client WHERE id = {id}").FirstOrDefault();
        public void DeleteById(int Id)
        {
            var conn = App.DataBase.Connection;
             conn.Query<LivingPlace>($"DELETE  FROM LivingPlace WHERE Client_id = {Id}");
        }


    }
}
