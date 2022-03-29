using MailContainerTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailContainerTest.Common
{
    public static class Constants
    {
        public static readonly Dictionary<MailType, AllowedMailType> MailTypesMapper = new Dictionary<MailType, AllowedMailType>
        {
            { MailType.StandardLetter, AllowedMailType.StandardLetter },
            { MailType.LargeLetter, AllowedMailType.LargeLetter },
            { MailType.SmallParcel, AllowedMailType.SmallParcel },
        };
    }
}
