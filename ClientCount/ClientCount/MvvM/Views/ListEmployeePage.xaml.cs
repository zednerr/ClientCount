using ClientCount.Models;
using ClientCount.MvvM.ViewModels;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientCount.MvvM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListEmployeePage : TabbedPage
    {
       static BrandService BrandService = new BrandService();
      static  ModelService modelService = new ModelService();
        public ListEmployeePage()
        {
            InitializeComponent();
            BindingContext = new ListEmployeeViewModel();
        }
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<string, string>("ListEmployeePage", "ListView", (sender, result) =>
            {
                BindingContext = new ListEmployeeViewModel();
            });
            MessagingCenter.Subscribe<SearchResult>(this, "PopUpData", (value) =>
            {
                SearchResult.ItemsSource = value.ReturnData_Employee;
            });
            MessagingCenter.Subscribe<string, string>("MainPage", "Add", (sender, result) => {
                BindingContext = new ListEmployeeViewModel();
            });
        }
        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<string>("ListEmployeePage", "ListView");
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                string result = await DisplayPromptAsync("Add model", "Enter model name: ", "Ok", "Cancel");
                int res = modelService.CreateModel(new Model
                {
                    ModelName = result
                });
                if (res > 0)
                {
                    MessagingCenter.Send("AddEquip", "Add", "Success");
                }
            }
            catch(Exception) {
                await DisplayAlert("Error", "Fill in the input field!", "Ok");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                string result = await DisplayPromptAsync("Add Brand", "Enter brand name: ","Ok","Cancel");
                int res = BrandService.CreateBrand(new Brand
                {
                    BrandName = result
                });
                if (res > 0)
                {
                    MessagingCenter.Send("AddEquip", "Add", "Success");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Fill in the input field!", "Ok");
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var brand =menuItem.CommandParameter as Brand;
            var conn = App.DataBase.Connection;
            try
            {
                var option = new ToastView("Brand Deleted!");
                conn.Query<Brand>($"Delete from brand where id = {brand.Id}");
                MessagingCenter.Send("DeleteEquip", "Add", "Success");

                await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
            }
            catch (SqlNullValueException)
            {
                await DisplayAlert("Error", "An error occurred during the execution of the brand", "Ok");
            }
            catch (SQLite.NotNullConstraintViolationException)
            {
                await DisplayAlert("Error", "An error occurred during the execution of the brand", "Ok");
            }
        }

        private async void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var model = menuItem.CommandParameter as Model;
            var conn = App.DataBase.Connection;
            try
            {
                var option = new ToastView("Model Deleted!");
                conn.Query<Model>($"Delete from model where id = {model.Id}");
                MessagingCenter.Send("DeleteEquip", "Add", "Success");

                await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
            }
            catch (SqlNullValueException)
            {
                await DisplayAlert("Error", "An error occurred during the execution of the brand", "Ok");
            }
            catch (SQLite.NotNullConstraintViolationException)
            {
                await DisplayAlert("Error", "An error occurred during the execution of the brand", "Ok");
            }
        }
        //int res = BrandService.DeleteBrand(objecs.Text);
        //    if (res > 0)
        //    {
        //        MessagingCenter.Send("AddEquip", "Add", "Success");
        //    } DELETE MODEL AND BRAND
    }
}