using MailContainerTest.Data;
using MailContainerTest.Rules;
using MailContainerTest.Types;
using System;
using System.Configuration;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        // Container Data Store could be injected on service.
        private readonly IMailContainerDataStore _mailContainerDataStore;

        public MailTransferService(IMailContainerDataStore mailContainerDataStore)
        {
            this._mailContainerDataStore = mailContainerDataStore;
        }

        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var mailContainer = _mailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);

            var result = new MakeMailTransferResult();

            if (mailContainer == null)
            {
                return result;
            }

            if(mailContainer.AllowedMailType == 0)
            {
                mailContainer.AllowedMailType = AllowedMailType.StandardLetter;
            }

            // Replaced switch and if/else statements by a rule pattern
            result.Success = MailContainerValidation.ValidateContainer(mailContainer, request);

            if (result.Success)
            {
                mailContainer.Capacity -= request.NumberOfMailItems;

                _mailContainerDataStore.UpdateMailContainer(mailContainer);
            }

            return result;
        }
    }
}
