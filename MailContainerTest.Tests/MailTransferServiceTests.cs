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
        IMailContainerDataStore mailContainerDataStore;

        [SetUp]
        public void Setup()
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            if (dataStoreType == "Backup")
            {
                 mailContainerDataStore = new BackupMailContainerDataStore();
            }
            else
            {
                mailContainerDataStore = new MailContainerDataStore();
            }
        }

        [Test]
        [TestCase(MailType.StandardLetter, 5, MailContainerStatus.Operational)]
        [TestCase(MailType.LargeLetter, 10, MailContainerStatus.Operational)]
        [TestCase(MailType.SmallParcel, 1, MailContainerStatus.Operational)]
        [TestCase(MailType.StandardLetter, 5, MailContainerStatus.OutOfService)]
        [TestCase(MailType.LargeLetter, 10, MailContainerStatus.OutOfService)]
        [TestCase(MailType.SmallParcel, 1, MailContainerStatus.OutOfService)]
        public void MakeMailTransferTest(MailType mailType, int numberOfMailItems, MailContainerStatus status = MailContainerStatus.Operational)
        {
            MailTransferService service = new MailTransferService(mailContainerDataStore);
            var result = service.MakeMailTransfer(new MakeMailTransferRequest() { MailType = mailType, NumberOfMailItems = numberOfMailItems });

            Assert.IsTrue(result.Success);
        }
    }
}
