-- Find events that have no physical resources allocated to them
SELECT 
    evt.event_id, 
    evt.title
FROM Events evt
LEFT OUTER JOIN Resources res ON evt.event_id = res.event_id
WHERE res.resource_id IS NULL;
