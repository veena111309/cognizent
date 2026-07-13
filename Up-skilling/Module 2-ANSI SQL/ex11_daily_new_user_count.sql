-- Count new user sign-ups per calendar day
SELECT 
    registration_date, 
    COUNT(*) AS new_users_count
FROM Users
GROUP BY registration_date
ORDER BY registration_date DESC;
