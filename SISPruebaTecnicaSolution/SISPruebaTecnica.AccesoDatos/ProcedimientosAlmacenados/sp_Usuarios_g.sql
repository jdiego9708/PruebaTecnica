CREATE OR ALTER PROC sp_Usuarios_g
@Tipo_busqueda varchar(50),
@Texto_busqueda varchar(50)
AS
BEGIN
	IF (@Tipo_busqueda = 'TODOS')
	BEGIN
		SELECT *
		FROM Usuarios
	END
	ELSE IF (@Tipo_busqueda = 'ID USUARIO')
	BEGIN
		SELECT *
		FROM Usuarios
		WHERE Id_usuario = CONVERT(int, @Texto_busqueda)
	END
	ELSE IF (@Tipo_busqueda = 'NOMBRE')
	BEGIN
		SELECT *
		FROM Usuarios
		WHERE Nombre_usuario like @Texto_busqueda + '%'
	END
END