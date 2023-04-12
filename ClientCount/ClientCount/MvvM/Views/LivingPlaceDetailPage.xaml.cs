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
        private void d_sold_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            d_com.MinimumDate = d_sold.Date;
        }
    }
}