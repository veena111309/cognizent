-- Identify users who have never registered for any events
SELECT 
    usr.user_id, 
    usr.full_name, 
    usr.email
FROM Users usr
WHERE NOT EXISTS (
    SELECT 1 
    FROM Registrations reg 
    WHERE reg.user_id = usr.user_id
);
