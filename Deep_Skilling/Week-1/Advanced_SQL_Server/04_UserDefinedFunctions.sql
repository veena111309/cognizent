-- ============================================================================
-- SQL Server User Defined Functions (UDF): Scalar, Table-Valued, Nested
-- Description: Hands-on implementation of custom functions.
-- Scope: Deep Skilling Week 1 - Exercise 5
-- ============================================================================

-- ----------------------------------------------------------------------------
-- 1. Database Schema & Sample Data Setup
-- ----------------------------------------------------------------------------

IF OBJECT_ID('StaffMembers', 'U') IS NOT NULL DROP TABLE StaffMembers;
IF OBJECT_ID('StaffDepartments', 'U') IS NOT NULL DROP TABLE StaffDepartments;

CREATE TABLE StaffDepartments (
    DeptID INT PRIMARY KEY,
    DeptName VARCHAR(100) NOT NULL
);

CREATE TABLE StaffMembers (
    StaffID INT PRIMARY KEY,
    GivenName VARCHAR(50) NOT NULL,
    SurName VARCHAR(50) NOT NULL,
    DeptID INT,
    MonthlySalary DECIMAL(10,2) CHECK (MonthlySalary >= 0),
    DateHired DATE DEFAULT GETDATE(),
    FOREIGN KEY (DeptID) REFERENCES StaffDepartments(DeptID)
);

-- Seed Data
INSERT INTO StaffDepartments VALUES
(10, 'Human Resources'),
(20, 'Engineering'),
(30, 'Finance');

INSERT INTO StaffMembers VALUES
(101, 'Liam', 'OConnor', 20, 6800.00, '2022-03-15'),
(102, 'Sophia', 'Vargas', 20, 7500.00, '2021-08-20'),
(103, 'Aiden', 'Chen', 30, 6200.00, '2023-01-10');
GO


-- ----------------------------------------------------------------------------
-- 2. Scalar Function
-- ----------------------------------------------------------------------------
-- Goal: Calculate annual earnings based on monthly salary.

CREATE FUNCTION ufn_GetAnnualEarnings
(
    @MonthlySalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN COALESCE(@MonthlySalary, 0.00) * 12.00;
END;
GO

-- Execute Scalar Function
SELECT 
    StaffID,
    GivenName,
    MonthlySalary,
    dbo.ufn_GetAnnualEarnings(MonthlySalary) AS AnnualEarnings
FROM StaffMembers;
GO


-- ----------------------------------------------------------------------------
-- 3. Table-Valued Function (Inline)
-- ----------------------------------------------------------------------------
-- Goal: Retrieve all staff members in a specific department.

CREATE FUNCTION ufn_ListStaffByDept
(
    @DeptID INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT StaffID, GivenName, SurName, DeptID, MonthlySalary, DateHired
    FROM StaffMembers
    WHERE DeptID = @DeptID
);
GO

-- Execute Table-Valued Function
SELECT * 
FROM dbo.ufn_ListStaffByDept(20);
GO


-- ----------------------------------------------------------------------------
-- 4. User Defined Function (UDF) for Bonus Estimation
-- ----------------------------------------------------------------------------
-- Goal: Estimate bonus as 12% of monthly salary.

CREATE FUNCTION ufn_EstimateStaffBonus
(
    @MonthlySalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN COALESCE(@MonthlySalary, 0.00) * 0.12;
END;
GO

SELECT 
    StaffID, 
    GivenName, 
    MonthlySalary, 
    dbo.ufn_EstimateStaffBonus(MonthlySalary) AS EstimatedBonus
FROM StaffMembers;
GO


-- ----------------------------------------------------------------------------
-- 5. Modifying (Altering) Function
-- ----------------------------------------------------------------------------
-- Goal: Adjust the estimated bonus rate to 18%.

ALTER FUNCTION ufn_EstimateStaffBonus
(
    @MonthlySalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN COALESCE(@MonthlySalary, 0.00) * 0.18;
END;
GO

-- Verify Modified Function
SELECT 
    StaffID, 
    GivenName, 
    MonthlySalary, 
    dbo.ufn_EstimateStaffBonus(MonthlySalary) AS RevisedBonus
FROM StaffMembers;
GO


-- ----------------------------------------------------------------------------
-- 6. Deleting (Dropping) and Recreating Function
-- ----------------------------------------------------------------------------

IF OBJECT_ID('dbo.ufn_EstimateStaffBonus', 'FN') IS NOT NULL
    DROP FUNCTION dbo.ufn_EstimateStaffBonus;
GO

-- Recreate for nested demonstration
CREATE FUNCTION ufn_EstimateStaffBonus
(
    @MonthlySalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN COALESCE(@MonthlySalary, 0.00) * 0.18;
END;
GO


-- ----------------------------------------------------------------------------
-- 7. Executing Functions inside Filtering and Conditions
-- ----------------------------------------------------------------------------

SELECT 
    StaffID,
    GivenName,
    dbo.ufn_GetAnnualEarnings(MonthlySalary) AS AnnualEarnings
FROM StaffMembers
WHERE StaffID = 101;
GO


-- ----------------------------------------------------------------------------
-- 8. Executing Table-Valued Function for Finance (Dept 30)
-- ----------------------------------------------------------------------------

SELECT * 
FROM dbo.ufn_ListStaffByDept(30);
GO


-- ----------------------------------------------------------------------------
-- 9. Nested Function Execution
-- ----------------------------------------------------------------------------
-- Goal: Calculate total rewards = Annual Earnings + Estimated Bonus.

CREATE FUNCTION ufn_CalculateTotalRewards
(
    @MonthlySalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN dbo.ufn_GetAnnualEarnings(@MonthlySalary) 
         + dbo.ufn_EstimateStaffBonus(@MonthlySalary);
END;
GO

-- Verify Nested Function
SELECT 
    StaffID,
    GivenName,
    MonthlySalary,
    dbo.ufn_CalculateTotalRewards(MonthlySalary) AS TotalRewards
FROM StaffMembers;
GO


-- ----------------------------------------------------------------------------
-- 10. Modifying Nested Function
-- ----------------------------------------------------------------------------
-- Goal: Change total rewards to be Annual Earnings + (Monthly Salary * 25% performance bonus).

ALTER FUNCTION ufn_CalculateTotalRewards
(
    @MonthlySalary DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN dbo.ufn_GetAnnualEarnings(@MonthlySalary) 
         + (COALESCE(@MonthlySalary, 0.00) * 0.25);
END;
GO

-- Verify final results
SELECT 
    StaffID,
    GivenName,
    MonthlySalary,
    dbo.ufn_CalculateTotalRewards(MonthlySalary) AS FinalTotalRewards
FROM StaffMembers;
GO
