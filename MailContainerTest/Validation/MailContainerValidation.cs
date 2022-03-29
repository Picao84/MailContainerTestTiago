using MailContainerTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailContainerTest.Common;

namespace MailContainerTest.Rules
{
    /// <summary>
    /// Rule pattern allows quick adding and removal of individual rules to use and could also be changed on the fly via an API call for example;
    /// </summary>
    public static class MailContainerValidation
    {
        private static Dictionary<AllowedMailType, List<Func<MailContainer,MakeMailTransferRequest,bool>>> ContainerRules = new Dictionary<AllowedMailType, List<Func<MailContainer,MakeMailTransferRequest,bool>>>
        {
            { AllowedMailType.StandardLetter, new List<Func<MailContainer,MakeMailTransferRequest, bool>> { TypeRule } },
            { AllowedMailType.LargeLetter, new List<Func<MailContainer,MakeMailTransferRequest, bool>> { TypeRule, CapacityRule } },
            { AllowedMailType.SmallParcel, new List<Func<MailContainer,MakeMailTransferRequest, bool>> { TypeRule, CapacityRule, IsOperationalRule } },
        };
        private static bool TypeRule(MailContainer container, MakeMailTransferRequest request)
        {
            return Constants.MailTypesMapper[request.MailType] == container.AllowedMailType;
        }

        private static bool CapacityRule(MailContainer container, MakeMailTransferRequest request)
        {
            return container.Capacity > request.NumberOfMailItems;
        }

        private static bool IsOperationalRule(MailContainer container, MakeMailTransferRequest request)
        {
            return container.Status == MailContainerStatus.Operational;
        }

        public static bool ValidateContainer(MailContainer container, MakeMailTransferRequest request)
        {
            foreach (var rule in ContainerRules[container.AllowedMailType])
            {
                if (!rule.Invoke(container, request))
                {
                    return false;
                }
            }
            return true;
        }

        public static void SetRule(AllowedMailType type, List<Func<MailContainer, MakeMailTransferRequest, bool>> rule)
        {
            if (ContainerRules.ContainsKey(type))
            {
                ContainerRules[type] = rule;
            }
            else
            {
                ContainerRules.Add(type, rule);
            }
        }
    }
}
