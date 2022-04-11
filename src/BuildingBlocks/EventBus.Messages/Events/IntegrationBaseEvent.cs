using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {

        public IntegrationBaseEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }

        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;

        }

        public Guid Id { get; init; }
        public DateTime CreationDate { get; init; }




    }
}
