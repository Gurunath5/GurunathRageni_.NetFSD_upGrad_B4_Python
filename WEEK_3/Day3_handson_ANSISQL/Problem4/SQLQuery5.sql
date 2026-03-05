-- =========================================
-- CREATE DATABASE
-- =========================================
CREATE DATABASE InventoryDb;
GO

USE InventoryDb;
GO

-- =========================================
-- CREATE TABLES
-- =========================================

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100) NOT NULL
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL
);

CREATE TABLE stocks (
    store_id INT,
    product_id INT,
    quantity INT NOT NULL,
    PRIMARY KEY (store_id, product_id),
    FOREIGN KEY (store_id) REFERENCES stores(store_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE order_items (
    item_id INT PRIMARY KEY,
    store_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (store_id) REFERENCES stores(store_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

 

INSERT INTO stores VALUES
(1, 'Hyderabad Store'),
(2, 'Bangalore Store');

INSERT INTO products VALUES
(101, 'Laptop'),
(102, 'Mobile'),
(103, 'Headphones');

INSERT INTO stocks VALUES
(1, 101, 50),
(1, 102, 100),
(1, 103, 80),
(2, 101, 40),
(2, 102, 60),
(2, 103, 70);

INSERT INTO order_items VALUES
(1, 1, 101, 5),
(2, 1, 101, 3),
(3, 1, 102, 10),
(4, 2, 101, 7),
(5, 2, 102, 8);


SELECT 
    p.product_name,
    s.store_name,
    st.quantity AS available_stock,
    ISNULL(SUM(oi.quantity), 0) AS total_quantity_sold
FROM stocks st
INNER JOIN products p 
    ON st.product_id = p.product_id
INNER JOIN stores s 
    ON st.store_id = s.store_id
LEFT JOIN order_items oi 
    ON st.product_id = oi.product_id
    AND st.store_id = oi.store_id
GROUP BY 
    p.product_name,
    s.store_name,
    st.quantity
ORDER BY 
    p.product_name;