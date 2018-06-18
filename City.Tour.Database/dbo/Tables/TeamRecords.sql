CREATE TABLE [dbo].[TeamRecords] (
    [Id]                 UNIQUEIDENTIFIER CONSTRAINT [DF_TeamRecords_Id] DEFAULT (newsequentialid()) NOT NULL,
    [TeamId]             UNIQUEIDENTIFIER NOT NULL,
    [PuzzleStartTime]    DATETIME         NOT NULL,
    [PuzzleCompleteTime] DATETIME         NULL,
    [IsPassPuzzleMap]    BIT              CONSTRAINT [DF_TeamRecords_IsPassPuzzle1Map] DEFAULT ((0)) NOT NULL,
    [PassPuzzleMapTime]  DATETIME         NULL,
    [CreateTime]         DATETIME         CONSTRAINT [DF_TeamRecords_CreateTime] DEFAULT (getdate()) NOT NULL,
    [ModifyTime]         DATETIME         CONSTRAINT [DF_TeamRecords_ModifyTime] DEFAULT (getdate()) NOT NULL,
    [Sort]               INT              NOT NULL,
    [TourPuzzleId]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_TeamRecord] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TeamRecord_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id]),
    CONSTRAINT [FK_TeamRecords_TourPuzzles] FOREIGN KEY ([TourPuzzleId]) REFERENCES [dbo].[TourPuzzles] ([Id])
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TeamRecords', @level2type = N'COLUMN', @level2name = N'TeamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'團隊進度紀錄列表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TeamRecords', @level2type = N'COLUMN', @level2name = N'Id';

