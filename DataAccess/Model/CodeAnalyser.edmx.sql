
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/10/2015 12:44:00
-- Generated from EDMX file: C:\CLR\TFS\Tools\Code Analyzer\DataAccess\Model\CodeAnalyser.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CodeAnalyzer];
GO
IF SCHEMA_ID(N'CodeAnalyzer') IS NULL EXECUTE(N'CREATE SCHEMA [CodeAnalyzer]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[CodeAnalyzer].[FK_CategoryDeclarationRuleDeclaration]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[RuleDeclaration] DROP CONSTRAINT [FK_CategoryDeclarationRuleDeclaration];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_LanguageDeclarationRuleDeclaration_LanguageDeclaration]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[LanguageDeclarationRuleDeclaration] DROP CONSTRAINT [FK_LanguageDeclarationRuleDeclaration_LanguageDeclaration];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_LanguageDeclarationRuleDeclaration_RuleDeclaration]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[LanguageDeclarationRuleDeclaration] DROP CONSTRAINT [FK_LanguageDeclarationRuleDeclaration_RuleDeclaration];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_ProjectDefinitionCategoryDefinition]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[CategoryDefinition] DROP CONSTRAINT [FK_ProjectDefinitionCategoryDefinition];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_CategoryDefinitionRuleDefinition]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[RuleDefinition] DROP CONSTRAINT [FK_CategoryDefinitionRuleDefinition];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_CategoryDefinitionCategoryDeclaration]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[CategoryDefinition] DROP CONSTRAINT [FK_CategoryDefinitionCategoryDeclaration];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_RuleDeclarationRuleDefinition]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[RuleDefinition] DROP CONSTRAINT [FK_RuleDeclarationRuleDefinition];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_ProjectDefinitionDirectoryDefinition]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[Directory] DROP CONSTRAINT [FK_ProjectDefinitionDirectoryDefinition];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_ProjectDefinitionFileDefinition]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[File] DROP CONSTRAINT [FK_ProjectDefinitionFileDefinition];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_UserUserRole]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[UserRole] DROP CONSTRAINT [FK_UserUserRole];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_UserProjectDefinition]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[ProjectDefinition] DROP CONSTRAINT [FK_UserProjectDefinition];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_UserProjectDefinition1]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[ProjectDefinition] DROP CONSTRAINT [FK_UserProjectDefinition1];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_UserProjectDefinition2]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[ProjectDefinition] DROP CONSTRAINT [FK_UserProjectDefinition2];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_MatchBatch]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[Match] DROP CONSTRAINT [FK_MatchBatch];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_ProjectDefinitionExcludedDirectory]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[ExcludedDirectory] DROP CONSTRAINT [FK_ProjectDefinitionExcludedDirectory];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_ProjectDefinitionExcludedFile]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[ExcludedFile] DROP CONSTRAINT [FK_ProjectDefinitionExcludedFile];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[FK_RuleDeclarationMatch]', 'F') IS NOT NULL
    ALTER TABLE [CodeAnalyzer].[Match] DROP CONSTRAINT [FK_RuleDeclarationMatch];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[CodeAnalyzer].[LanguageDeclaration]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[LanguageDeclaration];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[CategoryDeclaration]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[CategoryDeclaration];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[RuleDeclaration]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[RuleDeclaration];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[CategoryDefinition]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[CategoryDefinition];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[ProjectDefinition]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[ProjectDefinition];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[RuleDefinition]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[RuleDefinition];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[File]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[File];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[Directory]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[Directory];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[User]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[User];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[UserRole]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[UserRole];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[Version]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[Version];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[Match]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[Match];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[Batch]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[Batch];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[ExcludedDirectory]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[ExcludedDirectory];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[ExcludedFile]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[ExcludedFile];
GO
IF OBJECT_ID(N'[CodeAnalyzer].[LanguageDeclarationRuleDeclaration]', 'U') IS NOT NULL
    DROP TABLE [CodeAnalyzer].[LanguageDeclarationRuleDeclaration];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LanguageDeclaration'
CREATE TABLE [CodeAnalyzer].[LanguageDeclaration] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Extension] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CategoryDeclaration'
CREATE TABLE [CodeAnalyzer].[CategoryDeclaration] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RuleDeclaration'
CREATE TABLE [CodeAnalyzer].[RuleDeclaration] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Severity] int  NOT NULL,
    [Expression] nvarchar(max)  NOT NULL,
    [CategoryDeclaration_Id] int  NOT NULL
);
GO

-- Creating table 'CategoryDefinition'
CREATE TABLE [CodeAnalyzer].[CategoryDefinition] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Enabled] bit  NOT NULL,
    [ProjectDefinition_Id] int  NOT NULL,
    [CategoryDeclaration_Id] int  NOT NULL
);
GO

