CREATE DATABASE GestionEventos;
GO

USE GestionEventos;
GO

CREATE TABLE Eventos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Fecha DATE NOT NULL,
    Lugar NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Participantes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    EventoId INT NOT NULL,
    CONSTRAINT FK_Participantes_Eventos FOREIGN KEY (EventoId) REFERENCES Eventos(Id)
);
GO

CREATE TABLE Organizadores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Cargo NVARCHAR(50) NOT NULL,
    EventoId INT NOT NULL,
    CONSTRAINT FK_Organizadores_Eventos FOREIGN KEY (EventoId) REFERENCES Eventos(Id)
);
GO

INSERT INTO Eventos (Nombre, Fecha, Lugar)
VALUES
('Conferencia de Tecnología 2026', '2026-10-15', 'Centro de Convenciones San Salvador'),
('Feria de Emprendimiento', '2026-11-05', 'Universidad de El Salvador'),
('Torneo Universitario de Fútbol', '2026-11-20', 'Estadio Universitario');
GO

INSERT INTO Participantes (Nombre, Email, EventoId)
VALUES
('Carlos Martínez', 'carlos.martinez@email.com', 1),
('Ana López', 'ana.lopez@email.com', 2),
('Diego Hernández', 'diego.hernandez@email.com', 3);
GO

INSERT INTO Organizadores (Nombre, Cargo, EventoId)
VALUES
('María García', 'Coordinadora General', 1),
('José Ramírez', 'Director de Logística', 2),
('Sofía Rodríguez', 'Coordinadora Deportiva', 3);
GO

SELECT * FROM Eventos;
SELECT * FROM Participantes;
SELECT * FROM Organizadores;
GO