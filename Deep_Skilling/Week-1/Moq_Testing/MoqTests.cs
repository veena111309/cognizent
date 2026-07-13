using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Moq_Testing
{
    [TestFixture]
    public class CustomerCommTests
    {
        private Mock<IMailSender> _mailSenderMock;
        private CustomerComm _customerComm;

        [SetUp]
        public void Init()
        {
            _mailSenderMock = new Mock<IMailSender>();
            _customerComm = new CustomerComm(_mailSenderMock.Object);
        }

        [Test]
        public void DispatchMail_WithValidInputs_InvokesSenderAndReturnsTrue()
        {
            // Arrange
            _mailSenderMock
                .Setup(x => x.SendMail("user@domain.com", "Test Message"))
                .Returns(true);

            // Act
            bool result = _customerComm.DispatchMail("user@domain.com", "Test Message");

            // Assert
            Assert.IsTrue(result);
            _mailSenderMock.Verify(x => x.SendMail("user@domain.com", "Test Message"), Times.Once);
        }

        [Test]
        public void DispatchMail_WithEmptyRecipient_ReturnsFalseWithoutCallingSender()
        {
            // Act
            bool result = _customerComm.DispatchMail("", "Test Message");

            // Assert
            Assert.IsFalse(result);
            _mailSenderMock.Verify(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }

    [TestFixture]
    public class DirectoryExplorerTests
    {
        private Mock<IDirectoryExplorer> _directoryExplorerMock;

        [SetUp]
        public void Setup()
        {
            _directoryExplorerMock = new Mock<IDirectoryExplorer>();
        }

        [Test]
        public void GetFiles_ReturnsMockedFilesSuccessfully()
        {
            // Arrange
            var mockedFiles = new List<string> { "doc1.pdf", "sheet2.xlsx", "image3.png" };
            _directoryExplorerMock
                .Setup(x => x.GetFiles(@"C:\SharedFolder"))
                .Returns(mockedFiles);

            // Act
            var result = _directoryExplorerMock.Object.GetFiles(@"C:\SharedFolder");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.Contains("sheet2.xlsx", (System.Collections.ICollection)result);
            _directoryExplorerMock.Verify(x => x.GetFiles(@"C:\SharedFolder"), Times.Once);
        }
    }
}