-- Creating table 'ProjectDefinition'
CREATE TABLE [CodeAnalyzer].[ProjectDefinition] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Enabled] bit  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Owner_Id] int  NOT NULL,
    [Contributers_Id] int  NOT NULL,
    [Followers_Id] int  NOT NULL
);
GO

-- Creating table 'RuleDefinition'
CREATE TABLE [CodeAnalyzer].[RuleDefinition] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Enabled] bit  NOT NULL,
    [CategoryDefinition_Id] int  NOT NULL,
    [RuleDeclaration_Id] int  NOT NULL
);
GO

-- Creating table 'File'
CREATE TABLE [CodeAnalyzer].[File] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Enabled] bit  NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [ProjectDefinition_Id] int  NOT NULL
);
GO

-- Creating table 'Directory'
CREATE TABLE [CodeAnalyzer].[Directory] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Enabled] bit  NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [ProjectDefinition_Id] int  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [CodeAnalyzer].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserRole'
CREATE TABLE [CodeAnalyzer].[UserRole] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'Version'
CREATE TABLE [CodeAnalyzer].[Version] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [VersionNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Match'
CREATE TABLE [CodeAnalyzer].[Match] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LineNumber] int  NOT NULL,
    [CodeExtract] nvarchar(max)  NOT NULL,
    [RootDirectoryPath] nvarchar(max)  NOT NULL,
    [Filename] nvarchar(max)  NOT NULL,
    [Status] int  NOT NULL,
    [Batch_Id] int  NOT NULL,
    [RuleDeclaration_Id] int  NOT NULL
);
GO

-- Creating table 'Batch'
CREATE TABLE [CodeAnalyzer].[Batch] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TimeStamp] datetime  NOT NULL
);
GO

-- Creating table 'ExcludedDirectory'
CREATE TABLE [CodeAnalyzer].[ExcludedDirectory] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Enabled] bit  NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [ProjectDefinition_Id] int  NOT NULL
);
GO

-- Creating table 'ExcludedFile'
CREATE TABLE [CodeAnalyzer].[ExcludedFile] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Enabled] bit  NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [ProjectDefinition_Id] int  NOT NULL
);
GO

