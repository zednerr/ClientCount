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
    public partial class AddActionsPage : ContentPage
    {
        public AddActionsPage(LivingPlace lPlace)
        {
            InitializeComponent();
            BindingContext = new AddActionsViewModel(lPlace);
            List<string> ActionList = new List<string>() { "TRP Call", "TRP Service", "TO Call", "TO Service" };
            actionslist.ItemsSource = ActionList;
        }

        
    }
}