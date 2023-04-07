using ClientCount.Models;
using ClientCount.DB;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ClientCount.Services
{
    public class ClientService
    {
        public int CountClients()
        {
            var conn = App.DataBase.Connection;

         int a =  conn.Query<Client>("Select id from client").Count();
            return a;
        }

        public int CreateClient(Client client)
        {
            var conn = App.DataBase.Connection;

            return conn.Insert(client);
        }
        public int CreateClients(List<Client> clients)
        {
            var conn = App.DataBase.Connection;

            return conn.InsertAll(clients);
        }

        public int UpdateClient(Client client)
        {
            var conn = App.DataBase.Connection;
            return conn.Update(client);
        }

        public int UpdateAllClients(List<Client> clients)
        {
            var conn = App.DataBase.Connection;
            return conn.UpdateAll(clients);
        }

        public int DeleteClient(Client client)
        {
            var conn = App.DataBase.Connection;
            
            return conn.Delete<Client>(client.Id);
        }

        public Client Get(int id)
        {
            var conn = App.DataBase.Connection;
            //var result = conn.Get(id, new TableMapping(typeof(Client)));
            //return (Client) result;


            //return conn.Get<Client>(id);

            //return conn.Table<Client>().Where(c => c.Id == id).FirstOrDefault();
            return conn.Query<Client>($"SELECT * FROM Client WHERE id = {id}").FirstOrDefault();

        }
        //public List<Client> GetAnyArgs(string args1, string args2)
        //{
        //    var conn = App.DataBase.Connection;
        //    return conn.Query<Client>($"SELECT * FROM Client WHERE ? like ?", new string[2] {args1, args2});
        //}
        public List<Client> ReadAllClients()
        {
            var conn = App.DataBase.Connection;
            //return conn.Table<Client>().ToList();

            return conn.Query<Client>("SELECT * FROM Client");
        }
        public List<Client> ReadAllClientsforview()
        {
            var conn = App.DataBase.Connection;
            //return conn.Table<Client>().ToList();

            return conn.Query<Client>("SELECT  id,FirstName||' '||LastName AS firstname,phonenumber FROM Client");
        }
        public List<Client> ReadAllClientsOnPage(int cur_page)
        {
            var conn = App.DataBase.Connection;
            //return conn.Table<Client>().ToList();
            if (cur_page == 1)
            {
                return conn.Query<Client>("SELECT  id,FirstName||' '||LastName AS firstname,phonenumber FROM Client LIMIT 5");
            }
            else if (cur_page >1)
            {
                int l_r = ((cur_page - 1)*5);
                int r_r =5;
                return conn.Query<Client>($"SELECT  id,FirstName||' '||LastName AS firstname,phonenumber FROM Client LIMIT {l_r},{r_r}");
            }
            else
            {
                return null;
            }
        }
    }
}