-- Creating table 'LanguageDeclarationRuleDeclaration'
CREATE TABLE [CodeAnalyzer].[LanguageDeclarationRuleDeclaration] (
    [LanguageDeclaration_Id] int  NOT NULL,
    [Rules_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LanguageDeclaration'
ALTER TABLE [CodeAnalyzer].[LanguageDeclaration]
ADD CONSTRAINT [PK_LanguageDeclaration]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CategoryDeclaration'
ALTER TABLE [CodeAnalyzer].[CategoryDeclaration]
ADD CONSTRAINT [PK_CategoryDeclaration]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RuleDeclaration'
ALTER TABLE [CodeAnalyzer].[RuleDeclaration]
ADD CONSTRAINT [PK_RuleDeclaration]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CategoryDefinition'
ALTER TABLE [CodeAnalyzer].[CategoryDefinition]
ADD CONSTRAINT [PK_CategoryDefinition]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectDefinition'
ALTER TABLE [CodeAnalyzer].[ProjectDefinition]
ADD CONSTRAINT [PK_ProjectDefinition]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RuleDefinition'
ALTER TABLE [CodeAnalyzer].[RuleDefinition]
ADD CONSTRAINT [PK_RuleDefinition]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'File'
ALTER TABLE [CodeAnalyzer].[File]
ADD CONSTRAINT [PK_File]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Directory'
ALTER TABLE [CodeAnalyzer].[Directory]
ADD CONSTRAINT [PK_Directory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [CodeAnalyzer].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserRole'
ALTER TABLE [CodeAnalyzer].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Version'
ALTER TABLE [CodeAnalyzer].[Version]
ADD CONSTRAINT [PK_Version]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Match'
ALTER TABLE [CodeAnalyzer].[Match]
ADD CONSTRAINT [PK_Match]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Batch'
ALTER TABLE [CodeAnalyzer].[Batch]
ADD CONSTRAINT [PK_Batch]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedDirectory'
ALTER TABLE [CodeAnalyzer].[ExcludedDirectory]
ADD CONSTRAINT [PK_ExcludedDirectory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedFile'
ALTER TABLE [CodeAnalyzer].[ExcludedFile]
ADD CONSTRAINT [PK_ExcludedFile]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LanguageDeclaration_Id], [Rules_Id] in table 'LanguageDeclarationRuleDeclaration'
ALTER TABLE [CodeAnalyzer].[LanguageDeclarationRuleDeclaration]
ADD CONSTRAINT [PK_LanguageDeclarationRuleDeclaration]
    PRIMARY KEY CLUSTERED ([LanguageDeclaration_Id], [Rules_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryDeclaration_Id] in table 'RuleDeclaration'
ALTER TABLE [CodeAnalyzer].[RuleDeclaration]
ADD CONSTRAINT [FK_CategoryDeclarationRuleDeclaration]
    FOREIGN KEY ([CategoryDeclaration_Id])
    REFERENCES [CodeAnalyzer].[CategoryDeclaration]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryDeclarationRuleDeclaration'
CREATE INDEX [IX_FK_CategoryDeclarationRuleDeclaration]
ON [CodeAnalyzer].[RuleDeclaration]
    ([CategoryDeclaration_Id]);
GO

-- Creating foreign key on [LanguageDeclaration_Id] in table 'LanguageDeclarationRuleDeclaration'
ALTER TABLE [CodeAnalyzer].[LanguageDeclarationRuleDeclaration]
ADD CONSTRAINT [FK_LanguageDeclarationRuleDeclaration_LanguageDeclaration]
    FOREIGN KEY ([LanguageDeclaration_Id])
    REFERENCES [CodeAnalyzer].[LanguageDeclaration]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Rules_Id] in table 'LanguageDeclarationRuleDeclaration'
ALTER TABLE [CodeAnalyzer].[LanguageDeclarationRuleDeclaration]
ADD CONSTRAINT [FK_LanguageDeclarationRuleDeclaration_RuleDeclaration]
    FOREIGN KEY ([Rules_Id])
    REFERENCES [CodeAnalyzer].[RuleDeclaration]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageDeclarationRuleDeclaration_RuleDeclaration'
CREATE INDEX [IX_FK_LanguageDeclarationRuleDeclaration_RuleDeclaration]
ON [CodeAnalyzer].[LanguageDeclarationRuleDeclaration]
    ([Rules_Id]);
GO

-- Creating foreign key on [ProjectDefinition_Id] in table 'CategoryDefinition'
ALTER TABLE [CodeAnalyzer].[CategoryDefinition]
ADD CONSTRAINT [FK_ProjectDefinitionCategoryDefinition]
    FOREIGN KEY ([ProjectDefinition_Id])
    REFERENCES [CodeAnalyzer].[ProjectDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDefinitionCategoryDefinition'
CREATE INDEX [IX_FK_ProjectDefinitionCategoryDefinition]
ON [CodeAnalyzer].[CategoryDefinition]
    ([ProjectDefinition_Id]);
GO

-- Creating foreign key on [CategoryDefinition_Id] in table 'RuleDefinition'
ALTER TABLE [CodeAnalyzer].[RuleDefinition]
ADD CONSTRAINT [FK_CategoryDefinitionRuleDefinition]
    FOREIGN KEY ([CategoryDefinition_Id])
    REFERENCES [CodeAnalyzer].[CategoryDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryDefinitionRuleDefinition'
CREATE INDEX [IX_FK_CategoryDefinitionRuleDefinition]
ON [CodeAnalyzer].[RuleDefinition]
    ([CategoryDefinition_Id]);
GO

-- Creating foreign key on [CategoryDeclaration_Id] in table 'CategoryDefinition'
ALTER TABLE [CodeAnalyzer].[CategoryDefinition]
ADD CONSTRAINT [FK_CategoryDefinitionCategoryDeclaration]
    FOREIGN KEY ([CategoryDeclaration_Id])
    REFERENCES [CodeAnalyzer].[CategoryDeclaration]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryDefinitionCategoryDeclaration'
CREATE INDEX [IX_FK_CategoryDefinitionCategoryDeclaration]
ON [CodeAnalyzer].[CategoryDefinition]
    ([CategoryDeclaration_Id]);
GO

-- Creating foreign key on [RuleDeclaration_Id] in table 'RuleDefinition'
ALTER TABLE [CodeAnalyzer].[RuleDefinition]
ADD CONSTRAINT [FK_RuleDeclarationRuleDefinition]
    FOREIGN KEY ([RuleDeclaration_Id])
    REFERENCES [CodeAnalyzer].[RuleDeclaration]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RuleDeclarationRuleDefinition'
CREATE INDEX [IX_FK_RuleDeclarationRuleDefinition]
ON [CodeAnalyzer].[RuleDefinition]
    ([RuleDeclaration_Id]);
GO

-- Creating foreign key on [ProjectDefinition_Id] in table 'Directory'
ALTER TABLE [CodeAnalyzer].[Directory]
ADD CONSTRAINT [FK_ProjectDefinitionDirectoryDefinition]
    FOREIGN KEY ([ProjectDefinition_Id])
    REFERENCES [CodeAnalyzer].[ProjectDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDefinitionDirectoryDefinition'
CREATE INDEX [IX_FK_ProjectDefinitionDirectoryDefinition]
ON [CodeAnalyzer].[Directory]
    ([ProjectDefinition_Id]);
GO

-- Creating foreign key on [ProjectDefinition_Id] in table 'File'
ALTER TABLE [CodeAnalyzer].[File]
ADD CONSTRAINT [FK_ProjectDefinitionFileDefinition]
    FOREIGN KEY ([ProjectDefinition_Id])
    REFERENCES [CodeAnalyzer].[ProjectDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDefinitionFileDefinition'
CREATE INDEX [IX_FK_ProjectDefinitionFileDefinition]
ON [CodeAnalyzer].[File]
    ([ProjectDefinition_Id]);
GO

-- Creating foreign key on [User_Id] in table 'UserRole'
ALTER TABLE [CodeAnalyzer].[UserRole]
ADD CONSTRAINT [FK_UserUserRole]
    FOREIGN KEY ([User_Id])
    REFERENCES [CodeAnalyzer].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserRole'
CREATE INDEX [IX_FK_UserUserRole]
ON [CodeAnalyzer].[UserRole]
    ([User_Id]);
GO

-- Creating foreign key on [Owner_Id] in table 'ProjectDefinition'
ALTER TABLE [CodeAnalyzer].[ProjectDefinition]
ADD CONSTRAINT [FK_UserProjectDefinition]
    FOREIGN KEY ([Owner_Id])
    REFERENCES [CodeAnalyzer].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProjectDefinition'
CREATE INDEX [IX_FK_UserProjectDefinition]
ON [CodeAnalyzer].[ProjectDefinition]
    ([Owner_Id]);
GO

-- Creating foreign key on [Contributers_Id] in table 'ProjectDefinition'
ALTER TABLE [CodeAnalyzer].[ProjectDefinition]
ADD CONSTRAINT [FK_UserProjectDefinition1]
    FOREIGN KEY ([Contributers_Id])
    REFERENCES [CodeAnalyzer].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProjectDefinition1'
CREATE INDEX [IX_FK_UserProjectDefinition1]
ON [CodeAnalyzer].[ProjectDefinition]
    ([Contributers_Id]);
GO

-- Creating foreign key on [Followers_Id] in table 'ProjectDefinition'
ALTER TABLE [CodeAnalyzer].[ProjectDefinition]
ADD CONSTRAINT [FK_UserProjectDefinition2]
    FOREIGN KEY ([Followers_Id])
    REFERENCES [CodeAnalyzer].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProjectDefinition2'
CREATE INDEX [IX_FK_UserProjectDefinition2]
ON [CodeAnalyzer].[ProjectDefinition]
    ([Followers_Id]);
GO

-- Creating foreign key on [Batch_Id] in table 'Match'
ALTER TABLE [CodeAnalyzer].[Match]
ADD CONSTRAINT [FK_MatchBatch]
    FOREIGN KEY ([Batch_Id])
    REFERENCES [CodeAnalyzer].[Batch]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MatchBatch'
CREATE INDEX [IX_FK_MatchBatch]
ON [CodeAnalyzer].[Match]
    ([Batch_Id]);
GO

-- Creating foreign key on [ProjectDefinition_Id] in table 'ExcludedDirectory'
ALTER TABLE [CodeAnalyzer].[ExcludedDirectory]
ADD CONSTRAINT [FK_ProjectDefinitionExcludedDirectory]
    FOREIGN KEY ([ProjectDefinition_Id])
    REFERENCES [CodeAnalyzer].[ProjectDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDefinitionExcludedDirectory'
CREATE INDEX [IX_FK_ProjectDefinitionExcludedDirectory]
ON [CodeAnalyzer].[ExcludedDirectory]
    ([ProjectDefinition_Id]);
GO

-- Creating foreign key on [ProjectDefinition_Id] in table 'ExcludedFile'
ALTER TABLE [CodeAnalyzer].[ExcludedFile]
ADD CONSTRAINT [FK_ProjectDefinitionExcludedFile]
    FOREIGN KEY ([ProjectDefinition_Id])
    REFERENCES [CodeAnalyzer].[ProjectDefinition]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectDefinitionExcludedFile'
CREATE INDEX [IX_FK_ProjectDefinitionExcludedFile]
ON [CodeAnalyzer].[ExcludedFile]
    ([ProjectDefinition_Id]);
GO

-- Creating foreign key on [RuleDeclaration_Id] in table 'Match'
ALTER TABLE [CodeAnalyzer].[Match]
ADD CONSTRAINT [FK_RuleDeclarationMatch]
    FOREIGN KEY ([RuleDeclaration_Id])
    REFERENCES [CodeAnalyzer].[RuleDeclaration]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RuleDeclarationMatch'
CREATE INDEX [IX_FK_RuleDeclarationMatch]
ON [CodeAnalyzer].[Match]
    ([RuleDeclaration_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------