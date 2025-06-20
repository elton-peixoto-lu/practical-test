-- Top 5 clientes que mais gastaram nos últimos 6 meses
DECLARE
    CURSOR c_top_customers IS
        SELECT c.customer_id, c.name, SUM(o.total_amount) AS total_spent
        FROM Customers c
        JOIN Orders o ON c.customer_id = o.customer_id
        WHERE o.order_date >= ADD_MONTHS(SYSDATE, -6)
        GROUP BY c.customer_id, c.name
        ORDER BY total_spent DESC
        FETCH FIRST 5 ROWS ONLY;
BEGIN
    FOR rec IN c_top_customers LOOP
        DBMS_OUTPUT.PUT_LINE('Customer ID: ' || rec.customer_id);
        DBMS_OUTPUT.PUT_LINE('Customer Name: ' || rec.name);
        DBMS_OUTPUT.PUT_LINE('Total Amount Spent: $' || rec.total_spent);

        FOR prod IN (
            SELECT p.product_name, SUM(oi.quantity) AS total_qty
            FROM Orders o
            JOIN Order_Items oi ON o.order_id = oi.order_id
            JOIN Products p ON oi.product_id = p.product_id
            WHERE o.customer_id = rec.customer_id
              AND o.order_date >= ADD_MONTHS(SYSDATE, -6)
            GROUP BY p.product_name
        ) LOOP
            DBMS_OUTPUT.PUT_LINE('    - ' || prod.product_name || ': ' || prod.total_qty || ' units');
        END LOOP;
        DBMS_OUTPUT.PUT_LINE('-----------------------------');
    END LOOP;
END;
/

-- Exemplo de saída:
-- Customer ID: 123
-- Customer Name: John Doe
-- Total Amount Spent: $5000
--     - Product A: 5 units
--     - Product B: 3 units
-- ----------------------------- 
