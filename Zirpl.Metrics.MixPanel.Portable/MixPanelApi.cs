using System;
using Zirpl.Logging;

namespace Zirpl.Metrics.MixPanel
{
    public class MixPanelApi
    {
        public IEventSender EventSender { get; set; }
        public ILog Log { get; set; }
        //public IIpAddressProvider ConfigurationProvider { get; set; }

        public String ProjectToken { get; set; }

        public MixPanelApi()
        {
        }

        public MixPanelApi(String projectToken)
        {
            this.ProjectToken = projectToken;
        }

        private void AssertValidProjectToken()
        {
            if (String.IsNullOrEmpty(this.ProjectToken))
            {
                throw new InvalidOperationException("Cannot create event or person without a ProjectToken");
            }
        }

        public Event CreateEvent()
        {
            this.AssertValidProjectToken();
            var eVent = new Event();
            this.OnCreateEvent(eVent);
            return eVent;
        }

        public T CreateEvent<T>() where T : Event, new()
        {
            this.AssertValidProjectToken();
            var eVent = new T();
            this.OnCreateEvent(eVent);
            return eVent;
        }

        public Event CreateEvent(String name)
        {
            this.AssertValidProjectToken();
            var eVent = new Event();
            eVent.EventName = name;
            this.OnCreateEvent(eVent);
            return eVent;
        }

        public T CreateEvent<T>(String name) where T : Event, new()
        {
            this.AssertValidProjectToken();
            var eVent = new T();
            eVent.EventName = name;
            this.OnCreateEvent(eVent);
            return eVent;
        }

        protected virtual void OnCreateEvent(Event eVent)
        {
            eVent.ProjectToken = this.ProjectToken;
        }



        public PersonIncrementEvent CreatePersonIncrementEvent()
        {
            this.AssertValidProjectToken();
            var personEvent = new PersonIncrementEvent();
            this.OnCreatePersonEvent(personEvent);
            return personEvent;
        }

        public T CreatePersonIncrementEvent<T>() where T : PersonIncrementEvent, new()
        {
            this.AssertValidProjectToken();
            var personEvent = new T();
            this.OnCreatePersonEvent(personEvent);
            return personEvent;
        }

        protected virtual void OnCreatePersonEvent(PersonIncrementEvent personEvent)
        {
            personEvent.ProjectToken = this.ProjectToken;
        }



        public PersonSetOnceEvent CreatePersonSetOnceEvent()
        {
            this.AssertValidProjectToken();
            var personEvent = new PersonSetOnceEvent();
            this.OnCreatePersonEvent(personEvent);
            return personEvent;
        }

        public T CreatePersonSetOnceEvent<T>() where T : PersonSetOnceEvent, new()
        {
            this.AssertValidProjectToken();
            var personEvent = new T();
            this.OnCreatePersonEvent(personEvent);
            return personEvent;
        }

        protected virtual void OnCreatePersonEvent(PersonSetOnceEvent personEvent)
        {
            personEvent.ProjectToken = this.ProjectToken;
        }



        public PersonSetEvent CreatePersonSetEvent()
        {
            this.AssertValidProjectToken();
            var personEvent = new PersonSetEvent();
            this.OnCreatePersonEvent(personEvent);
            return personEvent;
        }

        public T CreatePersonSetEvent<T>() where T : PersonSetEvent, new()
        {
            this.AssertValidProjectToken();
            var personEvent = new T();
            this.OnCreatePersonEvent(personEvent);
            return personEvent;
        }

        protected virtual void OnCreatePersonEvent(PersonSetEvent personEvent)
        {
            personEvent.ProjectToken = this.ProjectToken;
        }



        public PersonTransactionEvent CreatePersonTransactionEvent()
        {
            this.AssertValidProjectToken();
            var personEvent = new PersonTransactionEvent();
            this.OnCreatePersonEvent(personEvent);
            return personEvent;
        }

        public T CreatePersonTransactionEvent<T>() where T : PersonTransactionEvent, new()
        {
            this.AssertValidProjectToken();
            var personEvent = new T();
            this.OnCreatePersonEvent(personEvent);
            return personEvent;
        }

        protected virtual void OnCreatePersonEvent(PersonTransactionEvent personEvent)
        {
            personEvent.ProjectToken = this.ProjectToken;
        }



        public virtual void Send(PersonEventBase personEvent)
        {
            var eventSender = this.EventSender ?? new AsyncEventSender() {Log = this.Log};
            eventSender.Send(personEvent);
        }

        public virtual void Send(Event eVent)
        {
            var eventSender = this.EventSender ?? new AsyncEventSender() { Log = this.Log };
            eventSender.Send(eVent);
        }
    }
}
