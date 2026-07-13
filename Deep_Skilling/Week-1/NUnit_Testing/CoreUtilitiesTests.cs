using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace NUnit_Testing
{
    // ============================================================================
    // Core Utilities NUnit Test Suite (Hands-on 1 to 9)
    // Goal: Validate all utility modules using NUnit assertions, parameters,
    //       exceptions, and Moq mocks where necessary.
    // ============================================================================

    // 1. Calculator Tests
    [TestFixture]
    public class SimpleCalculatorTests
    {
        private SimpleCalculator _calc;

        [SetUp]
        public void Init() => _calc = new SimpleCalculator();

        [TestCase(10, 5, 15)]
        [TestCase(-3, -2, -5)]
        public void Add_ReturnsCorrectSum(double a, double b, double expected)
        {
            Assert.AreEqual(expected, _calc.Add(a, b));
        }

        [Test]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => _calc.Divide(10, 0));
        }
    }

    // 2. Math Operations Tests
    [TestFixture]
    public class MathOperationsTests
    {
        private MathOperations _math;

        [SetUp]
        public void Setup() => _math = new MathOperations();

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(5, 120)]
        public void Factorial_CalculatesValueSuccessfully(int input, long expected)
        {
            Assert.AreEqual(expected, _math.Factorial(input));
        }

        [TestCase(2, ExpectedResult = true)]
        [TestCase(4, ExpectedResult = false)]
        [TestCase(17, ExpectedResult = true)]
        public bool IsPrime_ChecksNumbers(int number)
        {
            return _math.IsPrime(number);
        }
    }

    // 3. URL Parser Tests
    [TestFixture]
    public class UrlParserTests
    {
        private UrlParser _parser;

        [SetUp]
        public void Setup() => _parser = new UrlParser();

        [Test]
        public void GetHostName_WithValidUrl_ReturnsHostName()
        {
            string host = _parser.GetHostName("https://github.com/veena111309/cognizent");
            Assert.AreEqual("github.com", host);
        }

        [Test]
        public void GetHostName_WithMalformedUrl_ReturnsNull()
        {
            string host = _parser.GetHostName("not-a-valid-url");
            Assert.IsNull(host);
        }
    }

    // 4. Ledger Manager Tests
    [TestFixture]
    public class LedgerManagerTests
    {
        private LedgerManager _manager;

        [SetUp]
        public void Setup()
        {
            _manager = new LedgerManager();
            _manager.OpenAccount("ACC-901", 500.00m);
        }

        [Test]
        public void Deposit_IncreasesBalance()
        {
            _manager.Transact("ACC-901", 200.00m);
            Assert.AreEqual(700.00m, _manager.GetBalance("ACC-901"));
        }

        [Test]
        public void Overdraft_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => _manager.Transact("ACC-901", -600.00m));
        }
    }

    // 5. Staff Registry Tests (Collection Assertions)
    [TestFixture]
    public class StaffRegistryTests
    {
        private StaffRegistry _registry;

        [SetUp]
        public void Setup()
        {
            _registry = new StaffRegistry();
        }

        [Test]
        public void Register_AddsRecord_CollectsCorrectCount()
        {
            var emp1 = new StaffRecord { Id = 1, Name = "Alice", Role = "QA" };
            var emp2 = new StaffRecord { Id = 2, Name = "Bob", Role = "PM" };

            _registry.Register(emp1);
            _registry.Register(emp2);

            var list = _registry.GetAll();

            Assert.AreEqual(2, list.Count);
            CollectionAssert.Contains(list, emp1);
            CollectionAssert.Contains(list, emp2);
        }

        [Test]
        public void Terminate_RemovesEmployee()
        {
            var emp = new StaffRecord { Id = 10, Name = "Charlie", Role = "Developer" };
            _registry.Register(emp);

            bool wasRemoved = _registry.Terminate(10);
            Assert.IsTrue(wasRemoved);
            Assert.IsNull(_registry.Find(10));
        }
    }

    // 6. Season Determiner Tests
    [TestFixture]
    public class SeasonDeterminerTests
    {
        private SeasonDeterminer _determiner;

        [SetUp]
        public void Setup() => _determiner = new SeasonDeterminer();

        [TestCase(12, "Winter")]
        [TestCase(4, "Spring")]
        [TestCase(7, "Summer")]
        [TestCase(10, "Autumn")]
        public void GetSeason_ReturnsExpectedSeasonName(int month, string expectedSeason)
        {
            Assert.AreEqual(expectedSeason, _determiner.GetSeasonByMonth(month));
        }

        [Test]
        public void GetSeason_WithInvalidMonth_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _determiner.GetSeasonByMonth(13));
        }
    }

    // 7. Leap Year Checker Tests
    [TestFixture]
    public class LeapYearCheckerTests
    {
        private LeapYearChecker _checker;

        [SetUp]
        public void Setup() => _checker = new LeapYearChecker();

        [TestCase(2000, ExpectedResult = true)]
        [TestCase(2024, ExpectedResult = true)]
        [TestCase(1900, ExpectedResult = false)]
        [TestCase(2023, ExpectedResult = false)]
        public bool IsLeapYear_IdentifiesLeapYearsCorrectly(int year)
        {
            return _checker.IsLeapYear(year);
        }
    }

    // 8. User Authenticator Tests
    [TestFixture]
    public class UserAuthenticatorTests
    {
        private UserAuthenticator _auth;

        [SetUp]
        public void Setup()
        {
            _auth = new UserAuthenticator();
            _auth.RegisterUser("VeenasAccount", "SecureP@ss123");
        }

        [Test]
        public void Authenticate_WithCorrectCredentials_ReturnsTrue()
        {
            Assert.IsTrue(_auth.Authenticate("VeenasAccount", "SecureP@ss123"));
        }

        [Test]
        public void Authenticate_IsCaseInsensitiveForUsername()
        {
            Assert.IsTrue(_auth.Authenticate("veenasaccount", "SecureP@ss123"));
        }

        [Test]
        public void Authenticate_WithIncorrectPassword_ReturnsFalse()
        {
            Assert.IsFalse(_auth.Authenticate("VeenasAccount", "WrongPassword"));
        }
    }

    // 9. Currency Converter Tests (Mocking ExchangeRate Feed)
    [TestFixture]
    public class CurrencyConverterTests
    {
        private Mock<IExchangeRateFeed> _feedMock;
        private CurrencyConverter _converter;

        [SetUp]
        public void Setup()
        {
            _feedMock = new Mock<IExchangeRateFeed>();
            _converter = new CurrencyConverter(_feedMock.Object);
        }

        [Test]
        public void ConvertUsdToEur_CalculatesCorrectValueUsingMockRate()
        {
            // Arrange
            _feedMock.Setup(x => x.GetUsdToEurRate()).Returns(0.92);

            // Act
            double result = _converter.ConvertUsdToEur(100.00);

            // Assert
            Assert.AreEqual(92.00, result);
            _feedMock.Verify(x => x.GetUsdToEurRate(), Times.Once);
        }
    }
}
