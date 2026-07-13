-- Count scheduled sessions for all upcoming events
SELECT 
    evt.event_id, 
    evt.title, 
    COUNT(ses.session_id) AS total_sessions
FROM Events evt
LEFT OUTER JOIN Sessions ses ON evt.event_id = ses.event_id
WHERE evt.status = 'upcoming'
GROUP BY evt.event_id, evt.title;
