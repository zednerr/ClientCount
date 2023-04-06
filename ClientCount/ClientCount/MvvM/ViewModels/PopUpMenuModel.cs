using ClientCount.MvvM.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace ClientCount.MvvM.ViewModels
{
    class PopUpMenuModel
    {
       
        public ICommand ActionsViewCommand
        {
            get
            {
                return new Command(async () =>
                {

                    await App.Navigation.PushAsync(new NavigationPage(new ActionViewPage()));
                });
            }
        }
        public ICommand EmployeeListCommand
        {
            get
            {
                return new Command(async () =>
                {

                    await App.Navigation.PushAsync(new NavigationPage(new ListEmployeePage()));
                });
            }
        }
        public ICommand ReportCommand
        {
            get
            {
                return new Command(() =>
                {
                    App.Navigation.ShowPopup(new ReportDetailPage());
                });
            }
        }

        public ICommand ListClientCommand
        {
            get
            {
                return new Command(() =>
                {
                    App.Navigation.PopToRootAsync();
                });
            }
        }
    }
}
