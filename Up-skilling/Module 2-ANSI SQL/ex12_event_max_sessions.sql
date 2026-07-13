-- Find events that exceed a threshold of 3 scheduled sessions
SELECT 
    evt.event_id, 
    evt.title, 
    COUNT(ses.session_id) AS session_count
FROM Events evt
INNER JOIN Sessions ses ON evt.event_id = ses.event_id
GROUP BY evt.event_id, evt.title
HAVING COUNT(ses.session_id) > 3;
