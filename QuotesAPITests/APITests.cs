using NUnit.Framework;
using Moq;
using QuotesAPI.Data;
using QuotesAPI.Models;

namespace QuotesAPITests
{
    public class APITests
    {
        private MockRepository _mockRepository;
        private Mock<QuotesDbContext> _quotesMockDbContext;
        public Quote _quote;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _quote = new Quote(10, "Test title", "Test Author", "Test description");
        }


        [TearDown]
        public void TearDown()
        {
            _mockRepository.VerifyAll();
        }

        [Test]
        public void quoteCanBeAddedToDatabase()
        {
        }
    }
}