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
    public partial class LivingPlaceListPage : ContentPage
    {
        private readonly Client current_client;
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<string, string>("LivingPlaceListPage", "UpdateLivingPlaceView", (sender, result) => {
                BindingContext = new LivingPlaceListViewModel(current_client);
            });
            MessagingCenter.Subscribe<SearchResult>(this, "PopUpData", (value) =>
            {
                SearchResult.ItemsSource = value.ReturnData_LivingPlace;
            });
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<string>("LivingPlaceListPage", "UpdateLivingPlaceView");
            MessagingCenter.Unsubscribe<SearchResult>(this, "PopUpData");
        }

        public LivingPlaceListPage(Client client)
        {
            InitializeComponent();
            current_client = client;
            BindingContext = new LivingPlaceListViewModel(client);

        }

        private async void searchanim_Clicked(object sender, EventArgs e)
        {
            await searchanim.ScaleTo(0.8, 150, Easing.Linear);
            await Task.Delay(250);
            await searchanim.ScaleTo(1, 150, Easing.Linear);
            var result = await Navigation.ShowPopupAsync(new SearchClientBar("listlivingplaceview",current_client));
        }

        private void notetextanim_Clicked(object sender, EventArgs e)
        {

        }
    }
}