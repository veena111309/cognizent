-- Identify users who submitted feedback with a rating below 3.0
SELECT 
    usr.full_name, 
    evt.title, 
    fb.rating, 
    fb.comments
FROM Feedback fb
INNER JOIN Users usr ON fb.user_id = usr.user_id
INNER JOIN Events evt ON fb.event_id = evt.event_id
WHERE fb.rating < 3.0;
