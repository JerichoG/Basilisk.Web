Select * from dbo.Categories

INSERT INTO dbo.Categories
VALUES('Olahraga', 'Perlengkapan sport seperti bola, pemukul kasti, samsak dan lain sebagainya.' )

UPDATE dbo.Categories
SET Id = 5
WHERE Name = 'Olahraga'

SELECT * FROM dbo.Suppliers
SELECT * FROM dbo.Salesmen

ALTER TABLE dbo.Cart
ADD CONSTRAINT FK_CusCart FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(Id)

ALTER TABLE dbo.Cart
ADD PRIMARY KEY (Id)

ALTER TABLE dbo.Cart
ADD CONSTRAINT FK_ProdCart FOREIGN KEY (ProductId) REFERENCES dbo.Products(Id)

SELECT cus.Id, cus.CompanyName, cus.Address, cus.City, cus.Phone,
		SUM(ca.Quantity)
FROM dbo.Customers AS cus
LEFT JOIN dbo.Cart AS ca ON ca.CustomerId = cus.Id
GROUP BY cus.Id, cus.CompanyName, cus.Address, cus.City, cus.Phone

SELECT * FROM dbo.Customers

DELETE FROM dbo.Orders
WHERE InvoiceNumber = '05-23-0005'

SELECT * FROM dbo.Cart
SELECT * FROM dbo.Orders
SELECT * FROM dbo.OrderDetails
SELECT * FROM dbo.Products
SELECT *
INSERT INTO dbo.Cart 
VALUES(1, 8, 30, 0)


SELECT sup.CompanyName, prod.[Name], prod.Price, cart.Quantity
FROM dbo.Cart AS cart
JOIN dbo.Products AS prod ON prod.Id = cart.ProductId
JOIN dbo.Suppliers AS sup ON sup.Id = prod.SupplierId
GROUP BY sup.CompanyName, prod.[Name], prod.Price, cart.Quantity

SELECT * FROM dbo.Cart
SELECT * FROM dbo.Products WHERE Id=14
WHERE ord.OrderDate == 





SELECT *
FROM dbo.Deliveries

SELECT *
FROM dbo.Products

UPDATE dbo.Products
SET Stock = Stock + 10
WHERE Name = 'Chainsaw Man'

SELECT * FROM dbo.Orders
SELECT * FROM dbo.OrderDetails

DELETE dbo.OrderDetails
WHERE ProductId = '241'

DELETE dbo.Orders
WHERE InvoiceNumber = '05-23-0004' 