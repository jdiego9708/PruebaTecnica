CREATE OR ALTER PROC sp_Usuarios_i
@Id_usuario int output,
@Nombre_usuario varchar(50),
@Fecha_nacimiento date,
@Genero varchar(1),
@Estado_usuario varchar(50)
AS
BEGIN
	INSERT INTO Usuarios
	VALUES (@Nombre_usuario, @Fecha_nacimiento, @Genero,
	@Estado_usuario)
	SET @Id_usuario = SCOPE_IDENTITY();
END