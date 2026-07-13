-- Find total registrations grouped by event city location
SELECT 
    evt.city, 
    COUNT(reg.registration_id) AS total_registrations
FROM Registrations reg
INNER JOIN Events evt ON reg.event_id = evt.event_id
GROUP BY evt.city
ORDER BY total_registrations DESC;
