using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelRecordApp2
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			var assembly = typeof(MainPage);
			IconImage.Source = ImageSource.FromResource("TravelRecordApp2.Assets.Images.plane.png", assembly);
		}

		private void LoginButton_Clicked(object sender, EventArgs e)
		{
			bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
			bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

			if (isEmailEmpty || isPasswordEmpty)
			{
				// require both email and password.
			}
			else
			{
				Navigation.PushAsync(new HomePage());
			}
		}

		private void RegisterUserButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new RegisterPage());
		}
	}
}
