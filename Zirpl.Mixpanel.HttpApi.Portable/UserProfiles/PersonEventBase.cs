using System;

namespace Zirpl.Mixpanel.HttpApi.UserProfiles
{
    public abstract class PersonEventBase : ICommonEvent
    {
        protected internal PersonEventBase()
        {
            this.Options = new Options();
        }

        public Options Options { get; private set; }

        /// <summary>
        /// $ip
        /// string
        /// 
        /// The IP address associated with a given profile. If $ip isn't provided 
        /// (and ip=0 isn't provided as a URL parameter), Mixpanel will use the IP 
        /// address of the request. Mixpanel uses an IP address to guess at the
        /// geographic location of users. If $ip is set to "0", Mixpanel will ignore 
        /// IP information.
        /// 
        /// See also: https://mixpanel.com/help/reference/http#people-analytics-updates
        /// </summary>
        public String IpAddress { get; set; }

        /// <summary>
        /// $distinct_id
        /// string
        /// 
        /// This is a string that identifies the profile you would like to update. 
        /// Updates with the same $distinct_id refer to the same profile. 
        /// If this $distinct_id matches a distinct_id you use in your events, 
        /// those events will show up in the activity feed associated with the 
        /// profile you've updated.
        /// 
        /// See also: https://mixpanel.com/help/reference/http#people-analytics-updates
        /// </summary>
        public String DistinctUserId { get; set; }

        /// <summary>
        /// $token
        /// string
        /// 
        /// The Mixpanel token associated with your project. You can find your Mixpanel 
        /// token in the project settings dialog in the Mixpanel app.
        /// 
        /// See also: https://mixpanel.com/help/reference/http#people-analytics-updates
        /// </summary>
        public String ProjectToken { get; internal set; }

        /// <summary>
        /// $time
        /// integer
        /// 
        /// Milliseconds since midnight, January 1st 1970, UTC. Updates are applied in 
        /// $time order, so setting this value can lead to unexpected results unless 
        /// care is taken. If $time is not included in a request, Mixpanel will use 
        /// the time the update arrives at the Mixpanel server.
        /// 
        /// See also: https://mixpanel.com/help/reference/http#people-analytics-updates
        /// </summary>
        public DateTime? EventTime { get; set; }

        /// <summary>
        /// $ignore_time
        /// true or false
        /// 
        /// If the $ignore_time property is present and true in your update request, 
        /// Mixpanel will not automatically update the "Last Seen" property of the profile. 
        /// Otherwise, Mixpanel will add a "Last Seen" property associated with the 
        /// current time for all $set, $append, and $add operations
        /// 
        /// See also: https://mixpanel.com/help/reference/http#people-analytics-updates
        /// </summary>
        public bool? IgnoreTime { get; set; }
    }
}
