use master
go
IF NOT EXISTS(SELECT name FROM master.dbo.sysdatabases WHERE NAME = 'MachineMindAdoNet')
CREATE DATABASE MachineMindAdoNet

GO 

USE MachineMindAdoNet

GO

if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Users')
create table Users(
	UserId NVARCHAR(50) PRIMARY KEY,
    EmployeeNumber NVARCHAR(20) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
    -- Otros atributos relevantes para la entidad Usuario
);

go

select * from dbo.Users

-- Tabla para la entidad "Rol"
if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Roles')
CREATE TABLE dbo.Roles (
    RoleId INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
    -- Otros atributos relevantes para la entidad Rol
);

select * from dbo.Roles;


-- Tabla para la tabla intermedia "UsuarioRol"
if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'UserRoles')
CREATE TABLE dbo.UserRoles (
    UserRoleId INT PRIMARY KEY,
    UserId NVARCHAR(50) NOT NULL,
    RoleId INT NOT NULL,
    CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES dbo.Users (UserId),
    CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles (RoleId)
);

select * from dbo.UserRoles;

-- Tabla para la entidad "Planta"
if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Plants')
CREATE TABLE dbo.Plants (
    PlantId NVARCHAR(50) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
    -- Otros atributos relevantes para la entidad Planta
);
select * from dbo.Plants;


-- Tabla para la entidad "Línea de Producción"
if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ProductionLines')
CREATE TABLE dbo.ProductionLines (
    ProductionLineId NVARCHAR(50) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    PlantId NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_ProductionLines_Plants FOREIGN KEY (PlantId) REFERENCES dbo.Plants (PlantId)
    -- Otros atributos relevantes para la entidad Línea de Producción
);

select * from dbo.ProductionLines;

-- Tabla para la entidad "Máquina"
if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Machines')
CREATE TABLE dbo.Machines (
    MachineId NVARCHAR(50) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    ProductionLineId NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Machines_ProductionLines FOREIGN KEY (ProductionLineId) REFERENCES dbo.ProductionLines (ProductionLineId)
    -- Otros atributos relevantes para la entidad Máquina
);
select * from dbo.Machines;


-- Tabla para la entidad "Receta"
if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Recipes')
CREATE TABLE dbo.Recipes (
    RecipeId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    MachineId NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_Recipes_Machines FOREIGN KEY (MachineId) REFERENCES dbo.Machines (MachineId)
    -- Otros atributos relevantes para la entidad Receta
);

select * from dbo.Recipes;


-- Tabla para la entidad "Parámetro"
if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Parameters')
-- Tabla para la entidad "Parámetro"
CREATE TABLE dbo.Parameters (
    ParameterId INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    UnitOfMeasure NVARCHAR(10) NOT NULL,
    Value NVARCHAR(10) NOT NULL,
    RecipeId INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha de creación
    LastModifiedDate DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha de última modificación
    CONSTRAINT FK_Parameters_Recipes FOREIGN KEY (RecipeId) REFERENCES dbo.Recipes (RecipeId)
    -- Otros atributos relevantes para la entidad Parámetro
);
select * from dbo.Parameters;

-- Tabla para la entidad "Ángulo de Movimiento"
if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Angles')
-- Tabla para la entidad "Ángulo de Movimiento"
CREATE TABLE dbo.Angles (
    AngleId INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    InitialValue INT NOT NULL,
    FinalValue INT NOT NULL,
    RecipeId INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha de creación
    LastModifiedDate DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha de última modificación
    CONSTRAINT FK_Angles_Recipes FOREIGN KEY (RecipeId) REFERENCES dbo.Recipes (RecipeId)
    -- Otros atributos relevantes para la entidad Ángulo de Movimiento
);
select * from dbo.Angles;