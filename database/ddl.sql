CREATE TABLE office(
office_id int PRIMARY KEY not null,
location varchar(225),
phone_num varchar(11)
);
CREATE TABLE car(
plate_id varchar(8) not null PRIMARY key,
model varchar(225),
year SMALLINT,
status int not null,
office_id int,
FOREIGN KEY(office_id) REFERENCES office(office_id)
);
CREATE TABLE customer(
customer_id int IDENTITY(1,1) PRIMARY KEY not null,
name varchar(225),
email varchar(225) UNIQUE,
password varchar(20),
phone_num varchar(11)
);
CREATE TABLE reservation(
reservation_id int IDENTITY(1,1) PRIMARY KEY not null,
customer_id int,
plate_id varchar(8),
payment int not null,
cost int,
start_date date,
end_date date,
FOREIGN KEY(customer_id) REFERENCES customer(customer_id),
FOREIGN KEY(plate_id) REFERENCES car(plate_id)
);