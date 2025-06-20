-- Criação das tabelas
CREATE TABLE Customers (
    customer_id NUMBER PRIMARY KEY,
    name VARCHAR2(100),
    email VARCHAR2(100),
    created_date DATE
);

CREATE TABLE Products (
    product_id NUMBER PRIMARY KEY,
    product_name VARCHAR2(100),
    category VARCHAR2(50)
);

CREATE TABLE Orders (
    order_id NUMBER PRIMARY KEY,
    customer_id NUMBER REFERENCES Customers(customer_id),
    order_date DATE,
    total_amount NUMBER
);

CREATE TABLE Order_Items (
    order_item_id NUMBER PRIMARY KEY,
    order_id NUMBER REFERENCES Orders(order_id),
    product_id NUMBER REFERENCES Products(product_id),
    quantity NUMBER,
    price NUMBER
);

-- Inserts de teste
INSERT INTO Customers VALUES (1, 'John Doe', 'john@example.com', SYSDATE-100);
INSERT INTO Customers VALUES (2, 'Jane Smith', 'jane@example.com', SYSDATE-50);
INSERT INTO Products VALUES (1, 'Product A', 'Books');
INSERT INTO Products VALUES (2, 'Product B', 'Books');
INSERT INTO Products VALUES (3, 'Product C', 'Electronics');
INSERT INTO Orders VALUES (1, 1, SYSDATE-10, 300);
INSERT INTO Orders VALUES (2, 2, SYSDATE-20, 500);
INSERT INTO Orders VALUES (3, 1, SYSDATE-5, 200);
INSERT INTO Order_Items VALUES (1, 1, 1, 2, 100);
INSERT INTO Order_Items VALUES (2, 1, 2, 1, 100);
INSERT INTO Order_Items VALUES (3, 2, 3, 2, 250);
INSERT INTO Order_Items VALUES (4, 3, 1, 1, 100); 
