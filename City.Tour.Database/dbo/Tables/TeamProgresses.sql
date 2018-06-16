CREATE TABLE [dbo].[TeamProgresses] (
    [Id]            UNIQUEIDENTIFIER CONSTRAINT [DF_TeamProgresses_Id] DEFAULT (newsequentialid()) NOT NULL,
    [TeamId]        UNIQUEIDENTIFIER NOT NULL,
    [IsPassMap1]    BIT              CONSTRAINT [DF_TeamProgresses_IsPassMap1] DEFAULT ((0)) NOT NULL,
    [IsPassMap2]    BIT              CONSTRAINT [DF_TeamProgresses_IsPassMap2] DEFAULT ((0)) NOT NULL,
    [IsPassMap3]    BIT              CONSTRAINT [DF_TeamProgresses_IsPassMap3] DEFAULT ((0)) NOT NULL,
    [IsPassMap4]    BIT              CONSTRAINT [DF_TeamProgresses_IsPassMap4] DEFAULT ((0)) NOT NULL,
    [IsPassMap5]    BIT              CONSTRAINT [DF_TeamProgresses_IsPassMap5] DEFAULT ((0)) NOT NULL,
    [IsPassMap6]    BIT              CONSTRAINT [DF_TeamProgresses_IsPassMap6] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle1] BIT              CONSTRAINT [DF_TeamProgresses_IsPassPuzzle1] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle2] BIT              CONSTRAINT [DF_TeamProgresses_IsPassPuzzle2] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle3] BIT              CONSTRAINT [DF_TeamProgresses_IsPassPuzzle3] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle4] BIT              CONSTRAINT [DF_TeamProgresses_IsPassPuzzle4] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle5] BIT              CONSTRAINT [DF_TeamProgresses_IsPassPuzzle5] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle6] BIT              CONSTRAINT [DF_TeamProgresses_IsPassPuzzle6] DEFAULT ((0)) NOT NULL,
    [ModifyTime]    DATETIME         CONSTRAINT [DF_TeamProgresses_ModifyTime] DEFAULT (getdate()) NOT NULL,
    [CreateTime]    DATETIME         CONSTRAINT [DF_TeamProgresses_CreateTime] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TeamProgresses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TeamProgresses_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'團隊Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TeamProgresses', @level2type = N'COLUMN', @level2name = N'TeamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'團隊進度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TeamProgresses', @level2type = N'COLUMN', @level2name = N'Id';

