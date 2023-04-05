using ClientCount.Models;
using ClientCount.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using ClientCount.MvvM.Views;

namespace ClientCount.MvvM.ViewModels
{
    public class LivingPlaceDetailModel:ViewModelBase
    {
        private readonly LivingPlaceService livingPlaceService;

        private readonly LivingPlace Current_livingPlace;

        private string city;
        private string houseNumber;
        private string region;
        private string street;
        private int client_id;
        private string flatNumber;
        public string City
        {
            get
            {
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

        public LivingPlaceDetailModel(LivingPlace lPlace)
        {
           livingPlaceService = new LivingPlaceService();
            Current_livingPlace = lPlace;

            Region = Current_livingPlace.Region;
            City = Current_livingPlace.City;
            Street = Current_livingPlace.Street;
            HouseNumber = Current_livingPlace.HouseNumber;
            FlatNumber = Current_livingPlace.FlatNumber;
            BrandName = Current_livingPlace.BrandName;
            DateSold = Current_livingPlace.DateSold;
            DateStartExp = Current_livingPlace.DateStartExp;
            GuaranteeNumber = Current_livingPlace.GuaranteeNumber;
            ModelName = Current_livingPlace.ModelName;
            SerialNumber = Current_livingPlace.SerialNumber;
            TypeModel = Current_livingPlace.TypeModel;
            Client_id = Current_livingPlace.Client_id;
        }
        public ICommand AddActionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Navigation.PushAsync(new NavigationPage(new AddActionsPage(Current_livingPlace)));
                });
            }
        }
        public ICommand UpdateLivingPlaceCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        Current_livingPlace.Region = region;
                        Current_livingPlace.City = city;
                        Current_livingPlace.Street = street;
                        Current_livingPlace.HouseNumber = houseNumber;
                        Current_livingPlace.FlatNumber = flatNumber;
                        Current_livingPlace.BrandName = brandName;
                        Current_livingPlace.DateSold = Convert.ToDateTime(dateSold).ToString("yyyy-MM-dd");
                        Current_livingPlace.DateStartExp = Convert.ToDateTime(dateStartExp).ToString("yyyy-MM-dd");
                        Current_livingPlace.GuaranteeNumber = guaranteeNumber;
                        Current_livingPlace.ModelName = modelName;
                        Current_livingPlace.SerialNumber = serialNumber;
                        Current_livingPlace.TypeModel = typeModel;
                        Current_livingPlace.Client_id = client_id;

                        int result = livingPlaceService.UpdateLivingPlace(Current_livingPlace);

                        if (result > 0)
                        {
                            var option = new ToastView("Data updated successfully!");
                            MessagingCenter.Send("LivingPlaceUpdateDetails", "UpdateLivingPlaceView", "Success");
                            await App.Current.MainPage.DisplayToastAsync(option.ToastOptions());
                        }
                    }
                    catch (NullReferenceException) {

                        await App.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");
                    }
                    catch (SQLite.NotNullConstraintViolationException)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Fill in the blanks!", "Ok");
                    }
                });
            }
        }
        public ICommand DeleteLivingPlaceCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                    bool accepted = await Application.Current.MainPage.DisplayAlert("Delete", "Are you sure you want to delete the address ?", "Yes", "No");
                    if (accepted)
                    {
                        LivingPlaceService lpservice = new LivingPlaceService();
                        int result = lpservice.DeleteLivingPlace(Current_livingPlace);
                        if (result > 0)
                        {
                            MessagingCenter.Send("LivingPlaceDetails", "UpdateLivingPlaceView", "Success");
                            await App.Navigation.PopAsync();
                        }
                        else
                        {
                            Message = "Ваш адрес не удалён!";
                        }
                    }

                });
            }
        }

    }
}
