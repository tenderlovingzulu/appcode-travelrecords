using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using SQLite;
using TravelRecordApp.Logic;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync();
			var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
			VenueListView.ItemsSource = venues;
		}

		private void SaveTravelItem_Clicked(object sender, EventArgs e)
		{
			try
			{
				var selectedVenue = VenueListView.SelectedItem as Venue;
				var firstCategory = selectedVenue.categories.FirstOrDefault();

				Post post = new Post()
				{
					Experience = ExperienceEntry.Text,
					CategoryId = firstCategory.id,
					CategoryName = firstCategory.name,
					VenueName = selectedVenue.name,
					Address = selectedVenue.location.address,
					Latitude = selectedVenue.location.lat,
					Longitude = selectedVenue.location.lng,
					Distance = selectedVenue.location.distance
				};

				using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
				{
					conn.CreateTable<Post>();
					int rows = conn.Insert(post);

					if (rows > 0)
						DisplayAlert("Success", "Experience successfully inserted.", "OK");
					else
						DisplayAlert("Failure", "Experience failed to be inserted.", "OK");
				}
			}
			catch { }
		}
	}
}