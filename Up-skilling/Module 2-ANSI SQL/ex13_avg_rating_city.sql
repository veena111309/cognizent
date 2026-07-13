-- Calculate average feedback rating categorized by city locations
SELECT 
    evt.city, 
    CAST(AVG(fb.rating) AS DECIMAL(10,2)) AS average_rating
FROM Events evt
INNER JOIN Feedback fb ON evt.event_id = fb.event_id
GROUP BY evt.city;
