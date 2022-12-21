CREATE OR ALTER PROC sp_Usuarios_u
@Id_usuario int,
@Nombre_usuario varchar(50),
@Fecha_nacimiento date,
@Genero varchar(1),
@Estado_usuario varchar(50)
AS
BEGIN
	UPDATE Usuarios SET
	Nombre_usuario = @Nombre_usuario,
	Fecha_nacimiento = @Fecha_nacimiento,
	Genero = @Genero,
	Estado_usuario = @Estado_usuario
	WHERE Id_usuario = @Id_usuario;
END