-- Find users who signed up recently but have not registered for any events
SELECT 
    usr.user_id, 
    usr.full_name, 
    usr.email, 
    usr.registration_date
FROM Users usr
LEFT OUTER JOIN Registrations reg ON usr.user_id = reg.user_id
WHERE reg.registration_id IS NULL 
  AND usr.registration_date >= '2026-01-01';
