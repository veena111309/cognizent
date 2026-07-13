-- List users who have submitted feedback for 3 or more events
SELECT 
    usr.user_id, 
    usr.full_name, 
    COUNT(fb.feedback_id) AS feedback_count
FROM Users usr
INNER JOIN Feedback fb ON usr.user_id = fb.user_id
GROUP BY usr.user_id, usr.full_name
HAVING COUNT(fb.feedback_id) >= 3;
