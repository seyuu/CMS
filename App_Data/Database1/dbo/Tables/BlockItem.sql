CREATE TABLE [dbo].[BlockItem] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [BlockId] INT NOT NULL,
    CONSTRAINT [PK_dbo.BlockItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.BlockItem_dbo.Block_BlockId] FOREIGN KEY ([BlockId]) REFERENCES [dbo].[Block] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_BlockId]
    ON [dbo].[BlockItem]([BlockId] ASC);

