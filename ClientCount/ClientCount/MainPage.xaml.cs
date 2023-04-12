using ClientCount.Models;
using ClientCount.MvvM.ViewModels;
using ClientCount.MvvM.Views;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Path = System.IO.Path;
using Xamarin.CommunityToolkit.Extensions;
using Android.Views.Accessibility;

namespace ClientCount
{
    public partial class MainPage : ContentPage
    {
        public int current_page = 1;
        static int item_on_page = 5;
        static ClientService clientService = new ClientService();
        static double a = clientService.CountClients();
        static double pages = Math.Ceiling(a/item_on_page);
        private double update_pages_count()
        {
            a = clientService.CountClients();
            pages = Math.Ceiling(a / item_on_page);
            return pages;
        }
        public MainPage()
        {
            InitializeComponent();
            update_pages_count();
            BindingContext = new ClientsListViewModel(current_page);
            App.Navigation = Navigation;
        }
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<string, string>("MainPage", "UpdateListView", (sender, result) => {
                update_pages_count();
                BindingContext = new ClientsListViewModel(current_page);
            });
            MessagingCenter.Subscribe<List<Client>>(this, "PopUpData", (value) =>
            {
                BindingContext = new ClientsListViewModel(value);
            });
        
        }   
        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<string>("MainPage", "UpdateListView");
            MessagingCenter.Unsubscribe<SearchResult>(this,"PopUpData");
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
         
            var conn = App.DataBase.Connection;

            var list = from client in conn.Table<Client>()
                       from livingPlace in conn.Table<LivingPlace>()
                       from employee in conn.Table<Employee>()
                       where client.Id == livingPlace.Client_id where client.Employee_id == employee.Id
                       select new {brand_name=livingPlace.BrandName,model_name=livingPlace.ModelName,serial_number=livingPlace.SerialNumber,guarantee_number=livingPlace.GuaranteeNumber, client_name = client.FirstName,
                           client_lastname=client.LastName,client_patronymic=client.Patronymic,data_sell=livingPlace.DateSold,data_start_exp=livingPlace.DateStartExp,living_city=livingPlace.City,living_street = livingPlace.Street,
                           living_house=livingPlace.HouseNumber,living_flat_number=livingPlace.FlatNumber,client_phonenumber=client.PhoneNumber,client_hphonenumber=client.HphoneNumber,client_employee_lastname = employee.LastName};


            ExcelService excelService = new ExcelService();

            var fileName = $"Client_list-{DateTime.Now.ToString()}-{Guid.NewGuid()}.xlsx";
            string filepath = excelService.GenerateExcel(fileName);

            var data = new ExcelStructure
            {
                Headers = new List<string>() {"Изготовитель", "Модель", "Серийный №", "№ гар.талона", "Клиент", "Дата пуска","Сотрудник", "Дата продажи", "Населенный пункт", "Улица", "Дом", "Кв", "Моб.телефон", "Дом.Телефон" }
            };

            foreach (var item in list)
            {
                data.Values.Add(new List<string>() {item.brand_name,item.model_name,item.serial_number,item.guarantee_number,item.client_lastname+" "+item.client_name+" "+item.client_patronymic,item.data_start_exp,item.client_employee_lastname, item.data_sell,item.living_city,item.living_street,item.living_house,item.living_flat_number,item.client_phonenumber,item.client_hphonenumber});
            }
            

            excelService.InsertDataIntoSheet(filepath, "Contacts", data);

            await Launcher.OpenAsync(new OpenFileRequest()
            {
                File = new ReadOnlyFile(filepath)
            });

            var filePaths = Path.Combine(FileSystem.CacheDirectory,filepath);
         
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(filePaths)
            });


        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await notetextanim.ScaleTo(0.8,150, Easing.Linear);
            await arrowanim.RelRotateTo(-180, 150);
            await Task.Delay(250);
            await arrowanim.RelRotateTo(180, 150);
            await notetextanim.ScaleTo(1, 150, Easing.Linear);
            var result = await Navigation.ShowPopupAsync(new PopUpMenu()); 
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            var conn = App.DataBase.Connection;

            var list = from client in conn.Table<Client>()
                       from livingPlace in conn.Table<LivingPlace>()
                       from employee in conn.Table<Employee>()
                       where client.Id == livingPlace.Client_id
                       where client.Employee_id == employee.Id
                       select new
                       {
                           brand_name = livingPlace.BrandName,
                           model_name = livingPlace.ModelName,
                           serial_number = livingPlace.SerialNumber,
                           guarantee_number = livingPlace.GuaranteeNumber,
                           client_name = client.FirstName,
                           client_lastname = client.LastName,
                           client_patronymic = client.Patronymic,
                           data_sell = livingPlace.DateSold,
                           data_start_exp = livingPlace.DateStartExp,
                           living_city = livingPlace.City,
                           living_street = livingPlace.Street,
                           living_house = livingPlace.HouseNumber,
                           living_flat_number = livingPlace.FlatNumber,
                           client_phonenumber = client.PhoneNumber,
                           client_hphonenumber = client.HphoneNumber,
                           client_employee_lastname = employee.LastName
                       };


            ExcelService excelService = new ExcelService();

            var fileName = $"Client_list-{Guid.NewGuid()}.xlsx";
            string filepath = excelService.GenerateExcel(fileName);

            var data = new ExcelStructure
            {
                Headers = new List<string>() { "Изготовитель", "Модель", "Серийный №", "№ гар.талона", "Клиент", "Дата пуска", "Сотрудник", "Дата продажи", "Населенный пункт", "Улица", "Дом", "Кв", "Моб.телефон", "Дом.Телефон" }
            };

            foreach (var item in list)
            {
                data.Values.Add(new List<string>() { item.brand_name, item.model_name, item.serial_number, item.guarantee_number, item.client_lastname + " " + item.client_name + " " + item.client_patronymic, item.data_start_exp, item.client_employee_lastname, item.data_sell, item.living_city, item.living_street, item.living_house, item.living_flat_number, item.client_phonenumber, item.client_hphonenumber });
            }


            excelService.InsertDataIntoSheet(filepath, "Contacts", data);

            await Launcher.OpenAsync(new OpenFileRequest()
            {
                File = new ReadOnlyFile(filepath)
            });

            var filePaths = Path.Combine(FileSystem.CacheDirectory, filepath);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(filePaths)
            });


        }

        private async void searchanim_Clicked(object sender, EventArgs e)
        {

            await searchanim.ScaleTo(0.8, 150, Easing.Linear);
            await Task.Delay(250);
            await searchanim.ScaleTo(1, 150, Easing.Linear);
                var result = await Navigation.ShowPopupAsync(new SearchClientBar("mainpage"));
        }

        private async void l_b_Clicked(object sender, EventArgs e)
        {
            await l_b.ScaleTo(0.8,75, Easing.Linear);
            await Task.Delay(50);
            await l_b.ScaleTo(1, 75, Easing.Linear);
            if (current_page - 1 != 0)
            {
                current_page -= 1;
                MessagingCenter.Send("PrevPage", "UpdateListView", "Success");
            }
        }

        private async void r_b_Clicked(object sender, EventArgs e)
        {
            await r_b.ScaleTo(0.8, 75, Easing.Linear);
            await Task.Delay(50);
            await r_b.ScaleTo(1, 75, Easing.Linear);
            if (current_page+1<=pages)
            current_page += 1;
            MessagingCenter.Send("NextPage", "UpdateListView", "Success");
        }
    }
}
