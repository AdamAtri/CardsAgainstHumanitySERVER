
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/03/2014 17:07:21
-- Generated from EDMX file: C:\Users\Developer\documents\visual studio 2013\Projects\CARDS_AGAINST_HUMANITY\CAH_DataLibrary\CAH_DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'cah') IS NULL EXECUTE(N'CREATE SCHEMA [cah]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[cah].[FK_GameRound]', 'F') IS NOT NULL
    ALTER TABLE [cah].[Rounds] DROP CONSTRAINT [FK_GameRound];
GO
IF OBJECT_ID(N'[cah].[FK_PlayerHand]', 'F') IS NOT NULL
    ALTER TABLE [cah].[Hands] DROP CONSTRAINT [FK_PlayerHand];
GO
IF OBJECT_ID(N'[cah].[FK_RoundHand]', 'F') IS NOT NULL
    ALTER TABLE [cah].[Hands] DROP CONSTRAINT [FK_RoundHand];
GO
IF OBJECT_ID(N'[cah].[FK_GamePlayer_Games]', 'F') IS NOT NULL
    ALTER TABLE [cah].[GamePlayer] DROP CONSTRAINT [FK_GamePlayer_Games];
GO
IF OBJECT_ID(N'[cah].[FK_GamePlayer_Players]', 'F') IS NOT NULL
    ALTER TABLE [cah].[GamePlayer] DROP CONSTRAINT [FK_GamePlayer_Players];
GO
IF OBJECT_ID(N'[cah].[FK_BlackCardRound]', 'F') IS NOT NULL
    ALTER TABLE [cah].[Rounds] DROP CONSTRAINT [FK_BlackCardRound];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[cah].[BlackCards]', 'U') IS NOT NULL
    DROP TABLE [cah].[BlackCards];
GO
IF OBJECT_ID(N'[cah].[Games]', 'U') IS NOT NULL
    DROP TABLE [cah].[Games];
GO
IF OBJECT_ID(N'[cah].[Hands]', 'U') IS NOT NULL
    DROP TABLE [cah].[Hands];
GO
IF OBJECT_ID(N'[cah].[Players]', 'U') IS NOT NULL
    DROP TABLE [cah].[Players];
GO
IF OBJECT_ID(N'[cah].[Rounds]', 'U') IS NOT NULL
    DROP TABLE [cah].[Rounds];
