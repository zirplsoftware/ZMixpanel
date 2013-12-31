using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Metrics.MixPanel.Web.Mvc
{
    public class InstanceHelper
    {
        private string _instanceName;

        public InstanceHelper Instance(String instanceName)
        {
            this._instanceName = instanceName;
            return this;
        }

        public InitBuilder CallInit()
        {
            return new InitBuilder();
        }

        public ConfigBuilder CallSetConfig()
        {
            return new SetConfigBuilder(this._instanceName).Config();
        }

        public AliasBuilder CallAlias(String alias)
        {
            return new AliasBuilder(this._instanceName).Alias(alias);
        }

        public IdentifyBuilder CallIdentify(String distinctId)
        {
            return new IdentifyBuilder(this._instanceName).DistinctId(distinctId);
        }

        public NameTagBuilder CallNameTag(String nameTag)
        {
            return new NameTagBuilder(this._instanceName).NameTag(nameTag);
        }

        public TrackPageViewBuilder CallTrackPageView()
        {
            return new TrackPageViewBuilder(this._instanceName);
        }

        public TrackPageViewBuilder CallTrackPageView(String page)
        {
            return new TrackPageViewBuilder(this._instanceName).Page(page);
        }

        public TrackBuilder CallTrack()
        {
            return new TrackBuilder(this._instanceName);
        }

        public TrackBuilder CallTrack(String eventName)
        {
            return new TrackBuilder(this._instanceName).EventName(eventName);
        }
    }
}
