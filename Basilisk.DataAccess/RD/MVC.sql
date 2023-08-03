SELECT p.Id, p.[Name] AS [Product], s.CompanyName AS [Supplier], c.[Name] AS [Category],
		p.[Description], p.Price, p.Stock, p.OnOrder, 
		(CASE 
			WHEN p.Discontinue = 0 THEN 'Continue'
			WHEN p.Discontinue = 1 THEN 'Discontinue'
		END) AS [Discontinue]
FROM dbo.Products AS p
JOIN dbo.Categories AS c ON c.Id = p.CategoryId
JOIN dbo.Suppliers AS s ON s.Id = p.SupplierId

SELECT * FROM dbo.Suppliers
WHERE Suppliers.CompanyName = ''