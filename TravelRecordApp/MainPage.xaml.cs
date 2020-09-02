using System.ComponentModel;
using Xamarin.Forms;

namespace TravelRecordApp
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

            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assembly);
        }

        void LoginButton_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (isEmailEmpty|| isPasswordEmpty)
            {

            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
