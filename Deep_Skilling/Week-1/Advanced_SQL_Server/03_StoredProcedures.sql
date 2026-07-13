-- ============================================================================
-- SQL Server Stored Procedures: Parameterized, Transactions, Dynamic SQL
-- Description: Hands-on implementation of stored procedures for data management.
-- Scope: Deep Skilling Week 1 - Exercise 4
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
(30, 'Finance'),
(40, 'Sales');

INSERT INTO StaffMembers VALUES
(101, 'Liam', 'OConnor', 20, 6800.00, '2022-03-15'),
(102, 'Sophia', 'Vargas', 20, 7500.00, '2021-08-20'),
(103, 'Aiden', 'Chen', 30, 6200.00, '2023-01-10'),
(104, 'Olivia', 'Taylor', 10, 4800.00, '2024-05-02'),
(105, 'Emma', 'Brown', 40, 5300.00, '2023-11-12');


-- ----------------------------------------------------------------------------
-- 2. Basic Retrieval Procedure
-- ----------------------------------------------------------------------------

CREATE PROCEDURE usp_GetStaffByDept
    @DeptID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT StaffID, GivenName, SurName, DeptID, MonthlySalary, DateHired
    FROM StaffMembers
    WHERE DeptID = @DeptID;
END;
GO

-- Execute Retrieval
EXEC usp_GetStaffByDept @DeptID = 20;
GO


-- ----------------------------------------------------------------------------
-- 3. Insertion Procedure
-- ----------------------------------------------------------------------------

CREATE PROCEDURE usp_RegisterStaff
    @StaffID INT,
    @GivenName VARCHAR(50),
    @SurName VARCHAR(50),
    @DeptID INT,
    @MonthlySalary DECIMAL(10,2),
    @DateHired DATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO StaffMembers (StaffID, GivenName, SurName, DeptID, MonthlySalary, DateHired)
    VALUES (@StaffID, @GivenName, @SurName, @DeptID, @MonthlySalary, @DateHired);
END;
GO

-- Execute Insertion
EXEC usp_RegisterStaff 106, 'Mateo', 'Silva', 20, 6100.00, '2026-07-01';
GO


-- ----------------------------------------------------------------------------
-- 4. Modifying (Altering) Stored Procedure
-- ----------------------------------------------------------------------------
-- Goal: Alter retrieval to include department names using a JOIN.

ALTER PROCEDURE usp_GetStaffByDept
    @DeptID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        s.StaffID, 
        s.GivenName, 
        s.SurName, 
        d.DeptName, 
        s.MonthlySalary, 
        s.DateHired
    FROM StaffMembers s
    INNER JOIN StaffDepartments d ON s.DeptID = d.DeptID
    WHERE s.DeptID = @DeptID;
END;
GO

-- Verify Altered Procedure
EXEC usp_GetStaffByDept @DeptID = 20;
GO


-- ----------------------------------------------------------------------------
-- 5. Deleting (Dropping) Stored Procedure
-- ----------------------------------------------------------------------------

IF OBJECT_ID('usp_RegisterStaff', 'P') IS NOT NULL
    DROP PROCEDURE usp_RegisterStaff;
GO


-- ----------------------------------------------------------------------------
-- 6. Aggregate Count Stored Procedure
-- ----------------------------------------------------------------------------

CREATE PROCEDURE usp_GetStaffCountByDept
    @DeptID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(*) AS StaffCount
    FROM StaffMembers
    WHERE DeptID = @DeptID;
END;
GO

EXEC usp_GetStaffCountByDept 20;
GO


-- ----------------------------------------------------------------------------
-- 7. Output Parameters
-- ----------------------------------------------------------------------------
-- Goal: Returns the total monthly salary budget for a given department.

CREATE PROCEDURE usp_GetDeptSalaryBudget
    @DeptID INT,
    @TotalSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT @TotalSalary = COALESCE(SUM(MonthlySalary), 0.00)
    FROM StaffMembers
    WHERE DeptID = @DeptID;
END;
GO

-- Execute with Output Variable
DECLARE @Budget DECIMAL(10,2);
EXEC usp_GetDeptSalaryBudget @DeptID = 20, @TotalSalary = @Budget OUTPUT;
SELECT @Budget AS DepartmentBudget;
GO


