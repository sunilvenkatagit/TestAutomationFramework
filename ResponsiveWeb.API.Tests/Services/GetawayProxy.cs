using System;

namespace ResponsiveWeb.API.Services
{
    public class GetawayProxy : BaseClass
    {
        private readonly string _baseUrl;

        // {Constructor}
        public GetawayProxy() => _baseUrl = GetProxyUrl("getaway");

        #region ==> Alert End Points
        public string AllAlerts()
        {
            return $"{ _baseUrl }/allalerts";
        }
        public string TotalAlertsFor(string routeMedium)
        {
            return $"{ _baseUrl }/allalerts/{ routeMedium }/TotalCount";
        }
        public string BannerAlerts()
        {
            return $"{ _baseUrl }/banneralerts";
        }
        public string WebBannerAlerts()
        {
            return $"{ _baseUrl }/webbanneralerts";
        }
        public string LastRefresh()
        {
            return $"{ _baseUrl }/lastrefresh";
        }
        #endregion ==> Alert End Points

        #region ==> SiteNav End Points
        public string SiteNav(string menuId, string apiToken)
        {
            return $"{ _baseUrl }/sitenav-v2?menuId={ menuId }&apiToken={ apiToken }";
        }
        public string SiteNavCacheReset(string apiKey)
        {
            return $"{ _baseUrl }/SiteNavCacheReset?code={ apiKey }";
        }
        #endregion ==> SiteNav End Points

        #region ==> Feedback form End Points
        // No API automation yet for Feedback form
        #endregion ==> Feedback form End Points

        #region ==> Twitter End Points
        public string TwitterTweetsFor(string accountName = null, int count = 0)
        {
            string twitterEndpoint = $"{ _baseUrl }/TwitterGetTweets";

            if (!string.IsNullOrEmpty(accountName) && !(count == 0))
            {
                twitterEndpoint = $"{ _baseUrl }/TwitterGetTweets?screen_name={ accountName }&count={ count }";
            }

            return twitterEndpoint;
        }
        #endregion ==> Twitter End Points

        #region ==> GTFS End Points
        public string GtfsRoutes()
        {
            return $"{ _baseUrl }/gtfs/routes";
        }
        public string GtfsRouteSchedule(string route, int direction, string date)
        {
            return $"{ _baseUrl }/gtfs/route/{route}/direction/{direction}/schedules/{date}";
        }
        public string GtfsStopSchedule_Static(int stopNumber, string date = null)
        {
            return $"{ _baseUrl }/gtfs/stop/{stopNumber}/schedules/{date}";
        }
        public string GtfsStopSchedule_RealTime(int stopNumber)
        {
            return $"{ _baseUrl }/gtfs/stop/{stopNumber}/realtimeschedules";
        }
        public string GtfsStationSchedule_Static(int stationId, string date = null)
        {
            return $"{ _baseUrl }/gtfs/station/{stationId}/schedules/{date}";
        }
        public string GtfsStationSchedule_RealTime(int stationId)
        {
            return $"{ _baseUrl }/gtfs/station/{stationId}/realtimeschedules";
        }
        public string GtfsLastRefresh()
        {
            return $"{ _baseUrl }/gtfs/lastdatarefresh";
        }
        public string GtfsMaxCalendarDate()
        {
            return $"{ _baseUrl }/gtfs/maxcalendardate";
        }

        public string GtfsRouteDirectionShapes(string route, int direction)
        {
            return $"{_baseUrl}/gtfs/route/{route}/direction/{direction}/shapes";
        }
        #endregion ==> GTFS End Points

        #region ==> Search API End Points
        public string SearchAPI()
        {
            return $"{ _baseUrl }/Search";
        }
        #endregion ==> Search API End Points
    }
}
