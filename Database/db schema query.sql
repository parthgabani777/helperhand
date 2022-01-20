/* Query for creating database */

CREATE DATABASE helperland;

/* Query for creating tables*/

CREATE TABLE Tbl_admin(
	PK_admin_email varchar(50) NOT NULL,
	admin_password varchar(max) NOT NULL,
	CONSTRAINT PK_Tbl_admin PRIMARY KEY (PK_admin_email)
);

CREATE TABLE Tbl_customer(
	PK_customer_Id bigint NOT NULL,
	customer_email varchar(50) NOT NULL,
	customer_firstname varchar(20) NOT NULL,
	customer_lastname varchar(20) NOT NULL,
	customer_mobile bigint NOT NULL,
	customer_password varchar(max) NOT NULL,
	customer_dob date NULL,
	customer_preferred_language int NULL,
	CONSTRAINT PK_Tbl_customer PRIMARY KEY (PK_customer_Id)
);

CREATE TABLE Tbl_customer_address(
	PK_address_id bigint NOT NULL,
	FK_customer_id bigint NOT NULL,
	customer_street_name varchar(50) NULL,
	customer_house_no varchar(50) NOT NULL,
	customer_postal_code int NOT NULL,
	customer_city varchar(50) NOT NULL,
	customer_address_mobile bigint NOT NULL,
	CONSTRAINT PK_Tbl_customer_address PRIMARY KEY (PK_address_id),
	CONSTRAINT FK_Tbl_customer_address_Tbl_customer FOREIGN KEY(PK_address_id)
		REFERENCES Tbl_customer (PK_customer_Id)
		ON UPDATE CASCADE
		ON DELETE CASCADE
);

CREATE TABLE Tbl_customer_favorite_block(
	CPK_customer_id bigint NOT NULL,
	CPK_service_provider_id bigint NOT NULL,
	customer_preference int NOT NULL,
	CONSTRAINT PK_Tbl_customer_favorite_block PRIMARY KEY (CPK_customer_id,CPK_service_provider_id),
	CONSTRAINT FK_Tbl_customer_favorite_block_Tbl_customer FOREIGN KEY(CPK_customer_id)
		REFERENCES Tbl_customer (PK_customer_Id)
		ON UPDATE CASCADE
		ON DELETE CASCADE,
	CONSTRAINT FK_Tbl_customer_favorite_block_Tbl_service_provider FOREIGN KEY(CPK_service_provider_id)
		REFERENCES Tbl_service_provider (PK_service_provider_id)
);

CREATE TABLE Tbl_extra_services(
	FK_service_id bigint NOT NULL,
	extra_service_name int NOT NULL,
	CONSTRAINT FK_Tbl_extra_services_Tbl_services FOREIGN KEY(FK_service_id)
		REFERENCES Tbl_services (PK_service_Id)
		ON UPDATE CASCADE
		ON DELETE CASCADE
);

CREATE TABLE Tbl_rating(
	CPK_customer_id bigint NOT NULL,
	CPK_service_id bigint NOT NULL,
	rating_on_time float NOT NULL,
	rating_friendly float NOT NULL,
	rating_qos float NOT NULL,
	CONSTRAINT PK_Tbl_rating PRIMARY KEY (CPK_customer_id,CPK_service_id),
	CONSTRAINT FK_Tbl_rating_Tbl_customer FOREIGN KEY(CPK_customer_id)
		REFERENCES Tbl_customer (PK_customer_Id)
		ON UPDATE CASCADE
		ON DELETE CASCADE,
	CONSTRAINT FK_Tbl_rating_Tbl_services FOREIGN KEY(CPK_service_id)
		REFERENCES Tbl_services (PK_service_Id)
);

CREATE TABLE Tbl_service_provider(
	PK_service_provider_id bigint NOT NULL,
	service_provider_email varchar(50) NOT NULL,
	service_provider_name varchar(20) NOT NULL,
	service_provider_surname varchar(20) NOT NULL,
	service_provider_mobile bigint NOT NULL,
	service_provider_password varchar(max) NOT NULL,
	service_provider_pet_at_home int NOT NULL,
	service_provider_dob date NULL,
	service_provider_gender int NULL,
	service_provider_avatar int NULL,
	service_provider_street_name varchar(50) NULL,
	service_provider_house_no varchar(50) NOT NULL,
	service_provider_postal_code int NOT NULL,
	service_provider_city varchar(50) NOT NULL,
	CONSTRAINT PK_Tbl_service_provider PRIMARY KEY (PK_service_provider_id)
);

CREATE TABLE Tbl_service_provider_block(
	CPK_service_provider_id bigint NOT NULL,
	CPK_customer_id bigint NOT NULL,
	CONSTRAINT PK_Tbl_service_provider_block PRIMARY KEY CLUSTERED (CPK_service_provider_id,CPK_customer_id),
	CONSTRAINT FK_Tbl_service_provider_block_Tbl_service_provider FOREIGN KEY(CPK_service_provider_id)
		REFERENCES Tbl_service_provider (PK_service_provider_id)
		ON UPDATE CASCADE
		ON DELETE CASCADE,
	CONSTRAINT FK_Tbl_service_provider_block_Tbl_customer FOREIGN KEY(CPK_customer_id)
		REFERENCES Tbl_customer (PK_customer_Id)
);

CREATE TABLE Tbl_services(
	PK_service_Id bigint NOT NULL,
	service_datetime datetime NOT NULL,
	service_hours float NOT NULL,
	service_total_charge float NOT NULL,
	service_status int NOT NULL,
	service_comments text NULL,
	service_pets_at_home int NOT NULL,
	FK_customer_id bigint NOT NULL,
	FK_service_provider_id bigint NOT NULL,
	CONSTRAINT PK_Tbl_services PRIMARY KEY CLUSTERED (PK_service_Id),
	CONSTRAINT FK_Tbl_services_Tbl_customer FOREIGN KEY(FK_customer_id)
		REFERENCES Tbl_customer (PK_customer_Id)
		ON UPDATE CASCADE
		ON DELETE CASCADE,
	CONSTRAINT FK_Tbl_services_Tbl_service_provider FOREIGN KEY(FK_service_provider_id)
		REFERENCES Tbl_service_provider (PK_service_provider_id)
);