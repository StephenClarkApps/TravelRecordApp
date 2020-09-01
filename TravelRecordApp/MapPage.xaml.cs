using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        IGeolocator locator = CrossGeolocator.Current;

        private bool hasLocationPermission = false;

        public MapPage()
        {
            InitializeComponent();

            GetPermissions();
        }

        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Need your location", "We need to access your location", "Ok");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (results.ContainsKey(Permission.LocationWhenInUse))
                        status = results[Permission.LocationWhenInUse];
                }

                if (status == PermissionStatus.Granted)
                {
                    hasLocationPermission = true;
                    locationsMap.IsShowingUser = true;

                    GetLocation();
                }
                else
                {
                    await DisplayAlert("Location denied", "You didn't give us permission to access location, so we can't show you where you are", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (hasLocationPermission)
            {
                if (CrossGeolocator.IsSupported && CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled)
                {
                    locator.PositionChanged += Locator_PositionChanged;
                    await locator.StartListeningAsync(TimeSpan.FromSeconds(0), 50, true);

                    var position = await locator.GetPositionAsync();

                    var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                    var span = new Xamarin.Forms.Maps.MapSpan(center, 2, 2);

                    locationsMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude),
                                 Distance.FromMiles(1)));

                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Post>();
                        var posts = conn.Table<Post>().ToList();

                        DisplayInMap(posts);
                    }
                }
            }
        }

        private void DisplayInMap(List<Post> posts)
        {
            foreach(var post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Logitude);

                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = post.VenueName,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (NullReferenceException nre) { }
                catch (Exception ex) { }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //var locator = CrossGeolocator.Current;
            locator.StopListeningAsync();
            locator.PositionChanged -= Locator_PositionChanged;
        }

        void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var position = await locator.GetPositionAsync();
                MoveMap(position);
            }
        }

        private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
        {
            var span = (MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude),
                                 Distance.FromMiles(1)));

            locationsMap.MoveToRegion(span);
        }
    }
}