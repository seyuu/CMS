CREATE TABLE [dbo].[Image] (
    [Id]    INT            NOT NULL,
    [Name]  NVARCHAR (255) NULL,
    [Title] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.Image] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Image_dbo.Block_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Block] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Image]([Id] ASC);

