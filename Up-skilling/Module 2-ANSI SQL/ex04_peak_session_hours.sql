-- Count the number of sessions scheduled for each event
SELECT 
    evt.event_id, 
    evt.title, 
    COUNT(ses.session_id) AS session_count
FROM Events evt
LEFT OUTER JOIN Sessions ses ON evt.event_id = ses.event_id
GROUP BY evt.event_id, evt.title;
