USE [ELTON]
GO
/****** Object:  Table [dbo].[tblCliente]    Script Date: 07/20/2016 16:27:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblCliente](
	[idCliente] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[DataNascimento] [datetime] NOT NULL,
	[Sexo] [bit] NOT NULL,
	[LimiteCompra] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tblCliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[uspClienteInserir]    Script Date: 07/20/2016 16:27:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteInserir]
	@Nome Varchar(100),
	@DataNascimento DateTime,
	@Sexo bit,
	@LimiteCompra decimal(18, 2)
AS
BEGIN
	INSERT INTO tblCliente
	(
		Nome,
		DataNascimento,
		Sexo,
		LimiteCompra
	)
	VALUES
	(
		@Nome,
		@DataNascimento,
		@Sexo,
		@LimiteCompra
	)
	
	SELECT @@IDENTITY AS Retorno
END
GO
/****** Object:  StoredProcedure [dbo].[uspClienteExcluir]    Script Date: 07/20/2016 16:27:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteExcluir]
	@idCliente INT
AS
BEGIN

	DELETE FROM
		tblCliente
	WHERE
		idCliente = @idCliente
	
	SELECT @idCliente AS Retorno
END
GO
/****** Object:  StoredProcedure [dbo].[uspClienteConsultarPorNome]    Script Date: 07/20/2016 16:27:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteConsultarPorNome]
	@Nome varchar(100)
AS
BEGIN

	SELECT
		idCliente,
		Nome,
		DataNascimento,
		Sexo,
		LimiteCompra
	FROM
		tblCliente
	WHERE
		Nome LIKE '%' + @Nome + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[uspClienteConsultarPorId]    Script Date: 07/20/2016 16:27:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteConsultarPorId]
	@IdCliente INT
AS
BEGIN

	SELECT
		idCliente,
		Nome,
		DataNascimento,
		Sexo,
		LimiteCompra
	FROM
		tblCliente
	WHERE
		idCliente = @IdCliente
END
GO
/****** Object:  StoredProcedure [dbo].[uspClienteAlterar]    Script Date: 07/20/2016 16:27:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspClienteAlterar]
	@IdCliente INT,
	@Nome varchar(100),
	@DataNascimento datetime,
	@Sexo bit,
	@LimiteCompra decimal(18, 2)
AS
BEGIN

	UPDATE
		tblCliente
	SET
		Nome = @Nome,
		DataNascimento = @DataNascimento,
		Sexo = @Sexo,
		LimiteCompra = @LimiteCompra
	WHERE
		idCliente = @IdCliente
		
	SELECT @IdCliente AS Retorno

END
GO
