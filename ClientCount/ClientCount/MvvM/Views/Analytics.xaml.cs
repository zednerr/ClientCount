using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entry = Microcharts.ChartEntry;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Microcharts;
using ClientCount.MvvM.ViewModels;
using DocumentFormat.OpenXml.Spreadsheet;
using ClientCount.Services;
using SQLite;
using Xamarin.CommunityToolkit.Extensions;
using ClientCount.Models;

namespace ClientCount.MvvM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Analytics : ContentPage
    {
        SQLiteConnection conn = App.DataBase.Connection;
        List<string> Sourceitems = new List<string> { "Employees", "Equipments","Tasks"};
        List<string> DateItems = new List<string> { "for the last month", "in half a year", "for the last year", "for all time" };
        List<AreaOutput> entr = new List<AreaOutput>();
        public void displaychart()
        {
            switch (SourceSel.SelectedItem)
            {
                case "Employees":
                    switch (TimeLine.SelectedItem)
                    {
                        case "for the last month":
                            entr = conn.Query<AreaOutput>("select employee.LastName as Category,COUNT(*) as count from employee,livingplace,client WHERE employee.Id=client.Employee_id AND client.Id=livingplace.Client_id AND DateStartExp >= date('now','start of month') AND DateStartExp <= datetime('now', 'start of month', '+1 month', '-1 day') group by employee.LastName");
                            break;
                        case "in half a year":
                            entr = conn.Query<AreaOutput>("select employee.LastName as Category,COUNT(*) as count from employee,livingplace,client WHERE employee.Id=client.Employee_id AND client.Id=livingplace.Client_id AND DateStartExp >= date('now','-6 month') group by employee.LastName");
                            break;
                        case "for the last year":
                            entr = conn.Query<AreaOutput>("select employee.LastName as Category,COUNT(*) as count from employee,livingplace,client WHERE employee.Id=client.Employee_id AND client.Id=livingplace.Client_id AND DateStartExp >= date('now','-1 year') group by employee.LastName");
                            break;
                        case "for all time":
                            entr = conn.Query<AreaOutput>("select employee.LastName as Category,COUNT(*) as count from employee,livingplace,client WHERE employee.Id=client.Employee_id AND client.Id=livingplace.Client_id group by employee.LastName");
                            break;
                    }
                    break;
                case "Equipments":
                    switch(TimeLine.SelectedItem)
                    {
                        case "for the last month":
                            entr = conn.Query<AreaOutput>("select ModelName as Category,COUNT(*) as count from livingplace WHERE DateStartExp >= date('now','start of month') AND DateStartExp <= datetime('now', 'start of month', '+1 month', '-1 day') group by ModelName");
                            break;
                        case "in half a year":
                            entr = conn.Query<AreaOutput>("select ModelName as Category,COUNT(*) as count from livingplace WHERE DateStartExp >= date('now','-6 month') group by ModelName");
                            break;
                        case "for the last year":
                            entr = conn.Query<AreaOutput>("select ModelName as Category,COUNT(*) as count from livingplace WHERE DateStartExp >= date('now','-1 year') group by ModelName");
                            break;
                        case "for all time":
                            entr = conn.Query<AreaOutput>("select ModelName as Category,COUNT(*) as count from livingplace group by ModelName");
                            break;
                    }
                    break;
                case "Tasks":
                    switch (TimeLine.SelectedItem)
                    {
                        case "for the last month":
                            entr = conn.Query<AreaOutput>("select DateAction as Category,COUNT(*) as count from actions WHERE DateAction >= date('now','start of month') AND DateAction <= datetime('now', 'start of month', '+1 month', '-1 day') group by DateAction");
                            break;
                        case "in half a year":
                            entr = conn.Query<AreaOutput>("select DateAction as Category,COUNT(*) as count from actions WHERE DateAction >= date('now','-6 month') group by DateAction");
                            break;
                        case "for the last year":
                            entr = conn.Query<AreaOutput>("select DateAction as Category,COUNT(*) as count from actions WHERE DateAction >= date('now','-1 year') group by DateAction");
                            break;
                        case "for all time":
                            entr = conn.Query<AreaOutput>("select DateAction as Category,COUNT(*) as count from actions group by DateAction");
                            break;
                    }
                  
                    break;
            }
            var entries = new List<Entry>();
            foreach (var data in entr)
            {
                Random ran = new Random();
                SKColor sKColor = SKColor.FromHsv(ran.Next(256), ran.Next(256), ran.Next(256));
               var entry = new Entry(data.count)
                {
                    Label = data.Category,
                    ValueLabel = data.count.ToString(),
                    Color = sKColor
                };
                entries.Add(entry);
            }
            //2:05
            var chart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 50,
            };
            Base_Chart.Chart = chart;
        }
        public Analytics()
        {
            InitializeComponent();
            SourceSel.ItemsSource =Sourceitems;
            SourceSel.SelectedIndex = 1;
            TimeLine.ItemsSource = DateItems;
            TimeLine.SelectedIndex = 0;
            displaychart();
        }
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<string, string>("Analytics", "UpdateChart", (sender, result) => {
                displaychart();
            });
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

        private void SourceSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessagingCenter.Send("Picker1", "UpdateChart","Success");
        }

        private void TimeLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessagingCenter.Send("Picker2", "UpdateChart", "Success");
        }
    }
    internal class AreaOutput
    {
        public string Date { get; set; }
        public string Category { get; set; }
        public int count { get; set; }
    }
}