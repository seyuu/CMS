CREATE TABLE [dbo].[Setting] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [AdminKullaniciAdi] NVARCHAR (50)  NULL,
    [AdminSifre]        NVARCHAR (50)  NULL,
    [Title]             NVARCHAR (255) NOT NULL,
    [Description]       NVARCHAR (255) NULL,
    [Keywords]          NVARCHAR (255) NULL,
    [SmtpServer]        NVARCHAR (255) NULL,
    [SmtpEmail]         NVARCHAR (255) NULL,
    [SmtpPassword]      NVARCHAR (255) NULL,
    [SmtpPort]          INT            NOT NULL,
    [SmtpSsl]           BIT            NOT NULL,
    CONSTRAINT [PK_dbo.Setting] PRIMARY KEY CLUSTERED ([Id] ASC)
);

