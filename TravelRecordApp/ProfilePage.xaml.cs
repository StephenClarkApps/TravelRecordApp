
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;

namespace TravelRecordApp
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var postTable = await Post.Read();

            categoriesListView.ItemsSource = Post.PostCategories(postTable);

            postCountLabel.Text = postTable.Count.ToString();

        }
    }
}
