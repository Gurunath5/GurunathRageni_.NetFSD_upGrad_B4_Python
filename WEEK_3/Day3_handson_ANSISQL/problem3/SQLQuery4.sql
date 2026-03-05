-- =========================================
-- CREATE DATABASE
-- =========================================
CREATE DATABASE StoreSalesDb;
GO

USE StoreSalesDb;
GO

-- =========================================
-- CREATE TABLES
-- =========================================

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100) NOT NULL
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    store_id INT NOT NULL,
    order_status INT NOT NULL,
    order_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (store_id) REFERENCES stores(store_id)
);

CREATE TABLE order_items (
    item_id INT PRIMARY KEY,
    order_id INT NOT NULL,
    quantity INT NOT NULL,
    list_price DECIMAL(10,2) NOT NULL,
    discount DECIMAL(4,2) DEFAULT 0,
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

-- =========================================
-- INSERT DATA
-- =========================================

INSERT INTO stores VALUES
(1, 'Hyderabad Store'),
(2, 'Bangalore Store'),
(3, 'Chennai Store');

INSERT INTO orders VALUES
(101, 1, 4, GETDATE()),
(102, 1, 4, GETDATE()),
(103, 2, 4, GETDATE()),
(104, 2, 1, GETDATE()),  -- Not completed
(105, 3, 4, GETDATE());

INSERT INTO order_items VALUES
(1, 101, 2, 1000, 0.10),
(2, 101, 1, 500, 0.05),
(3, 102, 3, 700, 0.00),
(4, 103, 5, 300, 0.15),
(5, 104, 2, 400, 0.10),  -- Not counted (status not 4)
(6, 105, 4, 800, 0.20);

-- =========================================
-- STORE WISE SALES SUMMARY QUERY
-- =========================================

SELECT 
    s.store_name,
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
FROM stores s
INNER JOIN orders o 
    ON s.store_id = o.store_id
INNER JOIN order_items oi 
    ON o.order_id = oi.order_id
WHERE o.order_status = 4
GROUP BY s.store_name
ORDER BY total_sales DESC;