-- Summarize the number of events managed by each user acting as organizer
SELECT 
    usr.user_id, 
    usr.full_name, 
    evt.status, 
    COUNT(evt.event_id) AS total_events
FROM Users usr
INNER JOIN Events evt ON usr.user_id = evt.organizer_id
GROUP BY usr.user_id, usr.full_name, evt.status;
