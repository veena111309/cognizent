-- Find events that do not have any sessions associated with them
SELECT 
    evt.event_id, 
    evt.title, 
    evt.city, 
    evt.start_date
FROM Events evt
LEFT OUTER JOIN Sessions ses ON evt.event_id = ses.event_id
WHERE ses.session_id IS NULL;
