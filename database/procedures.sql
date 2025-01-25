CREATE PROCEDURE addCar
    @plate_id VARCHAR(8),
    @model VARCHAR(225),
    @year SMALLINT,
    @status INT,
    @office_id INT
AS
BEGIN    
    INSERT INTO car (plate_id, model, year, status, office_id
    VALUES (@plate_id, @model, @year, @status, @office_id);
END;

CREATE PROCEDURE get_all_cars
AS
BEGIN
    SELECT * FROM car;
END;


CREATE PROCEDURE get_cars_with_customer_id]
    @customer_id INT
AS
BEGIN
    Select * from reservation as r 
    join car as c on r.plate_id = c.plate_id
    where r.customer_id = @customer_id;
end;


CREATE PROCEDURE get_cars_with_model_and_customer_id
    @customer_id INT,
    @model varchar(255)
AS
BEGIN
    Select * from reservation as r
    join car as c on r.plate_id = c.plate_id
    where r.customer_id = @customer_id
    and c.model like @model + '%';
end;


CREATE PROCEDURE get_cars_with_status_and_customer_id
    @customer_id INT,
    @status int
AS
BEGIN
    Select * from reservation as r
    join car as c on r.plate_id = c.plate_id
    where c.status = @status;
end;


CREATE PROCEDURE get_cars_with_year_and_customer_id    
    @customer_id INT,
    @year SMALLINT
AS
BEGIN
    Select * from reservation as r
    join car as c on r.plate_id = c.plate_id
    where r.customer_id = @customer_id
    and c.year = @year;
end;


CREATE PROCEDURE get_filtered_cars
    @model VARCHAR(225) = NULL,
    @year SMALLINT = NULL,
    @status INT = NULL
AS
BEGIN
    SELECT * FROM car
    WHERE (@model IS NULL OR model LIKE @model + '%')
    AND (@year IS NULL OR year = @year)
    AND (@status IS NULL OR status = @status);
END;


CREATE PROCEDURE login
    @p_email VARCHAR(225),
    @p_password VARCHAR(20)
AS
BEGIN
    SELECT customer_id, name, email
    FROM customer
    WHERE email = @p_email AND password = @p_password;
END;


CREATE PROCEDURE reserve_car
    @p_customer_id INT,
    @p_plate_id VARCHAR(8),
    @p_payment INT,
    @p_cost INT,
    @p_start_date DATE,
    @p_end_date DATE
AS
BEGIN
    INSERT INTO reservation (customer_id, plate_id, payment, cost, start_date, end_date)
    VALUES (@p_customer_id, @p_plate_id, @p_payment, @p_cost, @p_start_date, @p_end_date);
	
	UPDATE car
    SET status = 1
    WHERE plate_id = @p_plate_id;
END;


CREATE PROCEDURE sign_up
    @p_name VARCHAR(225),
    @p_email VARCHAR(225),
    @p_password VARCHAR(20),
    @p_phone_num VARCHAR(11)
AS
BEGIN
    INSERT INTO customer (name, email, password, phone_num)
    VALUES (@p_name, @p_email, @p_password, @p_phone_num);
END;