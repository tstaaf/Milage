using Milage.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Milage.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripEntryPage : ContentPage
    {
        public TripEntryPage()
        {
            InitializeComponent();
        }

        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var trip = (Trips)BindingContext;
            try
            {
                if(LocationEntry.Text == null || KilometerEntry.Text == null)
                {
                    await DisplayAlert("Invalid entry", "One or more fields are empty", "Ok");
                }
                else
                {
                    trip.Location = LocationEntry.Text;
                    trip.Date = DateTime.Now;
                    trip.Month = DateTime.Now.ToString("MMM");
                    trip.Kilometers = KilometerEntry.Text;
                    await App.Database.SaveTripAsync(trip);
                    await Navigation.PopAsync();
                }
                
            }
            catch(Exception)
            {
               await DisplayAlert("Error", "Something went wrong", "Ok");
            }           
        }

        async void DeleteToolBar_Clicked(object sender, EventArgs e)
        {
            var trip = (Trips)BindingContext;
            await App.Database.DeleteTripAsync(trip);

            await Navigation.PopAsync();
        }
    }
}