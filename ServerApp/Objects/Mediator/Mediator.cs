using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Objects.Mediator
{
    abstract class AbstractMediator

    {
        public abstract void Register(Collegue participant);
        public abstract void Send(
          string from, string to, string message);
    }

    class Mediator : AbstractMediator

    {
        private Dictionary<string, Collegue> _participants =
          new Dictionary<string, Collegue>();

        public override void Register(Collegue participant)
        {
            if (!_participants.ContainsValue(participant))
            {
                _participants[participant.Name] = participant;
            }

            participant.Chatroom = this;
        }

        public override void Send(
          string from, string to, string message)
        {
            Collegue participant = _participants[to];

            if (participant != null)
            {
                participant.Receive(from, message);
            }
        }
    }
}
