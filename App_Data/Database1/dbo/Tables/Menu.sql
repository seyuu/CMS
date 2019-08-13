CREATE TABLE [dbo].[Menu] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX) NULL,
    [Title] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Menu] PRIMARY KEY CLUSTERED ([Id] ASC)
);

