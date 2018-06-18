CREATE TABLE [dbo].[Tours] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_Tours_Id] DEFAULT (newsequentialid()) NOT NULL,
    [CreateTime]  DATETIME         CONSTRAINT [DF_Tours_CreateTime] DEFAULT (getdate()) NOT NULL,
    [Name]        NVARCHAR (500)   NOT NULL,
    [Descript]    NVARCHAR (MAX)   NULL,
    [Banner]      NVARCHAR (MAX)   NULL,
    [BannerThumb] NVARCHAR (MAX)   NULL,
    [IsDelete]    BIT              CONSTRAINT [DF_Tours_IsDelete] DEFAULT ((0)) NOT NULL,
    [ModifyTime]  DATETIME         CONSTRAINT [DF_Tours_ModifyTime] DEFAULT (getdate()) NOT NULL,
    [Info]        NVARCHAR (MAX)   NULL,
    [Sort]        INT              CONSTRAINT [DF_Tours_Sort] DEFAULT ((0)) NOT NULL,
    [VideoUrl]    NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Tours] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'路線列表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Tours', @level2type = N'COLUMN', @level2name = N'Id';

