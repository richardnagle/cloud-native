using System;
using System.Collections.Generic;

namespace cloud.native.contracts
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CommunicationDto> Communications { get; set; } = new CommunicationDto[0];
    }
}
