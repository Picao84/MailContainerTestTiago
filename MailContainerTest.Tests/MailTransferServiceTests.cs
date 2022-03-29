using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailContainerTest.Tests
{
    [TestFixture]
    public class MailTransferServiceTests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        [TestCase(AllowedMailType.StandardLetter, MailType.StandardLetter, 10, 5, MailContainerStatus.Operational)]
        [TestCase(AllowedMailType.LargeLetter, MailType.LargeLetter, 10, 5, MailContainerStatus.Operational)]
        [TestCase(AllowedMailType.SmallParcel, MailType.SmallParcel, 10, 5, MailContainerStatus.Operational)]
        public void MakeMailTransferPositiveTest(AllowedMailType mailContainerType, MailType mailType, int mailContainerCapacity, int numberOfMailItems, MailContainerStatus status = MailContainerStatus.Operational)
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            IMailContainerDataStore mailContainerDataStore;

            if (dataStoreType == "Backup")
            {
                mailContainerDataStore = new TestBackupMailContainerDataStore(mailContainerType, mailContainerCapacity, status);
            }
            else
            {
                mailContainerDataStore = new TestMailContainerDataStore(mailContainerType, mailContainerCapacity, status);
            }

            MailTransferService service = new MailTransferService(mailContainerDataStore);
            var result = service.MakeMailTransfer(new MakeMailTransferRequest() { MailType = mailType, NumberOfMailItems = numberOfMailItems });

            Assert.IsTrue(result.Success);
        }

        [Test]
        [TestCase(AllowedMailType.LargeLetter, MailType.StandardLetter, 10, 5, MailContainerStatus.Operational)]
        [TestCase(AllowedMailType.LargeLetter, MailType.LargeLetter, 10, 12, MailContainerStatus.Operational)]
        [TestCase(AllowedMailType.SmallParcel, MailType.SmallParcel, 10, 5, MailContainerStatus.OutOfService)]
        public void MakeMailTransferNegativeTest(AllowedMailType mailContainerType, MailType mailType, int mailContainerCapacity, int numberOfMailItems, MailContainerStatus status = MailContainerStatus.Operational)
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            IMailContainerDataStore mailContainerDataStore;

            if (dataStoreType == "Backup")
            {
                mailContainerDataStore = new TestBackupMailContainerDataStore(mailContainerType, mailContainerCapacity, status);
            }
            else
            {
                mailContainerDataStore = new TestMailContainerDataStore(mailContainerType, mailContainerCapacity, status);
            }

            MailTransferService service = new MailTransferService(mailContainerDataStore);
            var result = service.MakeMailTransfer(new MakeMailTransferRequest() { MailType = mailType, NumberOfMailItems = numberOfMailItems });

            Assert.IsFalse(result.Success);
        }
    }
}
