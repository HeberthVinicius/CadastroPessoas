--CREATE Database CadastroPessoas;

/*
USE CadastroPessoas; 

CREATE TABLE Pessoa (
	CPF NVARCHAR(11) NOT NULL PRIMARY KEY,
	Nome VARCHAR(80) NOT NULL,
	RG VARCHAR(15) NOT NULL,
	Email VARCHAR(80) NOT NULL
) 
*/

--PODERIA SER UMA PKG

CREATE PROCEDURE sp_CriarPessoa
	@CPF VARCHAR(11),
	@Nome VARCHAR(80),
	@RG VARCHAR(15),
	@Email VARCHAR(80)
	
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Pessoa WHERE CPF = @CPF)
	BEGIN
		INSERT INTO Pessoa (CPF,Nome,RG,Email) 
		VALUES (@CPF, @NOME, @RG, @Email)
	END
	ELSE
	BEGIN
		THROW 50000, 'CPF já cadastrado!', 1; 
	END
END

--GET
CREATE PROCEDURE sp_GetPessoa
AS
BEGIN
	SELECT CPF, Nome, RG, Email FROM Pessoa
END


--PUT/PATCH

CREATE PROCEDURE sp_AtualizarPessoa
	@CPF VARCHAR(11),
	@Nome VARCHAR(80),
	@RG VARCHAR(15),
	@Email VARCHAR(80)
	
AS
BEGIN
	UPDATE Pessoa 
	SET Nome = @Nome,
		RG = @RG,
		Email = @Email
	WHERE CPF = @CPF;
END

--DELETE

CREATE PROCEDURE sp_DeletarPessoa
	@CPF NVARCHAR(11)
AS
BEGIN
	DELETE FROM Pessoa Where CPF = @CPF
END


