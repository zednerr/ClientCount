using ClientCount.Models;
using ClientCount.MvvM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientCount.MvvM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeDetailPage : ContentPage
    {
        public EmployeeDetailPage(Employee employee)
        {
            InitializeComponent();
            BindingContext = new EmployeeDetailModel(employee);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
           
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<string, string>("EmployeeDetails", "UpdateListView", async (sen, result) =>
            {
                var option = new ToastView("Employee deleted successfully!");
                await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
            });

            MessagingCenter.Unsubscribe<string>("EmployeeDetails", "UpdateListView");
        }
    }
}