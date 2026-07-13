using System;
using System.Collections.Generic;

namespace NUnit_Testing
{
    // ============================================================================
    // Core Utilities for Hands-on 1-9
    // Goal: Implements all 9 testable utility requirements in a clean domain structure.
    // ============================================================================

    // 1. Calculator
    public class SimpleCalculator
    {
        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;
        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Denominator cannot be zero.");
            }
            return a / b;
        }
    }

    // 2. Math Library
    public class MathOperations
    {
        public long Factorial(int n)
        {
            if (n < 0) throw new ArgumentException("Negative numbers not allowed.");
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        public bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));
            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }

    // 3. URL Host Name Parser
    public class UrlParser
    {
        public string GetHostName(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be empty.");
            }

            try
            {
                var uri = new Uri(url);
                return uri.Host;
            }
            catch (UriFormatException)
            {
                return null;
            }
        }
    }

    // 4. Accounts Manager
    public class LedgerAccount
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }

    public class LedgerManager
    {
        private readonly Dictionary<string, LedgerAccount> _accounts = new Dictionary<string, LedgerAccount>();

        public void OpenAccount(string accountNum, decimal initialDeposit)
        {
            if (initialDeposit < 0) throw new ArgumentException("Initial deposit cannot be negative.");
            _accounts[accountNum] = new LedgerAccount { AccountNumber = accountNum, Balance = initialDeposit };
        }

        public decimal GetBalance(string accountNum)
        {
            if (!_accounts.ContainsKey(accountNum)) throw new KeyNotFoundException("Account does not exist.");
            return _accounts[accountNum].Balance;
        }

        public void Transact(string accountNum, decimal amount)
        {
            if (!_accounts.ContainsKey(accountNum)) throw new KeyNotFoundException("Account does not exist.");
            var acc = _accounts[accountNum];
            if (acc.Balance + amount < 0) throw new InvalidOperationException("Insufficient funds.");
            acc.Balance += amount;
        }
    }

    // 5. Collections (Employee Registry)
    public class StaffRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class StaffRegistry
    {
        private readonly List<StaffRecord> _records = new List<StaffRecord>();

        public void Register(StaffRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));
            _records.Add(record);
        }

        public bool Terminate(int id)
        {
            var item = _records.Find(x => x.Id == id);
            if (item == null) return false;
            return _records.Remove(item);
        }

        public StaffRecord Find(int id)
        {
            return _records.Find(x => x.Id == id);
        }

        public IReadOnlyCollection<StaffRecord> GetAll() => _records.AsReadOnly();
    }

    // 6. Season Manager
    public class SeasonDeterminer
    {
        public string GetSeasonByMonth(int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
            }

            return month switch
            {
                12 or 1 or 2 => "Winter",
                3 or 4 or 5 => "Spring",
                6 or 7 or 8 => "Summer",
                9 or 10 or 11 => "Autumn",
                _ => "Unknown"
            };
        }
    }

    // 7. Leap Year Calculator
    public class LeapYearChecker
    {
        public bool IsLeapYear(int year)
        {
            if (year < 1) throw new ArgumentOutOfRangeException(nameof(year), "Year must be greater than zero.");
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }
    }

    // 8. User Manager
    public class UserRecord
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }

    public class UserAuthenticator
    {
        private readonly Dictionary<string, string> _credentials = new Dictionary<string, string>();

        public void RegisterUser(string username, string rawPassword)
        {
            _credentials[username.ToLower()] = rawPassword;
        }

        public bool Authenticate(string username, string rawPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(rawPassword)) return false;
            string key = username.ToLower();
            return _credentials.ContainsKey(key) && _credentials[key] == rawPassword;
        }
    }

    // 9. Converter (with Exchange Rate Feed)
    public interface IExchangeRateFeed
    {
        double GetUsdToEurRate();
    }

    public class CurrencyConverter
    {
        private readonly IExchangeRateFeed _feed;

        public CurrencyConverter(IExchangeRateFeed feed)
        {
            _feed = feed;
        }

        public double ConvertUsdToEur(double usdAmount)
        {
            if (usdAmount < 0) throw new ArgumentException("Amount cannot be negative.");
            double rate = _feed.GetUsdToEurRate();
            return usdAmount * rate;
        }
    }
}
