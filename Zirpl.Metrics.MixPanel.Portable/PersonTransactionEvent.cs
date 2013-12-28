using System;

namespace Zirpl.Metrics.MixPanel
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
