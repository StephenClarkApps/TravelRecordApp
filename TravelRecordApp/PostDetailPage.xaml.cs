using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class PostDetailPage : ContentPage
    {

        Post selectedPost;

        public PostDetailPage()
        {

        }

        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();

            this.selectedPost = selectedPost;

            experienceEntry.Text = selectedPost.Experience;
            venueLabel.Text = selectedPost.VenueName;
            categoryLabel.Text = selectedPost.CategoryName;
            addressLabel.Text = selectedPost.Address;
            coordinatesLabel.Text = $"{selectedPost.Latitude},{selectedPost.Longitude}";
            distanceLabel.Text = $"{selectedPost.Distance} m";

        }

        //void updateButton_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    selectedPost.Experience = experienceEntry.Text;

        //    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
        //    {
        //        conn.CreateTable<Post>();
        //        int rows = conn.Update(selectedPost);

        //        if (rows > 0)
        //            DisplayAlert("Success", "Experience successfully updated", "Ok");
        //        else
        //            DisplayAlert("Failure", "Experience failed to be updated", "OK");
        //    }
        //}

        //void deleteButton_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
        //    {
        //        conn.CreateTable<Post>();
        //        int rows = conn.Delete(selectedPost);

        //        if (rows > 0)
        //            DisplayAlert("Success", "Experience successfully deleted", "Ok");
        //        else
        //            DisplayAlert("Failure", "Experience failed to be deleted", "OK");
        //    }

        //}

    }
}
