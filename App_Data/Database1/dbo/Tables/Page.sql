CREATE TABLE [dbo].[Page] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NULL,
    [Title]       NVARCHAR (255) NULL,
    [Description] NVARCHAR (255) NULL,
    [Keywords]    NVARCHAR (255) NULL,
    [Active]      BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Page] PRIMARY KEY CLUSTERED ([Id] ASC)
);

