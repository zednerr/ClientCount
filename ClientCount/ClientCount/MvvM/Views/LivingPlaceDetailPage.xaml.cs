using ClientCount.Models;
using ClientCount.MvvM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientCount.MvvM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LivingPlaceDetailPage : ContentPage
    {
        private readonly LivingPlace livingPlace;
        public LivingPlaceDetailPage(LivingPlace lPlace)
        {
            InitializeComponent();
            livingPlace = lPlace;
            BindingContext = new LivingPlaceDetailModel(lPlace);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<string, string>("LivingPlaceDetails", "UpdateLivingPlaceView", async (sen, result) =>
            {
                var option = new ToastView("Address deleted successfully!");
                await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
            });
            MessagingCenter.Unsubscribe<string>("LivingPlaceDetails", "UpdateLivingPlaceView");
        }
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var option = new ToastView("Address updated successfully!");
            await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
        }

        private void d_sold_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            d_com.MinimumDate = d_sold.Date;
        }
    }
}