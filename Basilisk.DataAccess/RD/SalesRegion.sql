

SELECT Salesmen.EmployeeNumber, CONCAT(Salesmen.FirstName, ' ',Salesmen.LastName) AS [Full Name] ,
		Salesmen.Level ,Salesmen.BirthDate, Salesmen.HiredDate, Salesmen.Address, Salesmen.City, Salesmen.Phone,
		Salesmen.SuperiorEmployeeNumber, reg.City, reg.Remark
From dbo.Salesmen
JOIN dbo.SalesmenRegions AS saleReg ON saleReg.SalesmanEmployeeNumber = Salesmen.EmployeeNumber
JOIN dbo.Regions AS reg ON reg.Id = saleReg.RegionId
WHERE Salesmen.EmployeeNumber = 'J200'


SELECT *
FROM dbo.Salesmen AS s
WHERE s.EmployeeNumber = 'J069';


SELECT Salesmen.EmployeeNumber, CONCAT(Salesmen.FirstName, ' 
',Salesmen.LastName) AS [Full Name] ,
		Salesmen.BirthDate, Salesmen.HiredDate, Salesmen.Address, Salesmen.City, Salesmen.Phone,
		CONCAT(s.FirstName, ' ', s.LastName) AS [Superior]
From dbo.Salesmen
LEFT JOIN dbo.Salesmen AS s ON s.EmployeeNumber = Salesmen.SuperiorEmployeeNumber