CREATE TABLE [dbo].[Puzzles] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_Puzzles_Id] DEFAULT (newsequentialid()) NOT NULL,
    [CreateTime]   DATETIME         CONSTRAINT [DF_Puzzles_CreateTime] DEFAULT (getdate()) NOT NULL,
    [TourId]       UNIQUEIDENTIFIER NOT NULL,
    [IsDelete]     BIT              CONSTRAINT [DF_Puzzles_IsDelete] DEFAULT ((0)) NOT NULL,
    [Name]         NVARCHAR (500)   NOT NULL,
    [Descript]     NVARCHAR (MAX)   NULL,
    [Picture]      NVARCHAR (MAX)   NULL,
    [Sort]         INT              CONSTRAINT [DF_Puzzles_Sort] DEFAULT ((0)) NULL,
    [ModifyTime]   DATETIME         CONSTRAINT [DF_Puzzles_ModifyTime] DEFAULT (getdate()) NOT NULL,
    [MapImage]     NVARCHAR (MAX)   NULL,
    [MapAnswer]    NVARCHAR (500)   NULL,
    [Longitude]    FLOAT (53)       NULL,
    [Latitude]     FLOAT (53)       NULL,
    [MapNameImage] NVARCHAR (500)   NULL,
    [Answer]       NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Puzzles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Puzzles_Tours] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tours] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_FK_Puzzles_Tours]
    ON [dbo].[Puzzles]([TourId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'謎題列表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Puzzles', @level2type = N'COLUMN', @level2name = N'Id';

