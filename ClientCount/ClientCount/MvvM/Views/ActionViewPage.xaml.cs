using ClientCount.Models;
using ClientCount.MvvM.ViewModels;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Android.Widget;
using System.Net.Sockets;

namespace ClientCount.MvvM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionViewPage : ContentPage
    {
        public ActionViewPage()
        {
            InitializeComponent();
            BindingContext = new ActionsListViewModel();

        }
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<string, string>("ActionViewPage", "UpdateListView", (sender, result) => {

                BindingContext = new ActionsListViewModel();
            });
        }
        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<string>("ActionViewPage", "UpdateListView");
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var action = menuItem.CommandParameter as DopActions;
            var conn = App.DataBase.Connection;
            try
            {
                var option = new ToastView("Task Completed!");
                conn.Query<Actions>($"Update actions set result ={"'Complete'"} where id={action.Id}");
                MessagingCenter.Send("UpdateAction", "UpdateListView", "Success");

                await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
            }
            catch(SqlNullValueException)
            {
                await DisplayAlert("Error", "An error occurred during the execution of the task", "Ok");
            }
            catch (SQLite.NotNullConstraintViolationException)
            {
               await DisplayAlert("Error", "An error occurred during the execution of the task", "Ok");
            }

        }

        private async void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var conn = App.DataBase.Connection;
            var menuItem = sender as MenuItem;
            var action = menuItem.CommandParameter as DopActions;
            try
            {
                var option = new ToastView("Task Not Completed!");
                conn.Query<Actions>($"Update actions set result ={"'Not Complete'"} where id={action.Id}");
                MessagingCenter.Send("UpdateAction", "UpdateListView", "Success");
                await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
            }
            catch (SqlNullValueException)
            {
                await DisplayAlert("Error", "An error occurred during the execution of the task", "Ok");
            }
            catch (SQLite.NotNullConstraintViolationException)
            {
                await DisplayAlert("Error", "An error occurred during the execution of the task", "Ok");
            }
        }

        private async void MenuItem_Clicked_2(object sender, EventArgs e)
        {
            var conn = App.DataBase.Connection;
            var menuItem = sender as MenuItem;
            var action = menuItem.CommandParameter as DopActions;
            try
            {
                var option = new ToastView("Task deleted!");
                conn.Query<Actions>($"Delete from actions where id={action.Id}");
                MessagingCenter.Send("UpdateAction", "UpdateListView", "Success");
                await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
            }
            catch (SqlNullValueException)
            {
                await DisplayAlert("Error", "An error occurred during the execution of the task", "Ok");
            }
            catch (SQLite.NotNullConstraintViolationException)
            {
                await DisplayAlert("Error", "An error occurred during the execution of the task", "Ok");
            }
        }

        private async void notetextanim_Clicked(object sender, EventArgs e)
        {
            await notetextanim.ScaleTo(0.8, 150, Easing.Linear);
            await arrowanim.RelRotateTo(-180, 150);
            await Task.Delay(250);
            await arrowanim.RelRotateTo(180, 150);
            await notetextanim.ScaleTo(1, 150, Easing.Linear);
            var result = await Navigation.ShowPopupAsync(new PopUpMenu());
        }
    }
}