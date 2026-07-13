-- Retrieve users and their registered upcoming events in the same city
SELECT 
    usr.user_id, 
    usr.full_name, 
    evt.event_id, 
    evt.title, 
    evt.city, 
    evt.start_date
FROM Users usr
INNER JOIN Registrations reg ON usr.user_id = reg.user_id
INNER JOIN Events evt ON reg.event_id = evt.event_id
WHERE evt.status = 'upcoming' 
  AND usr.city = evt.city
ORDER BY evt.start_date ASC;
