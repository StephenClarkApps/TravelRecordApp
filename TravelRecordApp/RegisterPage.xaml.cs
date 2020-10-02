using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {
                // We register the user
                Users user = new Users()
                {
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text
                };

                await App.MobileService.GetTable<Users>().InsertAsync(user);

            }
            else
            {
                DisplayAlert("Error", "Password don't match", "Ok");
            }
        }
    }
}
