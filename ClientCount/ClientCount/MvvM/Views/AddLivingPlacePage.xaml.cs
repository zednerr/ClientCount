using ClientCount.Models;
using ClientCount.MvvM.ViewModels;
using ClientCount.MvvM.ViewModels.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using ClientCount.Services;

namespace ClientCount.MvvM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLivingPlacePage : ContentPage
    {
       
        public AddLivingPlacePage(Client client)
        {
            InitializeComponent();
            d_l.Date = DateTime.Now;
            BindingContext = new AddLivingPlaceViewModel(client);
            BrandService brandService = new BrandService();
            var conn = App.DataBase.Connection;
            var result_1 = brandService.GetAllBrands();
            List<string> BrandList = new List<string>();
            foreach (var item in result_1)
            {
                BrandList.Add(item.BrandName);
            }
            ModelService modelService = new ModelService();
            List<string> ModelList = new List<string>();
            var result_2 = modelService.GetAllModels();
            foreach (var item in result_2)
            {
                ModelList.Add(item.ModelName);
            }
            BrandPick.ItemsSource = BrandList;
            ModelPick.ItemsSource = ModelList;

        }

        private void DatePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void d_l_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            d_u.MinimumDate =d_l.Date;
        }
    }
}