using SQLite;
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
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Post>();
				var posts = conn.Table<Post>().ToList();
				PostListView.ItemsSource = posts;
			}
		}

		private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (PostListView.SelectedItem is Post selectedPost)
			{
				Navigation.PushAsync(new PostDetailPage(selectedPost));
			}
		}
	}
}