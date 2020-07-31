using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms.Maps;

namespace TravelRecordApp2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		private bool hasLocationPermission = false;

		public MapPage()
		{
			InitializeComponent();
			GetPermissions();
		}


		private async void GetPermissions()
		{
			try
			{
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>();

				if (status != PermissionStatus.Granted)
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
						await DisplayAlert("Need your location", "We need to access your location", "Ok");

					status = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();

				}

				if (status == PermissionStatus.Granted)
				{
					hasLocationPermission = true;
					LocationsMap.IsShowingUser = true;
					GetLocation();
				}
				else
					await DisplayAlert("Location denied", "You didn't give us permission to access location, so we can't show you the map.", "Ok");
			}
			catch (Exception e)
			{
				await DisplayAlert("Error", e.Message, "Ok");
			}
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (hasLocationPermission)
			{
				var locator = CrossGeolocator.Current;
				locator.PositionChanged += Locator_PositionChanged;
				await locator.StartListeningAsync(TimeSpan.Zero, 100);
			}

			GetLocation();

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Post>();
				var posts = conn.Table<Post>().ToList();
				DisplayInMap(posts);
			}
		}

		private void DisplayInMap(List<Post> posts)
		{
			foreach (var post in posts)
			{
				try
				{
					var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);
					var pin = new Pin()
					{
						Type = PinType.SavedPin,
						Position = position,
						Label = post.VenueName,
						Address = post.Address
					};

					LocationsMap.Pins.Add(pin);
				}
				catch { }
			}
		}

		private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
		{
			MoveMap(new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude));
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			if (hasLocationPermission)
			{
				CrossGeolocator.Current.StopListeningAsync();
				CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;
			}
		}

		private async void GetLocation()
		{
			if (hasLocationPermission)
			{
				var locator = CrossGeolocator.Current;
				var position = await locator.GetPositionAsync();
				MoveMap(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));
			}
		}

		private void MoveMap(Xamarin.Forms.Maps.Position position)
		{
			var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
			var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
			LocationsMap.MoveToRegion(span);
		}
	}
}