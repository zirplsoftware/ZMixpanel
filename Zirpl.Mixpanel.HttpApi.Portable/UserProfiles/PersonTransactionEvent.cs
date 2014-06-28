using System;

namespace Zirpl.Mixpanel.HttpApi.UserProfiles
{
    public class PersonTransactionEvent: PersonEventBase
    {
        protected internal PersonTransactionEvent()
        {
            
        }
        public DateTime TransactionDateTime { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}
