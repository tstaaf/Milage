using Milage.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Collections.Generic;

namespace Milage.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripsPage : ContentPage
    {
        public TripsPage()
        {
            InitializeComponent();
            MonthPicker.Items.Add("Jan");
            MonthPicker.Items.Add("Feb");
            MonthPicker.Items.Add("Mar");
            MonthPicker.Items.Add("Apr");
            MonthPicker.Items.Add("May");
            MonthPicker.Items.Add("Jun");
            MonthPicker.Items.Add("Jul");
            MonthPicker.Items.Add("Aug");
            MonthPicker.Items.Add("Sep");
            MonthPicker.Items.Add("Oct");
            MonthPicker.Items.Add("Nov");
            MonthPicker.Items.Add("Dec");
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            string currentMonth = DateTime.Now.ToString("MMM");
            MonthPicker.SelectedItem = currentMonth;
            var trips = await App.Database.GetTripsAsync();
            TripList.ItemsSource = trips;

            //var kmSum = trips.Where(c => c.Month == currentMonth).Sum(k => Convert.ToInt32(k.Kilometers));
            //var tripsDistance = App.Database.GetTotalDistance().Result.ToString();

            try
            {
                TotalDistance.Text = "Total: " + GetTotalKilometers(trips) + " km";
            }
            catch (Exception)
            {
                TotalDistance.Text = "Total: 0km";
            }
        }
        private int GetTotalKilometers(List<Trips> trips)
        {
            string currentMonth = DateTime.Now.ToString("MMM");
            var kmSum = trips.Where(c => c.Month == currentMonth).Sum(k => Convert.ToInt32(k.Kilometers));
            return kmSum;

        }
        private async void MonthPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var dbTrips = await App.Database.GetTripsAsync();
                var SortedTrips = dbTrips.FindAll(m => m.Month == MonthPicker.SelectedItem.ToString());
                if (SortedTrips.Count <= 0)
                {
                    await DisplayAlert("No entries", "No entries on selected month", "Ok");
                    TripList.ItemsSource = "";
                    TotalDistance.Text = "Total: 0 km";
                }
                else
                {
                    TripList.ItemsSource = SortedTrips;
                    TotalDistance.Text = "Total: " + GetTotalKilometers(dbTrips) + "km";
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Something went wrong" + ex.ToString(), "Ok");
            }
        }

        async void OnTripAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TripEntryPage
            {
                BindingContext = new Trips()
            });
        }

        async void TripList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TripEntryPage
                {
                    BindingContext = e.SelectedItem as Trips
                });
            }
        }

        async void OnToolBarDelete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete", "Are you sure you want to delete all entries?", "Yes", "No");

            if (answer)
            {
                await App.Database.DeleteAllTripsAsync();
                TripList.ItemsSource = await App.Database.GetTripsAsync();
            }

        }
    }
}