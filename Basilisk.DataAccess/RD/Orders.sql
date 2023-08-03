--Index :
--inv number
--cust name
--orderdate
--totalPembayaran (total barang + diskon + delivery cost)

SELECT ord.InvoiceNumber, cus.CompanyName, ord.OrderDate, 
		SUM((ordet.UnitPrice * (1-(ordet.Discount/100)) * ordet.Quantity) + ord.DeliveryCost) AS [Total Fee]
FROM dbo.Orders AS ord
JOIN dbo.Customers AS cus ON cus.Id = ord.CustomerId
JOIN dbo.OrderDetails AS ordet ON ordet.InvoiceNumber = ord.InvoiceNumber
GROUP BY ord.InvoiceNumber, cus.CompanyName, ord.OrderDate

--Detail :
--Transaction Info:
--inv number
--cust name
--sales name
--orderdate
SELECT ord.InvoiceNumber AS [Invoice Number], 
		cus.CompanyName AS [Customer], 
		CONCAT(sales.FirstName, ' ', sales.LastName) AS [Sales],
		ord.OrderDate [Order Date]
FROM dbo.Orders AS ord
JOIN dbo.Customers AS cus ON cus.Id = ord.CustomerId
JOIN dbo.Salesmen AS sales ON sales.EmployeeNumber = ord.SalesEmployeeNumber
WHERE ord.InvoiceNumber = '04-18-0002'

--delivery info :
--shipdate, duedate, deliveryaddress, delcity, deliverypostalcode,  deliverycost
SELECT ord.ShippedDate AS [Shipped Date],
		ord.DueDate AS [Due Date], ord.DestinationAddress AS [Destination Address],
		ord.DestinationCity AS [Destination City], ord.DestinationPostalCode AS [Destination Postal Code],
		ord.DeliveryCost AS [Cost]
FROM dbo.Orders AS ord
WHERE ord.InvoiceNumber = '04-18-0002'


--product info:
--prodname, qty, harga, diskon
SELECT prod.[Name] AS [Product], ordet.Quantity AS [Qty], 
		ordet.UnitPrice AS [Unit Price], ordet.Discount AS [Discount]
FROM dbo.OrderDetails AS ordet
LEFT JOIN dbo.Products AS prod ON prod.Id = ordet.ProductId
WHERE ordet.InvoiceNumber = '05-18-0001'

select * from dbo.Orders
select * from dbo.OrderDetails
select * from dbo.Deliveries
select * from dbo.Products
select * from dbo.Customers