GO
IF OBJECT_ID(N'[cah].[GamePlayer]', 'U') IS NOT NULL
    DROP TABLE [cah].[GamePlayer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BlackCards'
CREATE TABLE [cah].[BlackCards] (
    [BCID] int IDENTITY(1,1) NOT NULL,
    [Sentence] nvarchar(max)  NOT NULL,
    [PickCount] int  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [cah].[Games] (
    [GameID] int IDENTITY(1,1) NOT NULL,
    [GameStatus] int  NOT NULL,
    [NumPlayers] int  NULL,
    [StartTime] datetime  NULL,
    [FinishTime] datetime  NULL,
    [Winner] int  NULL
);
GO

-- Creating table 'Hands'
CREATE TABLE [cah].[Hands] (
    [HandID] int IDENTITY(1,1) NOT NULL,
    [RoundID] int  NOT NULL,
    [PlayerID] int  NOT NULL,
    [Card01] int  NOT NULL,
    [Card02] int  NOT NULL,
    [Card03] int  NOT NULL,
    [Card04] int  NOT NULL,
    [Card05] int  NOT NULL,
    [Card06] int  NOT NULL,
    [Card07] int  NOT NULL,
    [Card08] int  NOT NULL,
    [Card09] int  NOT NULL,
    [Card10] int  NOT NULL,
    [Selection] nvarchar(11)  NULL,
    [Vote] int  NULL
);
GO

-- Creating table 'Players'
CREATE TABLE [cah].[Players] (
    [PlayerID] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(150)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(150)  NOT NULL,
    [FName] nvarchar(30)  NULL,
    [LName] nvarchar(30)  NULL,
    [BirthDate] datetime  NULL,
    [Mobile] nvarchar(20)  NULL,
    [Avatar] varbinary(max)  NULL
);
GO

-- Creating table 'Rounds'
CREATE TABLE [cah].[Rounds] (
    [RoundID] int IDENTITY(1,1) NOT NULL,
    [RoundNum] int  NOT NULL,
    [GameID] int  NOT NULL,
    [BlackCardID] int  NOT NULL
);
GO

-- Creating table 'RoundWinners'
CREATE TABLE [cah].[RoundWinners] (
    [PlayerID] int  NOT NULL,
    [RoundID] int  NOT NULL
);
GO

-- Creating table 'GamePlayer'
CREATE TABLE [cah].[GamePlayer] (
    [Games_GameID] int  NOT NULL,
    [Players_PlayerID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BCID] in table 'BlackCards'
ALTER TABLE [cah].[BlackCards]
ADD CONSTRAINT [PK_BlackCards]
    PRIMARY KEY CLUSTERED ([BCID] ASC);
GO

-- Creating primary key on [GameID] in table 'Games'
ALTER TABLE [cah].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([GameID] ASC);
GO

-- Creating primary key on [HandID] in table 'Hands'
ALTER TABLE [cah].[Hands]
ADD CONSTRAINT [PK_Hands]
    PRIMARY KEY CLUSTERED ([HandID] ASC);
GO

-- Creating primary key on [PlayerID] in table 'Players'
ALTER TABLE [cah].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([PlayerID] ASC);
GO

-- Creating primary key on [RoundID] in table 'Rounds'
ALTER TABLE [cah].[Rounds]
ADD CONSTRAINT [PK_Rounds]
    PRIMARY KEY CLUSTERED ([RoundID] ASC);
GO

-- Creating primary key on [PlayerID], [RoundID] in table 'RoundWinners'
ALTER TABLE [cah].[RoundWinners]
ADD CONSTRAINT [PK_RoundWinners]
    PRIMARY KEY CLUSTERED ([PlayerID], [RoundID] ASC);
GO

-- Creating primary key on [Games_GameID], [Players_PlayerID] in table 'GamePlayer'
ALTER TABLE [cah].[GamePlayer]
ADD CONSTRAINT [PK_GamePlayer]
    PRIMARY KEY CLUSTERED ([Games_GameID], [Players_PlayerID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GameID] in table 'Rounds'
ALTER TABLE [cah].[Rounds]
ADD CONSTRAINT [FK_GameRound]
    FOREIGN KEY ([GameID])
    REFERENCES [cah].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameRound'
CREATE INDEX [IX_FK_GameRound]
ON [cah].[Rounds]
    ([GameID]);
GO

-- Creating foreign key on [PlayerID] in table 'Hands'
ALTER TABLE [cah].[Hands]
ADD CONSTRAINT [FK_PlayerHand]
    FOREIGN KEY ([PlayerID])
    REFERENCES [cah].[Players]
        ([PlayerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerHand'
CREATE INDEX [IX_FK_PlayerHand]
ON [cah].[Hands]
    ([PlayerID]);
GO

-- Creating foreign key on [RoundID] in table 'Hands'
ALTER TABLE [cah].[Hands]
ADD CONSTRAINT [FK_RoundHand]
    FOREIGN KEY ([RoundID])
    REFERENCES [cah].[Rounds]
        ([RoundID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoundHand'
CREATE INDEX [IX_FK_RoundHand]
ON [cah].[Hands]
    ([RoundID]);
GO

-- Creating foreign key on [Games_GameID] in table 'GamePlayer'
ALTER TABLE [cah].[GamePlayer]
ADD CONSTRAINT [FK_GamePlayer_Games]
    FOREIGN KEY ([Games_GameID])
    REFERENCES [cah].[Games]
        ([GameID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Players_PlayerID] in table 'GamePlayer'
ALTER TABLE [cah].[GamePlayer]
ADD CONSTRAINT [FK_GamePlayer_Players]
    FOREIGN KEY ([Players_PlayerID])
    REFERENCES [cah].[Players]
        ([PlayerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GamePlayer_Players'
CREATE INDEX [IX_FK_GamePlayer_Players]
ON [cah].[GamePlayer]
    ([Players_PlayerID]);
GO

-- Creating foreign key on [BlackCardID] in table 'Rounds'
ALTER TABLE [cah].[Rounds]
ADD CONSTRAINT [FK_BlackCardRound]
    FOREIGN KEY ([BlackCardID])
    REFERENCES [cah].[BlackCards]
        ([BCID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BlackCardRound'
CREATE INDEX [IX_FK_BlackCardRound]
ON [cah].[Rounds]
    ([BlackCardID]);
GO

-- Creating foreign key on [PlayerID] in table 'RoundWinners'
ALTER TABLE [cah].[RoundWinners]
ADD CONSTRAINT [FK_PlayerRoundWinner]
    FOREIGN KEY ([PlayerID])
    REFERENCES [cah].[Players]
        ([PlayerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoundID] in table 'RoundWinners'
ALTER TABLE [cah].[RoundWinners]
ADD CONSTRAINT [FK_RoundRoundWinner]
    FOREIGN KEY ([RoundID])
    REFERENCES [cah].[Rounds]
        ([RoundID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoundRoundWinner'
CREATE INDEX [IX_FK_RoundRoundWinner]
ON [cah].[RoundWinners]
    ([RoundID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------