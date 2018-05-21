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
    [Puzzle1Id]   UNIQUEIDENTIFIER NULL,
    [Puzzle2Id]   UNIQUEIDENTIFIER NULL,
    [Puzzle3Id]   UNIQUEIDENTIFIER NULL,
    [Puzzle4Id]   UNIQUEIDENTIFIER NULL,
    [Puzzle5Id]   UNIQUEIDENTIFIER NULL,
    [Puzzle6Id]   UNIQUEIDENTIFIER NULL,
    [Sort]        INT              CONSTRAINT [DF_Tours_Sort] DEFAULT ((0)) NOT NULL,
    [VideoUrl]    NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Tours] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tours_Puzzles] FOREIGN KEY ([Puzzle1Id]) REFERENCES [dbo].[Puzzles] ([Id]),
    CONSTRAINT [FK_Tours_Puzzles1] FOREIGN KEY ([Puzzle2Id]) REFERENCES [dbo].[Puzzles] ([Id]),
    CONSTRAINT [FK_Tours_Puzzles2] FOREIGN KEY ([Puzzle3Id]) REFERENCES [dbo].[Puzzles] ([Id]),
    CONSTRAINT [FK_Tours_Puzzles3] FOREIGN KEY ([Puzzle4Id]) REFERENCES [dbo].[Puzzles] ([Id]),
    CONSTRAINT [FK_Tours_Puzzles4] FOREIGN KEY ([Puzzle5Id]) REFERENCES [dbo].[Puzzles] ([Id]),
    CONSTRAINT [FK_Tours_Puzzles5] FOREIGN KEY ([Puzzle6Id]) REFERENCES [dbo].[Puzzles] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Tours_Puzzles5]
    ON [dbo].[Tours]([Puzzle6Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Tours_Puzzles4]
    ON [dbo].[Tours]([Puzzle5Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Tours_Puzzles3]
    ON [dbo].[Tours]([Puzzle4Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Tours_Puzzles2]
    ON [dbo].[Tours]([Puzzle3Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Tours_Puzzles1]
    ON [dbo].[Tours]([Puzzle2Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Tours_Puzzles]
    ON [dbo].[Tours]([Puzzle1Id] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'路線列表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Tours', @level2type = N'COLUMN', @level2name = N'Id';

