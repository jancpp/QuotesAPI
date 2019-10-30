using NUnit.Framework;
using Moq;
using QuotesAPI.Data;
using QuotesAPI.Models;
using System.Collections.Generic;
using QuotesAPI.Controllers;

namespace QuotesAPITests
{
    public class QuotesControllerTest
    {
        private QuotesController _quotesController;
        private Mock<QuotesDbContext> _mockDbContext;
        private Quote _quote;

        [SetUp]
        public void Setup()
        {
            _mockDbContext = new Mock<QuotesDbContext>(MockBehavior.Strict);
            _quotesController = new QuotesController(_mockDbContext.Object);

            _quote = new Quote(10, "Test title", "Test Author", "Test description");
        }


        [TearDown]
        public void TearDown()
        {
            _mockDbContext.VerifyAll();
        }

        [Test]
        public void quoteCanBeAddedToDatabase()
        {
            //_mockDbContext.Setup(c => c.Add(_quote)).Returns();
        }
    }
}