CREATE OR ALTER PROC sp_Usuarios_d
@Id_usuario int
AS
BEGIN
	DELETE FROM Usuarios WHERE Id_usuario = @Id_usuario
END