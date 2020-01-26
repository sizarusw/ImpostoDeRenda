# ImpostoDeRenda

REACT JS + .NetCore 3.1 + Entity Framework Core + SQLServer

Baixe o SQLServer.

Para configurar o banco vá no arquivo DataAccess/ImpostoRendaContext.cs e altere a string de conexão:

"data source=localhost\SQLEXPRESS; initial catalog=Sudden;persist security info=True;user id=sa;password=teste"

Como é EF você pode usar como code first e migrar para o banco ou simplesmente criar a tabela manualmente (Arquivo está na raiz ContribuinteArquivoDB.sql):

USE [NomeDoBanco]
GO

/****** Object:  Table [dbo].[Contribuinte]    Script Date: 26/01/2020 15:58:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contribuinte](
	[IdContribuinte] [int] IDENTITY(1,1) NOT NULL,
	[Cpf] [varchar](14) NOT NULL,
	[Nome] [varchar](255) NOT NULL,
	[Renda] [decimal](18, 2) NOT NULL,
	[QuantidadeDependente] [int] NOT NULL,
 CONSTRAINT [PK_Contribuinte] PRIMARY KEY CLUSTERED 
(
	[IdContribuinte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO







