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
	public partial class ClientDetailPage : ContentPage
	{
		public ClientDetailPage(Client client)
		{
			InitializeComponent();
			BindingContext = new ClientDetailViewModel(client);
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Subscribe<string, string>("ClientDetails", "UpdateListView", async (sen, result) =>
			{
                var option = new ToastView("Client deleted successfully!");
                await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
            });

            MessagingCenter.Unsubscribe<string>("ClientDetails", "UpdateListView");


        }
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            var option = new ToastView("Client updated successfully!");
            await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());

        }
    }
}