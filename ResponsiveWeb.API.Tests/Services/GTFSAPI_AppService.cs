namespace ResponsiveWeb.API.Services
{
    public class GTFSAPI_AppService : BaseClass
    {
        private readonly string _baseUrl;

        // {Constructor}
        public GTFSAPI_AppService() => _baseUrl = GetAppService();

        #region ==> GTFS End Points
        public string GtfsRouteSchedule(string route, int direction, string date)
        {
            return $"{ _baseUrl }/route/{route}/direction/{direction}/schedules/{date}";
        }
        public string GtfsStopSchedule_Static(int stopNumber, string date = null)
        {
            return $"{ _baseUrl }/stop/{stopNumber}/schedules/{date}";
        }
        public string GtfsStopSchedule_RealTime(int stopNumber)
        {
            return $"{ _baseUrl }/stop/{stopNumber}/realtimeschedules";
        }
        public string GtfsStationSchedule_Static(int stationId, string date = null)
        {
            return $"{ _baseUrl }/station/{stationId}/schedules/{date}";
        }
        public string GtfsStationSchedule_RealTime(int stationId)
        {
            return $"{ _baseUrl }/station/{stationId}/realtimeschedules";
        }
        public string GtfsLastRefresh()
        {
            return $"{ _baseUrl }/lastgtfsrefresh";
        }
        public string GtfsMaxCalendarDate()
        {
            return $"{ _baseUrl }/maxcalendardate";
        }
        public string GtfsStop_LatLons(double topLeftLat, double topLeftLon, double bottomRightLat, double bottomRightLon)
        {
            return $"{_baseUrl}/stops/{topLeftLat},{topLeftLon}/{bottomRightLat},{bottomRightLon}";
        }
        #endregion ==> GTFS End Points
    }
}
