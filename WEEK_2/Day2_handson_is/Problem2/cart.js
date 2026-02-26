// cartUtils.js

export const calculateTotal = (products) =>
    products.reduce(
        (total, product) => total + product.price * product.quantity,0);

export const generateInvoice = (products) => {
    const total = calculateTotal(products);

    const items = products
        .map(product => `
                ${product.name}
                Price: ₹${product.price}
                Quantity: ${product.quantity}
                Subtotal: ₹${product.price * product.quantity}
                -------------------------`).join("");

    return `
 SHOPPING CART INVOICE
=========================
${items}
TOTAL AMOUNT: ₹${total}
=========================
`;
};