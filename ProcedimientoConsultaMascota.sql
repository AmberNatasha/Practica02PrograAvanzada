USE Practica2;
GO

CREATE PROCEDURE dbo.sp_ConsultarMascotas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Cedula,
        c.Nombre AS NombreCliente,
        m.Nombre AS NombreMascota,
        m.Especie,
        m.Peso
    FROM dbo.Mascotas m
    INNER JOIN dbo.Clientes c
        ON m.IdCliente = c.IdCliente
    ORDER BY c.Nombre, m.Nombre;
END
GO


EXEC dbo.sp_ConsultarMascotas