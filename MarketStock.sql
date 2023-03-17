CREATE DATABASE MarketStock

USE MarketStock;

CREATE TABLE Suppliers (
  [SupplierID] INT PRIMARY KEY IDENTITY(1,1),
  [SupplierName] NVARCHAR(MAX) NOT NULL CHECK ( [SupplierName] NOT LIKE N'')
);

CREATE TABLE ProductTypes (
  [ProductTypeID] INT PRIMARY KEY IDENTITY(1,1),
  [ProductTypeName] NVARCHAR(MAX) NOT NULL CHECK ( [ProductTypeName] NOT LIKE N'')
);

CREATE TABLE Products
(
    [ProductID]     INT PRIMARY KEY IDENTITY (1,1),
    [ProductName]   NVARCHAR(MAX)  NOT NULL CHECK ( [ProductName] NOT LIKE N''),
    [ProductTypeID] INT            NOT NULL FOREIGN KEY REFERENCES [ProductTypes] ([ProductTypeID]),
    [SupplierID]    INT            NOT NULL FOREIGN KEY REFERENCES [Suppliers] ([SupplierID]),
    [Cost]          DECIMAL(18, 2) NOT NULL
);

CREATE TABLE Deliveries (
  [DeliveryID] INT PRIMARY KEY IDENTITY(1,1),
  [ProductID] INT NOT NULL FOREIGN KEY REFERENCES [Products]([ProductID]),
  [Quantity] INT NOT NULL,
  [DeliveryDate] DATE NOT NULL
);

insert into Suppliers (SupplierName) values ('Kling LLC');
insert into Suppliers (SupplierName) values ('Kuphal Inc');
insert into Suppliers (SupplierName) values ('Jacobson');
insert into Suppliers (SupplierName) values ('Reinge');
insert into Suppliers (SupplierName) values ('Mante-Mohr');
insert into Suppliers (SupplierName) values ('Crooks');
insert into Suppliers (SupplierName) values ('Sahibo');

insert into ProductTypes (ProductTypeName) values ('Chocolate');
insert into ProductTypes (ProductTypeName) values ('Pecan Raisin');
insert into ProductTypes (ProductTypeName) values ('Pepper');
insert into ProductTypes (ProductTypeName) values ('Beef');
insert into ProductTypes (ProductTypeName) values ('Cream');
insert into ProductTypes (ProductTypeName) values ('Chocolate');
insert into ProductTypes (ProductTypeName) values ('Pastry');
insert into ProductTypes (ProductTypeName) values ('Pepper');
insert into ProductTypes (ProductTypeName) values ('Apple');
insert into ProductTypes (ProductTypeName) values ('Edible Flower');

insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('Milk, Callets', 1, 2, 31.0);
insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('Tarts', 2, 4, 16.92);
insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('Pablano', 3, 6, 1.55);
insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('Tongue, Fresh', 4, 3, 24.39);
insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('18%', 5, 1, 13.36);
insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('Pistoles, White', 6, 1, 39.39);
insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('Key Limepoppy Seed Tea', 7, 5, 30.88);
insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('Gypsy Pepper', 8, 3, 3.22);
insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('Royal Gala', 9, 2, 34.84);
insert into Products (ProductName, ProductTypeID, SupplierID, Cost) values ('Mixed', 10, 6, 20.12);

INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (1, 10, '1-20-2023')
INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (2, 100, '1-1-2023')
INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (3, 15, '12-15-2022')
INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (4, 5, '2-20-2023')
INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (5, 13, '2-01-2023')
INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (6, 20, '2-20-2023')
INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (7, 30, '2-10-2023')
INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (8, 10, '2-16-2023')
INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (9, 60, '2-23-2023')
INSERT INTO Deliveries (ProductID, Quantity, DeliveryDate) VALUES (10, 25, '2-24-2023')

SELECT * FROM Deliveries JOIN Products ON Products.ProductTypeID = Deliveries.DeliveryID ORDER BY Quantity DESC

