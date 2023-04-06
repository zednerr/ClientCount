using ClientCount.Models;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientCount.MvvM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchClientBar : Popup
    {
        private readonly string _page;
        private readonly int _curclient;
        public SearchClientBar(string page)
        {
            InitializeComponent();
             _page = page;
            switch (page)
            {
                case "listemployeepage":
                    Lab.Text = "Search Employee";
                    break;
                case "mainpage":
                    Lab.Text = "Search Client";
                    break;
                case "listlivingplaceview":
                    Lab.Text = "Search LivingPlace";
                    break;
            }
        }
        public SearchClientBar(string page,Client currentclient)
        {
            InitializeComponent();
            _page = page;
            _curclient = currentclient.Id;
            switch (page)
            {
                case "listemployeepage":
                    Lab.Text = "Search Employee";
                    break;
                case "mainpage":
                    Lab.Text = "Search Client";
                    break;
                case "listlivingplaceview":
                    Lab.Text = "Search LivingPlace";
                    break;
            }
        }
        private void bar_search_TextChanged_1(object sender, TextChangedEventArgs e)
        {
    
            var conn = App.DataBase.Connection;
            switch (_page)
            {
                case "listemployeepage":
        
                    var list1 = conn.Query<Employee>($"Select id,firstname||' '||lastname as firstname,phonenumber from employee where firstname||lastname||phonenumber LIKE '%{bar_search.Text}%'");
                    MessagingCenter.Send(new SearchResult() { ReturnData_Employee = list1 }, "PopUpData");
                    break;
                case "mainpage":
                
                    var list2 = conn.Query<Client>($"Select id,firstname||' '||lastname as firstname,phonenumber from client where firstname||lastname||phonenumber LIKE '%{bar_search.Text}%'");
                    MessagingCenter.Send(list2, "PopUpData");
                    break;
                case "listlivingplaceview":
                
                    var list3 = conn.Query<LivingPlace>($"Select * from livingplace where Region||City||street||brandname||guaranteenumber||modelname||serialnumber LIKE '%{bar_search.Text}%' AND livingplace.client_id = {_curclient}");
                    MessagingCenter.Send(new SearchResult() { ReturnData_LivingPlace = list3 }, "PopUpData");
                    break;
                    default: 
                    break;
            }
            //    SearchResult.ItemsSource = vList.Where(f => f.LastName.ToLower().Contains(SearchC.Text.ToLower()));
            
        }
    }
}