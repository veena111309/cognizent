-- Sum of resources and total cost allocated per event
SELECT 
    evt.event_id, 
    evt.title, 
    COUNT(res.resource_id) AS total_resources, 
    SUM(res.cost) AS total_resource_cost
FROM Events evt
INNER JOIN Resources res ON evt.event_id = res.event_id
GROUP BY evt.event_id, evt.title;
