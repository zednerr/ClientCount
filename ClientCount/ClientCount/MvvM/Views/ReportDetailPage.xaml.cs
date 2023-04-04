using ClientCount.Models;
using ClientCount.Services;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientCount.MvvM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportDetailPage : Popup
	{
		public ReportDetailPage ()
		{
			InitializeComponent ();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            Size size = new Size();
            size.Width = mainDisplayInfo.Width / mainDisplayInfo.Density;
            size.Height = 240;
            Size = size;
            Content_s.WidthRequest =size.Width-80;
            Content_s.HeightRequest = size.Height;
            var options = new List<string> { "За весь час", "За поточний місяць", "Вручну" };
            picker_fil.ItemsSource = options;
            picker_fil.SelectedItem = "За весь час";
            d_l.Date = DateTime.Now;
            d_u.Date = DateTime.Now;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string date_low="";
            string date_up="";
            var conn = App.DataBase.Connection;
            var selected_options = picker_fil.SelectedItem;
            switch (selected_options)
            {
                case "За весь час":
                    var date_l = conn.Query<LivingPlace>("SELECT livingPlace.DateStartExp FROM livingplace ORDER BY livingPlace.DateStartExp ASC LIMIT 1");
                    var date_u= conn.Query<LivingPlace>("SELECT livingPlace.DateStartExp FROM livingplace ORDER BY livingPlace.DateStartExp DESC LIMIT 1");
                    date_low = date_l[0].DateStartExp;
                    date_up = date_u[0].DateStartExp;
                    break;
                case "За поточний місяць":
                     date_l = conn.Query<LivingPlace>("SELECT DateStartExp FROM livingplace WHERE DateStartExp >= date('now','start of month') AND DateStartExp <= datetime('now', 'start of month', '+1 month', '-1 day') ORDER BY DateStartExp ASC LIMIT 1");
                     date_u = conn.Query<LivingPlace>("SELECT DateStartExp FROM livingplace WHERE DateStartExp >= date('now','start of month') AND DateStartExp <= datetime('now', 'start of month', '+1 month', '-1 day') ORDER BY DateStartExp DESC LIMIT 1");
                    date_low = date_l[0].DateStartExp;
                    date_up = date_u[0].DateStartExp;
                    break;
                case "Вручну":
                    date_low = d_l.Date.ToString("yyyy-MM-dd");
                    date_up = d_u.Date.ToString("yyyy-MM-dd");
                    break;
                default:
                    return;
            }
            var list = from client in conn.Table<Client>()
                       from livingPlace in conn.Table<LivingPlace>()
                       from employee in conn.Table<Employee>()
                       where client.Id == livingPlace.Client_id
                       where client.Employee_id == employee.Id
                       where Convert.ToDateTime(livingPlace.DateStartExp) >= Convert.ToDateTime(date_low)
                       where Convert.ToDateTime(livingPlace.DateStartExp) <= Convert.ToDateTime(date_up)
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
            var fileName = $"Звіт-{DateTime.Now.ToString("dd-mm-yyyy")}.xlsx";
            string filepath = excelService.GenerateExcel(fileName);

            var data = new ExcelStructure
            {
                Headers = new List<string>() { "Виробник", "Модель", "Серійний №", "№ Гар.талона", "Клієнт", "Дата пуску", "Співробітник", "Дата продажу", "Населенний пункт", "Вулиця", "Дім", "Кв", "Моб.телефон", "Дом.Телефон" }
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
                Title = "2023",
                File = new ShareFile(filePaths)
            });


        }

        private void picker_fil_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (picker_fil.SelectedItem.ToString())
            {
                case "За весь час":

                    d_u.IsEnabled = false;

                    d_l.IsEnabled = false;
                    break;
                case "За поточний місяць":

                    d_u.IsEnabled = false;

                    d_l.IsEnabled = false;
                    break;
                case "Вручну":

                    d_u.IsEnabled = true;

                    d_l.IsEnabled = true;
                    break;
            }           
        }

        private void d_l_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            d_u.MinimumDate = d_l.Date;
        }
    }
}