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
    public partial class ListEmployeePage : ContentPage
    {
        public ListEmployeePage()
        {
            InitializeComponent();
            BindingContext = new ListEmployeeViewModel();
        }
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<string, string>("ListEmployeePage", "UpdateListView", (sender, result) =>
            {
                BindingContext = new ListEmployeeViewModel();
            });
            MessagingCenter.Subscribe<SearchResult>(this, "PopUpData", (value) =>
            {
                SearchResult.ItemsSource = value.ReturnData_Employee;
            });
        }
        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<string>("ListEmployeePage", "UpdateListView");
            MessagingCenter.Unsubscribe<SearchResult>(this, "PopUpData");
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

        private async void searchanim_Clicked(object sender, EventArgs e)
        {
            await searchanim.ScaleTo(0.8, 150, Easing.Linear);
            await Task.Delay(250);
            await searchanim.ScaleTo(1, 150, Easing.Linear);
            var result = await Navigation.ShowPopupAsync(new SearchClientBar("listemployeepage"));
        }
    }
}