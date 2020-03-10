SELECT e.Id, e.Title, e.Start_date, e.End_date, e.State,s.Name, s.FirstName, s.Email from Event as e
LEFT JOIN Student as s ON e.Student_id = s.Id
WHERE e.Teacher_id = 14