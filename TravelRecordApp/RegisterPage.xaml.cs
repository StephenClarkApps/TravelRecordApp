using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        void RegisterButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {
                // We register the user
            }
            else
            {
                DisplayAlert("Error", "Password don't match", "Ok");
            }
        }
    }
}
