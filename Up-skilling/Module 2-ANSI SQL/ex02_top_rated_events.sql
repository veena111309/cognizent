-- Retrieve events with an average feedback rating of 4.0 or higher
SELECT 
    evt.event_id, 
    evt.title, 
    AVG(fb.rating) AS avg_rating, 
    COUNT(fb.feedback_id) AS total_feedback
FROM Events evt
INNER JOIN Feedback fb ON evt.event_id = fb.event_id
GROUP BY evt.event_id, evt.title
HAVING AVG(fb.rating) >= 4.0
ORDER BY avg_rating DESC;
