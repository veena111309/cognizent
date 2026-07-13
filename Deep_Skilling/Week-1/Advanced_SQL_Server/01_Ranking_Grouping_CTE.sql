-- ============================================================================
-- SQL Server Advanced Concepts: Window Functions, Grouping Sets, CTEs, & MERGE
-- Description: Hands-on implementation of enterprise query techniques.
-- Scope: Deep Skilling Week 1 - Exercise 1
-- ============================================================================

-- ----------------------------------------------------------------------------
-- 1. Setup Sample Schema and Data
-- ----------------------------------------------------------------------------
-- We use a corporate sales environment to keep calculations realistic.

IF OBJECT_ID('SalesInvoiceDetails', 'U') IS NOT NULL DROP TABLE SalesInvoiceDetails;
IF OBJECT_ID('SalesInvoices', 'U') IS NOT NULL DROP TABLE SalesInvoices;
IF OBJECT_ID('StoreProducts', 'U') IS NOT NULL DROP TABLE StoreProducts;
IF OBJECT_ID('StoreCustomers', 'U') IS NOT NULL DROP TABLE StoreCustomers;

CREATE TABLE StoreCustomers (
    CustomerID INT PRIMARY KEY,
    CustomerName NVARCHAR(100),
    Territory NVARCHAR(50)
);

CREATE TABLE StoreProducts (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(100),
    ProductCategory NVARCHAR(50),
    UnitPrice DECIMAL(10, 2)
);

CREATE TABLE SalesInvoices (
    InvoiceID INT PRIMARY KEY,
    CustomerID INT,
    InvoiceDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES StoreCustomers(CustomerID)
);

CREATE TABLE SalesInvoiceDetails (
    DetailID INT PRIMARY KEY,
    InvoiceID INT,
    ProductID INT,
    OrderQty INT,
    LineTotal DECIMAL(10, 2),
    FOREIGN KEY (InvoiceID) REFERENCES SalesInvoices(InvoiceID),
    FOREIGN KEY (ProductID) REFERENCES StoreProducts(ProductID)
);

-- Insert Seed Data
INSERT INTO StoreCustomers (CustomerID, CustomerName, Territory) VALUES
(101, 'Globex Corp', 'North America'),
(102, 'Initech LLC', 'North America'),
(103, 'Umbrella Corp', 'Europe'),
(104, 'Vehement Capital', 'Asia-Pacific');

INSERT INTO StoreProducts (ProductID, ProductName, ProductCategory, UnitPrice) VALUES
(201, 'Ultrabook Pro 15', 'Hardware', 1200.00),
(202, 'Dev Workspace Screen 34', 'Hardware', 450.00),
(203, 'Mechanical Keyboard K8', 'Accessories', 120.00),
(204, 'Noise Cancelling Headset H2', 'Accessories', 180.00),
(205, 'Cloud IDE Subscription', 'Software', 300.00);

INSERT INTO SalesInvoices (InvoiceID, CustomerID, InvoiceDate) VALUES
(1001, 101, '2026-01-10'),
(1002, 102, '2026-02-15'),
(1003, 103, '2026-03-20'),
(1004, 104, '2026-04-25');

INSERT INTO SalesInvoiceDetails (DetailID, InvoiceID, ProductID, OrderQty, LineTotal) VALUES
(1, 1001, 201, 2, 2400.00),
(2, 1001, 203, 5, 600.00),
(3, 1002, 202, 3, 1350.00),
(4, 1003, 205, 10, 3000.00),
(5, 1004, 204, 4, 720.00);


-- ----------------------------------------------------------------------------
-- 2. Ranking and Window Functions
-- ----------------------------------------------------------------------------
-- Goal: Rank products within their categories based on unit price.

