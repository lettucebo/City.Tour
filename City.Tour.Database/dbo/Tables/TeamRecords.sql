CREATE TABLE [dbo].[TeamRecords] (
    [Id]                  UNIQUEIDENTIFIER CONSTRAINT [DF_TeamRecords_Id] DEFAULT (newsequentialid()) NOT NULL,
    [TeamId]              UNIQUEIDENTIFIER NOT NULL,
    [Puzzle1StartTime]    DATETIME         NULL,
    [Puzzle1CompleteTime] DATETIME         NULL,
    [Puzzle2StartTime]    DATETIME         NULL,
    [Puzzle2CompleteTime] DATETIME         NULL,
    [Puzzle3StartTime]    DATETIME         NULL,
    [Puzzle3CompleteTime] DATETIME         NULL,
    [Puzzle4StartTime]    DATETIME         NULL,
    [Puzzle4CompleteTime] DATETIME         NULL,
    [Puzzle5StartTime]    DATETIME         NULL,
    [Puzzle5CompleteTime] DATETIME         NULL,
    [Puzzle6StartTime]    DATETIME         NULL,
    [Puzzle6CompleteTime] DATETIME         NULL,
    [IsPassPuzzle1Map]    BIT              CONSTRAINT [DF_TeamRecords_IsPassPuzzle1Map] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle2Map]    BIT              CONSTRAINT [DF_TeamRecords_IsPassPuzzle2Map] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle3Map]    BIT              CONSTRAINT [DF_TeamRecords_IsPassPuzzle3Map] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle4Map]    BIT              CONSTRAINT [DF_TeamRecords_IsPassPuzzle4Map] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle5Map]    BIT              CONSTRAINT [DF_TeamRecords_IsPassPuzzle5Map] DEFAULT ((0)) NOT NULL,
    [IsPassPuzzle6Map]    BIT              CONSTRAINT [DF_TeamRecords_IsPassPuzzle6Map] DEFAULT ((0)) NOT NULL,
    [PassPuzzle1MapTime]  DATETIME         NULL,
    [PassPuzzle2MapTime]  DATETIME         NULL,
    [PassPuzzle3MapTime]  DATETIME         NULL,
    [PassPuzzle4MapTime]  DATETIME         NULL,
    [PassPuzzle5MapTime]  DATETIME         NULL,
    [PassPuzzle6MapTime]  DATETIME         NULL,
    CONSTRAINT [PK_TeamRecord] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TeamRecord_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TeamRecords', @level2type = N'COLUMN', @level2name = N'TeamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'團隊進度紀錄列表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TeamRecords', @level2type = N'COLUMN', @level2name = N'Id';