-- ----------------------------------------------------------------------------
-- 8. Simple Update Stored Procedure
-- ----------------------------------------------------------------------------

CREATE PROCEDURE usp_AdjustStaffSalary
    @StaffID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE StaffMembers
    SET MonthlySalary = @NewSalary
    WHERE StaffID = @StaffID;
END;
GO

EXEC usp_AdjustStaffSalary 101, 7200.00;
GO


-- ----------------------------------------------------------------------------
-- 9. Bulk Modification Stored Procedure
-- ----------------------------------------------------------------------------
-- Goal: Give a fixed salary increase to all staff members in a department.

CREATE PROCEDURE usp_ApplyDeptBonus
    @DeptID INT,
    @SalaryIncrease DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE StaffMembers
    SET MonthlySalary = MonthlySalary + @SalaryIncrease
    WHERE DeptID = @DeptID;
END;
GO

EXEC usp_ApplyDeptBonus 20, 350.00;
GO


-- ----------------------------------------------------------------------------
-- 10. Stored Procedure with Transaction Block
-- ----------------------------------------------------------------------------
-- Goal: Safely update salary within a transaction with rollback protection.

CREATE PROCEDURE usp_UpdateSalarySecure
    @StaffID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Business Rule: Salary cannot exceed 15000.00
        IF @NewSalary > 15000.00
        BEGIN
            THROW 50001, 'Proposed salary exceeds corporate caps.', 1;
        END

        UPDATE StaffMembers
        SET MonthlySalary = @NewSalary
        WHERE StaffID = @StaffID;

        COMMIT TRANSACTION;
        PRINT 'Transaction committed successfully.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        PRINT 'Transaction rolled back.';
        THROW;
    END CATCH
END;
GO

-- Test Success
EXEC usp_UpdateSalarySecure 103, 6500.00;
-- Test Fail (Triggering exception and rollback)
-- EXEC usp_UpdateSalarySecure 103, 16000.00;
GO


-- ----------------------------------------------------------------------------
-- 11. Dynamic SQL Stored Procedure
-- ----------------------------------------------------------------------------
-- Goal: Query staff members dynamically by filtering columns safely.
-- Note: Always sanitize inputs to prevent SQL Injection.

CREATE PROCEDURE usp_FindStaffDynamic
    @FieldName NVARCHAR(50),
    @SearchValue NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Safe Whitelisting validation of target columns
    IF @FieldName NOT IN ('GivenName', 'SurName', 'DeptID')
    BEGIN
        RAISERROR('Unauthorized search column specified.', 16, 1);
        RETURN;
    END

    DECLARE @DynamicSQL NVARCHAR(MAX);
    DECLARE @ParameterDef NVARCHAR(500) = N'@FilterVal NVARCHAR(100)';

    -- Constructing parameterised query to block SQL injection
    SET @DynamicSQL = N'SELECT * FROM StaffMembers WHERE ' + QUOTENAME(@FieldName) + N' = @FilterVal';

    EXEC sp_executesql @DynamicSQL, @ParameterDef, @FilterVal = @SearchValue;
END;
GO

EXEC usp_FindStaffDynamic 'GivenName', 'Liam';
GO


-- ----------------------------------------------------------------------------
-- 12. Robust Error Handling Procedure
-- ----------------------------------------------------------------------------

CREATE PROCEDURE usp_UpdateSalaryWithLogging
    @StaffID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        -- Check if employee exists
        IF NOT EXISTS (SELECT 1 FROM StaffMembers WHERE StaffID = @StaffID)
        BEGIN
            RAISERROR('Staff Member ID not found.', 16, 1);
            RETURN;
        END

        UPDATE StaffMembers
        SET MonthlySalary = @NewSalary
        WHERE StaffID = @StaffID;

        PRINT 'Salary adjusted successfully.';
    END TRY
    BEGIN CATCH
        PRINT 'An error occurred during execution:';
        PRINT 'Error Code: ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
        PRINT 'Error Severity: ' + CAST(ERROR_SEVERITY() AS VARCHAR(10));
        PRINT 'Error Msg: ' + ERROR_MESSAGE();
    END CATCH
END;
GO

EXEC usp_UpdateSalaryWithLogging 999, 5000.00; -- Testing non-existing employee handling
GO
