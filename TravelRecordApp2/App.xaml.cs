﻿using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp2
{
	public partial class App : Application
	{
		public static string DatabaseLocation = string.Empty;
		public static MobileServiceClient client = new MobileServiceClient("https://app-travel-records-xamarin.azurewebsites.net");

		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

		public App(string databaseLocation)
		{
			InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
			DatabaseLocation = databaseLocation;
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
