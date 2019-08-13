CREATE TABLE [dbo].[Form] (
    [Id]      INT            NOT NULL,
    [Email]   NVARCHAR (255) NULL,
    [Subject] NVARCHAR (255) NULL,
    CONSTRAINT [PK_dbo.Form] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Form_dbo.Block_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Block] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Form]([Id] ASC);

