CREATE TABLE [dbo].[Text] (
    [Id]      INT            NOT NULL,
    [Content] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Text] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Text_dbo.Block_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Block] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[Text]([Id] ASC);

