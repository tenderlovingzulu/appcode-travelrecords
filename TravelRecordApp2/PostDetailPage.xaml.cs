﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostDetailPage : ContentPage
	{
		readonly Post selectedPost;

		public PostDetailPage(Post selectedPost)
		{
			InitializeComponent();

			this.selectedPost = selectedPost;
			ExperienceEntry.Text = selectedPost.Experience;
		}

		private void UpdateButton_Clicked(object sender, EventArgs e)
		{
			selectedPost.Experience = ExperienceEntry.Text;

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Post>();
				int rows = conn.Update(selectedPost);

				if (rows > 0)
					DisplayAlert("Success", "Experience successfully updated.", "OK");
				else
					DisplayAlert("Failure", "Experience failed to be updated.", "OK");
			}
		}

		private void DeleteButton_Clicked(object sender, EventArgs e)
		{
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Post>();
				int rows = conn.Delete(selectedPost);

				if (rows > 0)
					DisplayAlert("Success", "Experience successfully deleted.", "OK");
				else
					DisplayAlert("Failure", "Experience failed to be deleted.", "OK");
			}
		}
	}
}