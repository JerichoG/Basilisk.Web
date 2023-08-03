SELECT ship.CompanyName AS [Shipper Name],
		ship.Phone AS [Contact],
		ord.InvoiceNumber AS [Invoice Number],
		ship.Cost AS [Fee],
		prod.Name AS [Product]
FROM Deliveries as ship
JOIN dbo.Orders AS ord ON ord.DeliveryId = ship.Id
JOIN dbo.OrderDetails AS ordet ON ord.InvoiceNumber = ordet.InvoiceNumber
JOIN dbo.Products AS prod ON prod.Id = ordet.ProductId
GROUP BY  ship.CompanyName,
		ship.Phone,
		ship.Cost,
		ord.InvoiceNumber,
		prod.Name

SELECT ship.CompanyName,
		ord.InvoiceNumber AS [Invoice Number],
		prod.[Name] AS [Product],
		ord.DestinationAddress AS [Address],
		ord.DestinationCity AS [City],
		ord.DestinationPostalCode AS [Postal Code]
FROM dbo.Deliveries AS ship
LEFT JOIN dbo.Orders AS ord ON ship.Id = ord.DeliveryId
LEFT JOIN dbo.OrderDetails AS ordet ON ord.InvoiceNumber = ordet.InvoiceNumber
LEFT JOIN dbo.Products AS prod ON ordet.ProductId = prod.Id
LEFT JOIN dbo.Customers AS cus ON cus.Id = ord.CustomerId
WHERE ship.Id= 12
GROUP BY  ord.InvoiceNumber,
		prod.[Name],
		ord.DestinationAddress,
		ord.DestinationCity,
		ord.DestinationPostalCode,
		ship.CompanyName


SELECT prod.Name,
		ord.InvoiceNumber
FROM dbo.Deliveries AS ship
JOIN dbo.Orders AS ord ON ship.Id = ord.DeliveryId
JOIN dbo.OrderDetails AS ordet ON ord.InvoiceNumber = ordet.InvoiceNumber
JOIN dbo.Products AS prod ON prod.Id = ordet.ProductId


SELECT *
FROM dbo.Deliveries

SELECT * 
FROM dbo.OrderDetails

SELECT  *
FROM dbo.Orders