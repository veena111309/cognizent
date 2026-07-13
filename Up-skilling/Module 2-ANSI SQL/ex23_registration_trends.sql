-- Registration counts grouped by calendar date
SELECT 
    CAST(registration_date AS DATE) AS date_of_reg, 
    COUNT(*) AS total_registrations
FROM Registrations
GROUP BY CAST(registration_date AS DATE)
ORDER BY date_of_reg DESC;
