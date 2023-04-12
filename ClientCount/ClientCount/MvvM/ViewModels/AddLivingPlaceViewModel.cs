using ClientCount.Models;
using ClientCount.MvvM.Views;
using ClientCount.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace ClientCount.MvvM.ViewModels.client
{
    public class AddLivingPlaceViewModel: ViewModelBase
    {
       
       
        private readonly LivingPlaceService livingPlaceService;

        private readonly Client Current_client;
       
        public AddLivingPlaceViewModel(Client client)
        {
            livingPlaceService = new LivingPlaceService();
            Current_client = client;
            Client_id =Current_client.Id;
        }
  

       

        private string city;
        private string houseNumber;
        private string region;
        private string street;
        private int client_id;
        private string flatNumber;
        public bool CanFormData(params string[] n)
        {
            for (int i = 0; i < n.Length; i++)
            {
                if (string.IsNullOrEmpty(n[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public string City { 
            get {
                return city;
            }
            set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged("City");
                }
            }
        }
        public string HouseNumber
        {
            get
            {
                return houseNumber;
            }
            set
            {
                if (houseNumber != value)
                {
                    houseNumber = value;
                    OnPropertyChanged("HouseNumber");
                }
            }
        }


        public string Region
        {
            get
            {
                return region;
            }
            set
            {
                if (region != value)
                {
                    region = value;
                    OnPropertyChanged("Region");
                }
            }
        }

        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                if (street != value)
                {
                    street = value;
                    OnPropertyChanged("Street");
                }
            }
        }


        public int Client_id
        {
            get
            {
                return client_id;
            }
            set
            {
                if (client_id != value)
                {
                    client_id = value;
                    OnPropertyChanged("Client_id");
                }
            }
        }

        public string FlatNumber
        {
            get
            {
                return flatNumber;
            }
            set
            {
                if (flatNumber != value)
                {
                    flatNumber = value;
                    OnPropertyChanged("FlatNumber");
                }
            }
        }
        private string brandName;
        public string BrandName
        {
            get
            {
                return brandName;
            }
            set
            {
                if (brandName != value)
                {
                    brandName = value;
                    OnPropertyChanged("BrandName");
                }
            }
        }
        private string guaranteeNumber;
        public string GuaranteeNumber
        {
            get
            {
                return guaranteeNumber;
            }
            set
            {
                if (guaranteeNumber != value)
                {
                    guaranteeNumber = value;
                    OnPropertyChanged("GuaranteeNumber");
                }
            }
        }

        private string modelName;
        public string ModelName
        {
            get
            {
                return modelName;
            }
            set
            {
                if (modelName != value)
                {
                    modelName = value;
                    OnPropertyChanged("ModelName");
                }
            }
        }

        private string serialNumber;
        public string SerialNumber
        {
            get
            {
                return serialNumber;
            }
            set
            {
                if (serialNumber != value)
                {
                    serialNumber = value;
                    OnPropertyChanged("SerialNumber");
                }
            }
        }

        private string typeModel;
        public string TypeModel
        {
            get
            {
                return typeModel;
            }
            set
            {
                if (typeModel != value)
                {
                    typeModel = value;
                    OnPropertyChanged("TypeModel");
                }
            }
        }

        private string dateSold;
        public string DateSold
        {
            get
            {
                return dateSold;
            }
            set
            {
                if (dateSold != value)
                {
                    dateSold = value;
                    OnPropertyChanged("DateSold");
                }
            }
        }

        private string dateStartExp;
        public string DateStartExp
        {
            get
            {
                return dateStartExp;
            }
            set
            {
                if (dateStartExp != value)
                {
                    dateStartExp = value;
                    OnPropertyChanged("DateStartExp");
                }
            }
        }

        private string message;
        public string Message
        {
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
            get { return message; }
        }
        public ICommand AddLivingPlaceCommand
        {
            get
            {
                return new Command(async() =>
                {

                    try
                    {
                        if (CanFormData(new string[] { city, houseNumber,Region,street,brandName,modelName,dateStartExp,serialNumber,typeModel,client_id.ToString() }) == true)
                        {
                            throw new NullReferenceException();
                        }
                        int result = livingPlaceService.CreateLivingPlace(new LivingPlace
                        {
                            Region = region,
                            City = city,
                            Street = street,
                            HouseNumber = houseNumber,
                            FlatNumber = flatNumber,
                            BrandName = brandName,
                            DateSold = Convert.ToDateTime(dateSold).ToString("yyyy-MM-dd"),
                            DateStartExp =Convert.ToDateTime(dateStartExp).ToString("yyyy-MM-dd"),
                            GuaranteeNumber = guaranteeNumber,
                            ModelName = modelName,
                            SerialNumber = serialNumber,
                            TypeModel = typeModel,
                            Client_id = client_id
                        });
                        if (result > 0)
                        {
                            var option = new ToastView("Address created successfully!");
                            MessagingCenter.Send("AddLivingPlace", "UpdateLivingPlaceView", "Success");
                            await App.Navigation.PopAsync();
                            await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
                        }
                    }
                    catch (NullReferenceException )
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");

                    }
                    catch (SQLite.NotNullConstraintViolationException )
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");
                    }
                });

            }
        }
    }
}
