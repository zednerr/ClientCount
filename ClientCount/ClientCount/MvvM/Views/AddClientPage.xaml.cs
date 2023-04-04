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
    public partial class AddClientPage : ContentPage
    {
         public AddClientPage()
        {
            InitializeComponent();
            BindingContext = new AddClientViewModel();
        }
    }
}