-- Brands table
CREATE TABLE brands (
    brand_id INT PRIMARY KEY IDENTITY(1,1),
    brand_name VARCHAR(50) NOT NULL
);

-- Categories table
CREATE TABLE categories (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(50) NOT NULL
);

-- Products table
CREATE TABLE products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    product_name VARCHAR(100) NOT NULL,
    brand_id INT NOT NULL,
    category_id INT NOT NULL,
    model_year INT,
    list_price DECIMAL(10,2),
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

INSERT INTO brands (brand_name) VALUES
('Samsung'),
('Apple'),
('Dell'),
('HP');

INSERT INTO categories (category_name) VALUES
('Mobile'),
('Laptop'),
('Tablet');

INSERT INTO products 
(product_name, brand_id, category_id, model_year, list_price)
VALUES
('Galaxy S21', 1, 1, 2021, 700),
('iPhone 13', 2, 1, 2022, 900),
('Dell Inspiron', 3, 2, 2021, 550),
('HP Pavilion', 4, 2, 2020, 480),
('iPad Air', 2, 3, 2022, 650);

SELECT 
    p.product_name,
    b.brand_name,
    c.category_name,
    p.model_year,
    p.list_price
FROM products p
INNER JOIN brands b 
    ON p.brand_id = b.brand_id
INNER JOIN categories c 
    ON p.category_id = c.category_id
WHERE p.list_price > 500
ORDER BY p.list_price ASC;