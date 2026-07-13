-- Count events registered and feedback provided for each user
SELECT 
    usr.user_id, 
    usr.full_name, 
    COUNT(DISTINCT reg.event_id) AS events_registered, 
    COUNT(DISTINCT fb.feedback_id) AS feedback_provided
FROM Users usr
LEFT OUTER JOIN Registrations reg ON usr.user_id = reg.user_id
LEFT OUTER JOIN Feedback fb ON usr.user_id = fb.user_id
GROUP BY usr.user_id, usr.full_name;
