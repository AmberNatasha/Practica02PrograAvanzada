USE [Practica2]
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_InsertarCliente]
    @Cedula varchar(50),
    @Nombre varchar(100),
    @Correo varchar(100),
    @Estado bit
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Clientes] ([Cedula], [Nombre], [Correo], [Estado])
    VALUES (@Cedula, @Nombre, @Correo, @Estado);

    SELECT CAST(SCOPE_IDENTITY() AS bigint);
END
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_InsertarMascota]
    @Nombre varchar(100),
    @Especie varchar(100),
    @Raza varchar(100),
    @Peso decimal(8, 2),
    @IdCliente bigint
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Mascotas] ([Nombre], [Especie], [Raza], [Peso], [IdCliente])
    VALUES (@Nombre, @Especie, @Raza, @Peso, @IdCliente);

    SELECT CAST(SCOPE_IDENTITY() AS bigint);
END
GO
