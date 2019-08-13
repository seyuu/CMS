CREATE TABLE [dbo].[Block] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [No]        INT NOT NULL,
    [Width]     INT NOT NULL,
    [SectionId] INT NOT NULL,
    CONSTRAINT [PK_dbo.Block] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Block_dbo.Section_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SectionId]
    ON [dbo].[Block]([SectionId] ASC);

