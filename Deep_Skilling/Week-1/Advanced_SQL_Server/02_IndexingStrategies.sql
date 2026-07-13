-- ============================================================================
-- SQL Server Performance Tuning: Clustered, Non-Clustered, & Composite Indexes
-- Description: Hands-on demonstration of query optimization through indexes.
-- Scope: Deep Skilling Week 1 - Exercise 2
-- ============================================================================

-- ----------------------------------------------------------------------------
-- 1. Setup Database Schema
-- ----------------------------------------------------------------------------
-- We use a financial ledger domain for this exercise.

IF OBJECT_ID('LedgerEntries', 'U') IS NOT NULL DROP TABLE LedgerEntries;
IF OBJECT_ID('CorporateAccounts', 'U') IS NOT NULL DROP TABLE CorporateAccounts;

-- Accounts Table
CREATE TABLE CorporateAccounts (
    AccountID INT PRIMARY KEY,
    AccountHolder NVARCHAR(150),
    BranchRegion NVARCHAR(50),
    DateCreated DATE
);

-- LedgerEntries Table
-- Note: We initially define it without a clustered index on the primary key, 
-- or we can use a non-clustered primary key constraint to allow creating a clustered index later.
CREATE TABLE LedgerEntries (
    EntryID INT CONSTRAINT PK_LedgerEntries PRIMARY KEY NONCLUSTERED,
    AccountID INT,
    TransactionDate DATE,
    Amount DECIMAL(15, 2),
    TransactionType NVARCHAR(10),
    FOREIGN KEY (AccountID) REFERENCES CorporateAccounts(AccountID)
);


-- ----------------------------------------------------------------------------
-- 2. Insert Sample Data
-- ----------------------------------------------------------------------------

INSERT INTO CorporateAccounts VALUES
(10, 'Aegis Industries', 'East', '2025-01-10'),
(20, 'Vortex Media', 'West', '2025-02-15'),
(30, 'Nexus Logistics', 'South', '2025-03-20'),
(40, 'Apex Systems', 'North', '2025-04-25');

INSERT INTO LedgerEntries VALUES
(1001, 10, '2026-06-01', 50000.00, 'Credit'),
(1002, 20, '2026-06-02', -12000.00, 'Debit'),
(1003, 30, '2026-06-02', 3500.00, 'Credit'),
(1004, 10, '2026-06-03', -4500.00, 'Debit'),
(1005, 40, '2026-06-04', 125000.00, 'Credit'),
(1006, 20, '2026-06-05', -1500.00, 'Debit');


-- ----------------------------------------------------------------------------
-- 3. Exercise 1: Non-Clustered Index on a Specific Field
-- ----------------------------------------------------------------------------
-- Goal: Accelerate lookup for account holder names.

-- Before Index Creation (Simulates Table Scan / Index Scan on Primary Key)
SELECT * 
FROM CorporateAccounts 
WHERE AccountHolder = 'Vortex Media';

-- Create Non-Clustered Index
CREATE NONCLUSTERED INDEX IX_CorporateAccounts_AccountHolder
ON CorporateAccounts(AccountHolder);

-- After Index Creation (Simulates Index Seek)
SELECT * 
FROM CorporateAccounts 
WHERE AccountHolder = 'Vortex Media';


-- ----------------------------------------------------------------------------
-- 4. Exercise 2: Clustered Index (Reorganizing Data physically)
-- ----------------------------------------------------------------------------
-- Goal: Physical organization of ledger entries by date for fast range queries.
-- Note: A table can have only ONE clustered index because data pages can be sorted in only one way.

-- Before Clustered Index on TransactionDate
SELECT * 
FROM LedgerEntries 
WHERE TransactionDate = '2026-06-02';

-- Create Clustered Index on LedgerEntries
CREATE CLUSTERED INDEX IX_LedgerEntries_TransactionDate
ON LedgerEntries(TransactionDate);

-- After Clustered Index (Performs clustered index seek or scan on ordered date)
SELECT * 
FROM LedgerEntries 
WHERE TransactionDate = '2026-06-02';


-- ----------------------------------------------------------------------------
-- 5. Exercise 3: Composite Index (Multiple Fields)
-- ----------------------------------------------------------------------------
-- Goal: Speed up queries filtering on multiple criteria (AccountID and TransactionDate).

-- Before Composite Index (Performs index scans / lookups)
SELECT * 
FROM LedgerEntries 
WHERE AccountID = 20 
  AND TransactionDate = '2026-06-02';

-- Create Composite Non-Clustered Index
-- Order of columns matters: put the column with highest selectivity first or the one filtered on equality.
CREATE NONCLUSTERED INDEX IX_LedgerEntries_AccountID_TransactionDate
ON LedgerEntries(AccountID, TransactionDate);

-- After Index (Uses index seek on the composite index)
SELECT * 
FROM LedgerEntries 
WHERE AccountID = 20 
  AND TransactionDate = '2026-06-02';
