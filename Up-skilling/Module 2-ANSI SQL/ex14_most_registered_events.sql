-- Retrieve events with more than 5 distinct registrations
SELECT 
    evt.event_id, 
    evt.title, 
    COUNT(reg.registration_id) AS registration_count
FROM Events evt
INNER JOIN Registrations reg ON evt.event_id = reg.event_id
GROUP BY evt.event_id, evt.title
HAVING COUNT(reg.registration_id) > 5;
