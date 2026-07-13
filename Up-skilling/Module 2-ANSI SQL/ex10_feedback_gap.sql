-- Find events that have registrations but have received no feedback
SELECT 
    evt.event_id, 
    evt.title
FROM Events evt
INNER JOIN Registrations reg ON evt.event_id = reg.event_id
LEFT OUTER JOIN Feedback fb ON evt.event_id = fb.event_id
WHERE fb.feedback_id IS NULL
GROUP BY evt.event_id, evt.title;
