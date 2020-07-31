using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				var postTable = conn.Table<Post>().ToList();

				var categories = (from p
								  in postTable
								  orderby p.CategoryId
								  select p.CategoryName).Distinct().ToList();

				var categories2 = postTable.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

				Dictionary<string, int> CountCategories = new Dictionary<string, int>();

				foreach (var category in categories2)
				{
					var count = (from post
								 in postTable
								 where post.CategoryName == category
								 select post).ToList().Count;

					var count2 = postTable.Where(p => p.CategoryName == category).ToList().Count;

					CountCategories.Add(category ?? "(unknown)", count2);
				}

				CategoriesListView.ItemsSource = CountCategories;

				PostCountLabel.Text = postTable.Count.ToString();
			}
		}
	}
}