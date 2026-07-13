-- Search for duplicate registrations (user registered for the same event multiple times)
SELECT 
    user_id, 
    event_id, 
    COUNT(*) AS duplicates_count
FROM Registrations
GROUP BY user_id, event_id
HAVING COUNT(*) > 1;
