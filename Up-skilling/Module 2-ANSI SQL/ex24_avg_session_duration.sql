-- Calculate average duration of sessions per event in minutes
SELECT 
    evt.event_id, 
    evt.title, 
    AVG(DATEDIFF(minute, ses.start_time, ses.end_time)) AS avg_session_duration_minutes
FROM Events evt
INNER JOIN Sessions ses ON evt.event_id = ses.event_id
GROUP BY evt.event_id, evt.title;
