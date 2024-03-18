use Ecommerce;
GO 
Create PROCEDURE dbo.SearchProducts
     @PreviousMonthsCount INT
AS
BEGIN 
  DECLARE @SearchDate DATETIME = DATEADD(MONTH, -@PreviousMonthsCount, GETDATE());

	SELECT TOP 10 p.Id, p.Name , SUM([Count])  'Quantity Sold' , COUNT(c.Id) 'No. Unique Customers' FROM dbo.Product p
	INNER JOIN dbo.ProductBaskets pb ON p.Id = pb.ProductId
	INNER JOIN dbo.Basket bsk on bsk.Id = pb.BasketId
	INNER JOIN dbo.Customer c on c.id = bsk.CustomerId
	where pb.DateCreated >= @SearchDate
	Group By p.Name , pb.DateCreated ,p.Id
	Order BY pb.DateCreated
END
GO