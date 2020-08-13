using System;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{
    public class Venue
    {
        public Venue()
        {
        }

        public static string GenerateURL(double latitude, double logitude)
        {
            return string.Format(Constants.VENUE_SEARCH, latitude, logitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
        }
    }
}