SELECT 
    ProductID,
    ProductName,
    ProductCategory,
    UnitPrice,
    ROW_NUMBER() OVER (
        PARTITION BY ProductCategory 
        ORDER BY UnitPrice DESC
    ) AS RowNum,
    RANK() OVER (
        PARTITION BY ProductCategory 
        ORDER BY UnitPrice DESC
    ) AS PriceRank,
    DENSE_RANK() OVER (
        PARTITION BY ProductCategory 
        ORDER BY UnitPrice DESC
    ) AS DensePriceRank
FROM StoreProducts;

-- Common Table Expression (CTE) to fetch the top 2 highest-priced products per category
WITH ProductRankingCTE AS (
    SELECT 
        ProductID,
        ProductName,
        ProductCategory,
        UnitPrice,
        DENSE_RANK() OVER (
            PARTITION BY ProductCategory 
            ORDER BY UnitPrice DESC
        ) AS CategoryRank
    FROM StoreProducts
)
SELECT * 
FROM ProductRankingCTE
WHERE CategoryRank <= 2;


-- ----------------------------------------------------------------------------
-- 3. Advanced Grouping (GROUPING SETS, ROLLUP, CUBE)
-- ----------------------------------------------------------------------------
-- Goal: Aggregate sales quantities by territory and category using advanced sets.

-- GROUPING SETS: Aggregates at specific defined levels (territory+category, territory only, category only)
SELECT 
    c.Territory,
    p.ProductCategory,
    SUM(d.OrderQty) AS TotalQuantityOrdered
FROM SalesInvoiceDetails d
JOIN StoreProducts p ON d.ProductID = p.ProductID
JOIN SalesInvoices i ON d.InvoiceID = i.InvoiceID
JOIN StoreCustomers c ON i.CustomerID = c.CustomerID
GROUP BY GROUPING SETS (
    (c.Territory, p.ProductCategory),
    (c.Territory),
    (p.ProductCategory),
    ()
);

-- ROLLUP: Creates hierarchical aggregations from left to right (Territory > ProductCategory > Grand Total)
SELECT 
    c.Territory,
    p.ProductCategory,
    SUM(d.LineTotal) AS RevenueTotal
FROM SalesInvoiceDetails d
JOIN StoreProducts p ON d.ProductID = p.ProductID
JOIN SalesInvoices i ON d.InvoiceID = i.InvoiceID
JOIN StoreCustomers c ON i.CustomerID = c.CustomerID
GROUP BY ROLLUP (
    c.Territory,
    p.ProductCategory
);

-- CUBE: Generates all possible permutations of aggregation columns
SELECT 
    c.Territory,
    p.ProductCategory,
    SUM(d.LineTotal) AS RevenueTotal
FROM SalesInvoiceDetails d
JOIN StoreProducts p ON d.ProductID = p.ProductID
JOIN SalesInvoices i ON d.InvoiceID = i.InvoiceID
JOIN StoreCustomers c ON i.CustomerID = c.CustomerID
GROUP BY CUBE (
    c.Territory,
    p.ProductCategory
);


-- ----------------------------------------------------------------------------
-- 4. Recursive Common Table Expression (CTE)
-- ----------------------------------------------------------------------------
-- Goal: Model a corporate reporting structure (hierarchy) and trace paths.

-- Create an Ad-hoc Org Chart Table for Demonstration
IF OBJECT_ID('tempdb..#CorpHierarchy') IS NOT NULL DROP TABLE #CorpHierarchy;
CREATE TABLE #CorpHierarchy (
    EmployeeID INT PRIMARY KEY,
    EmployeeName NVARCHAR(100),
    ManagerID INT NULL
);

INSERT INTO #CorpHierarchy VALUES
(1, 'CEO Elizabeth', NULL),
(2, 'VP Operations Mark', 1),
(3, 'VP Sales Julian', 1),
(4, 'Operations Manager Sarah', 2),
(5, 'Sales Lead Arthur', 3),
(6, 'Junior Sales Associate Diana', 5);

