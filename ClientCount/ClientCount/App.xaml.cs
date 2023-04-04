using ClientCount.DB;
using ClientCount.Models;
using ClientCount.MvvM.Views;
using ClientCount.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientCount
{
    public partial class App : Application
    {
        public static INavigation Navigation { get; set; }
        private static ClientsCountDataBase _database;

        public static ClientsCountDataBase DataBase
        {
            get
            {
                if (_database == null)
                    _database = new ClientsCountDataBase();
                return _database;
            }
        }
        public App()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new MainPage());
         
        }
        public static async Task<bool> ConfirmAlert(string title, string message)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, "Да", "Нет");
        }
        public static async Task DisplayAlert(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Ok");
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
