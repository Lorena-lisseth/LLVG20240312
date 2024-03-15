-- Creación de la base de datos LRVG20241103DB
CREATE DATABASE LLVG20241403DB;
USE LLVG20241403DB;

CREATE TABLE Clientes (
    id_cliente INT IDENTITY(1, 1) PRIMARY KEY,
    nombre VARCHAR(200),
    direccion VARCHAR(255),
    correo_electronico VARCHAR(100),
);

-- Tabla de Números de Teléfono
CREATE TABLE NumerosTelefonos (
    id_telefono INT IDENTITY(1, 1) PRIMARY KEY,
    id_cliente INT,
    numero_telefono VARCHAR(9),
    tipo_telefono VARCHAR(50), -- Ejemplo: "Casa", "Trabajo", "Móvil", etc.
    -- Otros campos adicionales que desees agregar
    FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente) ON DELETE CASCADE
);
