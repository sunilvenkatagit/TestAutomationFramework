namespace ResponsiveWeb.API.Services
{
    public class PostawayProxy : BaseClass
    {
        private readonly string _baseUrl;

        // {Constructor}
        public PostawayProxy() => _baseUrl = GetProxyUrl("postaway");

        #region => RSP Profile End Points
        public string UserProfile(string profileId)
        {
            return $"{ _baseUrl }/users/{ profileId }";
        }
        #endregion => RSP Profile End Points

        #region => RSP (Registration Subscription Preferences) End Points
        public string RspAlerts(string profileId)
        {
            return $"{ _baseUrl }/rsp/{ profileId }/alerts";
        }
        public string DeliveryOptions(string profileId)
        {
            return $"{ _baseUrl }/rsp/{ profileId }/delivery/options";
        }
        public string SmsSubscription(string profileId)
        {
            return $"{ _baseUrl }/rsp/{ profileId }/delivery/options/sms";
        }
        public string SmsValidation(string profileId)
        {
            return $"{ _baseUrl }/rsp/{ profileId }/delivery/options/sms/validation";
        }
        public string SnoozeDeliverySchedule(string profileId)
        {
            return $"{ _baseUrl }/rsp/{ profileId }/delivery/snooze";
        }
        public string UnSubscribeFromEmailAndSms()
        {
            return $"{ _baseUrl }/rsp/subscriptions";
        }
        #endregion
    }
}
