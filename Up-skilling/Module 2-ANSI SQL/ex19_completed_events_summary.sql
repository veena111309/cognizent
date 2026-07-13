-- Total registrations and feedback scores for completed events
SELECT 
    evt.event_id, 
    evt.title, 
    COUNT(DISTINCT reg.registration_id) AS total_registrations, 
    CAST(AVG(fb.rating) AS DECIMAL(10,2)) AS avg_feedback_rating
FROM Events evt
LEFT OUTER JOIN Registrations reg ON evt.event_id = reg.event_id
LEFT OUTER JOIN Feedback fb ON evt.event_id = fb.event_id
WHERE evt.status = 'completed'
GROUP BY evt.event_id, evt.title;
