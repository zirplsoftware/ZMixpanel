using System;

namespace Zirpl.Mixpanel.Web.Mvc.JavaScript
{
    public class InstanceHelper
    {
        private string _instanceName;

        public InstanceHelper Instance(String instanceName)
        {
            this._instanceName = instanceName;
            return this;
        }

        public PeopleHelper People()
        {
            return new PeopleHelper(this._instanceName);
        }

        public InstallBuilder Install()
        {
            return new InstallBuilder();
        }

        public InitBuilder CallInit()
        {
            return new InitBuilder();
        }

        public InitBuilder CallInit(String token)
        {
            return new InitBuilder().Token(token);
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

        public TrackLinksBuilder CallTrackLinks()
        {
            return new TrackLinksBuilder(this._instanceName);
        }

        public TrackLinksBuilder CallTrackLinks(String domSelector, String eventName)
        {
            return new TrackLinksBuilder(this._instanceName).EventName(eventName).DomSelector(domSelector);
        }

        public TrackFormsBuilder CallTrackForms()
        {
            return new TrackFormsBuilder(this._instanceName);
        }

        public TrackFormsBuilder CallTrackForms(String domSelector, String eventName)
        {
            return new TrackFormsBuilder(this._instanceName).EventName(eventName).DomSelector(domSelector);
        }

        public PropertiesVariableBuilder PropertiesVariable()
        {
            return new PropertiesVariableBuilder();
        }

        public PropertiesVariableBuilder PropertiesVariable(String variableName)
        {
            return new PropertiesVariableBuilder().VariableName(variableName);
        }
    }
}
