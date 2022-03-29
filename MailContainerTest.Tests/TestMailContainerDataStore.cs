using MailContainerTest.Data;
using MailContainerTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailContainerTest.Tests
{
    public class TestMailContainerDataStore : IMailContainerDataStore
    {
        public AllowedMailType _allowedMailType { get; set; } = AllowedMailType.StandardLetter;
        public int _capacity { get; set; }
        public MailContainerStatus _status { get; set; } = MailContainerStatus.Operational;
        public TestMailContainerDataStore(AllowedMailType type, int capacity, MailContainerStatus status)
        {
            _allowedMailType = type;
            _capacity = capacity;
            _status = status; 
        }

        public MailContainer GetMailContainer(string mailContainerNumber)
        {
            return new MailContainer() { AllowedMailType = _allowedMailType, Capacity = _capacity, Status = _status };
        }

        public void UpdateMailContainer(MailContainer mailContainer)
        {
            
        }
    }
}
