using ClientCount.Models;
using ClientCount.MvvM.ViewModels;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientCount.MvvM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpMenu : Popup
    {
        public PopUpMenu()
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            Size size = new Size();
            size.Width = mainDisplayInfo.Width / mainDisplayInfo.Density;
            size.Height = 270;
            Size = size;
            lay.WidthRequest = size.Width;
            lay.HeightRequest = size.Height;
            lays.WidthRequest = size.Width;
            lays.HeightRequest = size.Height;

            BindingContext = new ClientsListViewModel();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            Dismiss(null);
        }

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}