using System;
using System.Web;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public class PeopleSetPropertiesBuilder :PropertiesBuilder<Object>
    {
        public PeopleSetPropertiesBuilder(IHtmlString outermostContainer)
            :base(outermostContainer)
        { }

        public PeopleSetPropertiesBuilder FirstName(String value)
        {
            this.Add("$first_name", value);
            return this;
        }
        public PeopleSetPropertiesBuilder LastName(String value)
        {
            this.Add("$last_name", value);
            return this;
        }
        public PeopleSetPropertiesBuilder Name(String value)
        {
            this.Add("$name", value);
            return this;
        }
        public PeopleSetPropertiesBuilder Phone(String value)
        {
            this.Add("phone", value);
            return this;
        }
        public PeopleSetPropertiesBuilder Email(String value)
        {
            this.Add("$email", value);
            return this;
        }
        public PeopleSetPropertiesBuilder Created(DateTime? value)
        {
            this.Add("$created", value);
            return this;
        }
    }
}