-- Recursive CTE to print organizational tree
WITH OrgHierarchyCTE AS (
    -- Anchor member
    SELECT 
        EmployeeID,
        EmployeeName,
        ManagerID,
        1 AS OrgLevel,
        CAST(EmployeeName AS NVARCHAR(MAX)) AS ReportingLine
    FROM #CorpHierarchy
    WHERE ManagerID IS NULL
    
    UNION ALL
    
    -- Recursive member
    SELECT 
        ch.EmployeeID,
        ch.EmployeeName,
        ch.ManagerID,
        cte.OrgLevel + 1 AS OrgLevel,
        CAST(cte.ReportingLine + ' -> ' + ch.EmployeeName AS NVARCHAR(MAX))
    FROM #CorpHierarchy ch
    INNER JOIN OrgHierarchyCTE cte ON ch.ManagerID = cte.EmployeeID
)
SELECT 
    REPLICATE('   ', OrgLevel - 1) + EmployeeName AS VisualHierarchy,
    OrgLevel,
    ReportingLine
FROM OrgHierarchyCTE
ORDER BY OrgLevel, EmployeeID;


-- ----------------------------------------------------------------------------
-- 5. Staging Table and MERGE (Upsert) Operation
-- ----------------------------------------------------------------------------
-- Goal: Merge a staging table into the main StoreProducts table.

-- Create temporary Staging Table
IF OBJECT_ID('tempdb..#StagingProducts') IS NOT NULL DROP TABLE #StagingProducts;
CREATE TABLE #StagingProducts (
    ProductID INT,
    ProductName NVARCHAR(100),
    ProductCategory NVARCHAR(50),
    UnitPrice DECIMAL(10, 2)
);

-- Seed staging table with 1 update (Ultrabook Pro 15 price drop) and 1 insert (New Software license)
INSERT INTO #StagingProducts VALUES
(201, 'Ultrabook Pro 15', 'Hardware', 1100.00), -- Updated price
(206, 'Virtual Whiteboard Pro', 'Software', 99.00); -- New product

-- Execute MERGE Statement
MERGE StoreProducts AS Target
USING #StagingProducts AS Source
ON Target.ProductID = Source.ProductID
WHEN MATCHED THEN
    UPDATE SET 
        Target.UnitPrice = Source.UnitPrice,
        Target.ProductName = Source.ProductName
WHEN NOT MATCHED THEN
    INSERT (ProductID, ProductName, ProductCategory, UnitPrice)
    VALUES (Source.ProductID, Source.ProductName, Source.ProductCategory, Source.UnitPrice);

-- Verify results
SELECT * FROM StoreProducts;


-- ----------------------------------------------------------------------------
-- 6. PIVOT and UNPIVOT
-- ----------------------------------------------------------------------------
-- Goal: Pivot sales data by product and month, then unpivot back.

-- Let's extract total sales quantities per product for Q1 (Months 1, 2, and 3)
WITH SalesSummaryCTE AS (
    SELECT 
        p.ProductName,
        MONTH(i.InvoiceDate) AS SaleMonth,
        d.OrderQty
    FROM SalesInvoiceDetails d
    JOIN StoreProducts p ON d.ProductID = p.ProductID
    JOIN SalesInvoices i ON d.InvoiceID = i.InvoiceID
)
SELECT ProductName, [1] AS JanSales, [2] AS FebSales, [3] AS MarSales
INTO #PivotedSales
FROM SalesSummaryCTE
PIVOT (
    SUM(OrderQty)
    FOR SaleMonth IN ([1], [2], [3])
) AS PivotTable;

-- View Pivoted Data
SELECT * FROM #PivotedSales;

-- Unpivot the pivoted data back into tabular format
SELECT 
    ProductName,
    SaleMonthName,
    TotalQty
FROM #PivotedSales
UNPIVOT (
    TotalQty FOR SaleMonthName IN (JanSales, FebSales, MarSales)
) AS UnpivotedTable;

-- Cleanup temporary tables
DROP TABLE #PivotedSales;
