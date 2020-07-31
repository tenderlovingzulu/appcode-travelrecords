using System;
using TravelRecordApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage()
		{
			InitializeComponent();
		}

		private async void RegisterButton_Clicked(object sender, EventArgs e)
		{
			if (passwordEntry.Text == confirmPasswordEntry.Text)
			{
				Users user = new Users()
				{
					Email = emailEntry.Text,
					Password = passwordEntry.Text
				};

				try
				{
					await App.client.GetTable<Users>().InsertAsync(user);
				}
				catch (Exception ex)
				{
					// do nothing on error for now.
				}
			}
			else
			{
				await DisplayAlert("Error", "Passwords don't match.", "Ok");
			}
		}
	}
}