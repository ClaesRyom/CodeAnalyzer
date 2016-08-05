-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/21/2014 17:06:05
-- Generated from EDMX file: C:\CLR\TFSPreview\Tools\Code Analyzer\DataAccess\Model\CodeAnalyser.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE master

ALTER DATABASE [CodeAnalyzer] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

IF EXISTS(select * from sys.databases where name='CodeAnalyzer')
	DROP DATABASE CodeAnalyzer
CREATE DATABASE CodeAnalyzer

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






-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                                                                           ║    
-- ║                        >>> SCRIPTING TEST DATA <<<                        ║    
-- ║                                                                           ║    
-- ╚═══════════════════════════════════════════════════════════════════════════╝



-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                           >> GLOBAL VARIABLES <<                          ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝


--   Declaring rule severity values...
--   ───────────────────────────────────────────────────────────────────────────
declare @RuleSeverity_NotDefined int
declare @RuleSeverity_Fatal      int
declare @RuleSeverity_Critical   int
declare @RuleSeverity_Warning    int
declare @RuleSeverity_Info       int

set @RuleSeverity_NotDefined = 0 -- 'NotDefined'
set @RuleSeverity_Fatal      = 1 -- 'Fatal'
set @RuleSeverity_Critical   = 2 -- 'Critical'
set @RuleSeverity_Warning    = 3 -- 'Warning'
set @RuleSeverity_Info       = 4 -- 'Info'


--   Declaring language names and extensions...
--   ───────────────────────────────────────────────────────────────────────────
declare @LanguageCSharp              varchar(30)
declare @LanguageCSharpExtension     varchar(30)
declare @LanguageHtml                varchar(30)
declare @LanguageHtmlExtension       varchar(30)
declare @LanguageCss                 varchar(30)
declare @LanguageCssExtension        varchar(30)
declare @LanguageJavaScript          varchar(30)
declare @LanguageJavaScriptExtension varchar(30)
declare @LanguageJava                varchar(30)
declare @LanguageJavaExtension       varchar(30)
declare @LanguageAsp                 varchar(30)
declare @LanguageAspExtension        varchar(30)
declare @LanguageAspNet              varchar(30)
declare @LanguageAspNetExtension     varchar(30)

set @LanguageCSharp              = 'C#'
set @LanguageCSharpExtension     = '*.cs'
set @LanguageHtml                = 'HTML'
set @LanguageHtmlExtension       = '*.html'
set @LanguageCss                 = 'CSS'
set @LanguageCssExtension        = '*.css'
set @LanguageJavaScript          = 'JavaScript'
set @LanguageJavaScriptExtension = '*.js'
set @LanguageJava                = 'Java'
set @LanguageJavaExtension       = '*.java'
set @LanguageAsp                 = 'ASP'
set @LanguageAspExtension        = '*.asp'
set @LanguageAspNet              = 'ASP.NET'
set @LanguageAspNetExtension     = '*.aspx'

declare @LanguageCSharp_Id     int
declare @LanguageHtml_Id       int
declare @LanguageCss_Id        int
declare @LanguageJavaScript_Id int
declare @LanguageJava_Id       int
declare @LanguageAsp_Id        int
declare @LanguageAspNet_Id     int


--   Declaring project names...
--   ───────────────────────────────────────────────────────────────────────────
declare @ProjectName varchar(30)
set @ProjectName = 'Code Analyser'


-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                        >> LANGUAGE DECLARATIONS <<                        ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝

insert into [CodeAnalyzer].[LanguageDeclaration] (Name, Extension) values ('C#', '*.cs');
insert into [CodeAnalyzer].[LanguageDeclaration] (Name, Extension) values ('CSS', '*.css');
insert into [CodeAnalyzer].[LanguageDeclaration] (Name, Extension) values ('JavaScript', '*.js');
insert into [CodeAnalyzer].[LanguageDeclaration] (Name, Extension) values ('HTML', '*.html');
insert into [CodeAnalyzer].[LanguageDeclaration] (Name, Extension) values ('ASP', '*.asp');
insert into [CodeAnalyzer].[LanguageDeclaration] (Name, Extension) values ('ASP.NET', '*.aspx');
insert into [CodeAnalyzer].[LanguageDeclaration] (Name, Extension) values ('Java', '*.java');

--   Retrieve language id's...
--   ───────────────────────────────────────────────────────────────────────────
select @LanguageCSharp_Id     = Id from [CodeAnalyzer].[LanguageDeclaration] where Extension = @LanguageCSharpExtension
select @LanguageHtml_Id       = Id from [CodeAnalyzer].[LanguageDeclaration] where Extension = @LanguageHtmlExtension
select @LanguageCss_Id        = Id from [CodeAnalyzer].[LanguageDeclaration] where Extension = @LanguageCssExtension
select @LanguageJavaScript_Id = Id from [CodeAnalyzer].[LanguageDeclaration] where Extension = @LanguageJavaScriptExtension
select @LanguageJava_Id       = Id from [CodeAnalyzer].[LanguageDeclaration] where Extension = @LanguageJavaExtension
select @LanguageAsp_Id        = Id from [CodeAnalyzer].[LanguageDeclaration] where Extension = @LanguageAspExtension
select @LanguageAspNet_Id     = Id from [CodeAnalyzer].[LanguageDeclaration] where Extension = @LanguageAspNetExtension

-- print 'Language Id for C#: ' + cast(@LanguageCSharp_Id as varchar(10)) + '.'
-- print 'Language Id for Java: ' + cast(@LanguageJava_Id as varchar(10)) + '.'
-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                        >> CATEGORY DECLARATIONS <<                        ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝

declare @CategoryDeclaration_TryCatches                      varchar(256)
declare @CategoryDeclaration_TryCatches_Desc                 varchar(1024)
declare @CategoryDeclaration_TryCatches_Id                   int
													         
declare @CategoryDeclaration_StaticConstructions             varchar(256)
declare @CategoryDeclaration_StaticConstructions_Desc        varchar(1024)
declare @CategoryDeclaration_StaticConstructions_Id          int

declare @CategoryDeclaration_Counters                        varchar(256)
declare @CategoryDeclaration_Counters_Desc                   varchar(1024)
declare @CategoryDeclaration_Counters_Id                     int

declare @CategoryDeclaration_CheckBasicFileConstruction      varchar(256)
declare @CategoryDeclaration_CheckBasicFileConstruction_Desc varchar(1024)
declare @CategoryDeclaration_CheckBasicFileConstruction_Id   int

declare @CategoryDeclaration_FindSpecialComments             varchar(256)
declare @CategoryDeclaration_FindSpecialComments_Desc        varchar(1024)
declare @CategoryDeclaration_FindSpecialComments_Id          int
													         
declare @CategoryDeclaration_SpecialAttributes               varchar(256)
declare @CategoryDeclaration_SpecialAttributes_Desc          varchar(1024)
declare @CategoryDeclaration_SpecialAttributes_Id            int
													         
declare @CategoryDeclaration_SpacesVsTabs                    varchar(256)
declare @CategoryDeclaration_SpacesVsTabs_Desc               varchar(1024)
declare @CategoryDeclaration_SpacesVsTabs_Id                 int

set @CategoryDeclaration_TryCatches                      = 'Try-Catches'
set @CategoryDeclaration_StaticConstructions             = 'Static constructions'
set @CategoryDeclaration_Counters                        = 'Counters'
set @CategoryDeclaration_CheckBasicFileConstruction      = 'Check basic file construction'
set @CategoryDeclaration_FindSpecialComments             = 'Find special comments'
set @CategoryDeclaration_SpecialAttributes               = 'Special attributes'
set @CategoryDeclaration_SpacesVsTabs                    = 'Spaces vs. Tabs'

set @CategoryDeclaration_TryCatches_Desc                 = 'Identifying bad try-catch constructions in the code base.'
set @CategoryDeclaration_StaticConstructions_Desc        = 'Identifying bad static constructions in the code base.'
set @CategoryDeclaration_Counters_Desc                   = 'Counts different code constructions in the code base. This could for instance be the number of classes, interfaces etc.'
set @CategoryDeclaration_CheckBasicFileConstruction_Desc = 'Identifies files where file header has not been entered, using statements are declared outside the namespace etc.'
set @CategoryDeclaration_FindSpecialComments_Desc        = 'Identify ToDo''s, Notes etc. in the code base.'
set @CategoryDeclaration_SpecialAttributes_Desc          = 'Identify special attributes in AssemblyInfo.cs, on classes or on methods.'
set @CategoryDeclaration_SpacesVsTabs_Desc               = 'Find places in the code where spaces are used for indention.'


insert into [CodeAnalyzer].[CategoryDeclaration] (Name, Description) values (@CategoryDeclaration_TryCatches,                 @CategoryDeclaration_TryCatches_Desc);
insert into [CodeAnalyzer].[CategoryDeclaration] (Name, Description) values (@CategoryDeclaration_StaticConstructions,        @CategoryDeclaration_StaticConstructions_Desc);
insert into [CodeAnalyzer].[CategoryDeclaration] (Name, Description) values (@CategoryDeclaration_Counters,                   @CategoryDeclaration_Counters_Desc);
insert into [CodeAnalyzer].[CategoryDeclaration] (Name, Description) values (@CategoryDeclaration_CheckBasicFileConstruction, @CategoryDeclaration_CheckBasicFileConstruction_Desc);
insert into [CodeAnalyzer].[CategoryDeclaration] (Name, Description) values (@CategoryDeclaration_FindSpecialComments,        @CategoryDeclaration_FindSpecialComments_Desc);
insert into [CodeAnalyzer].[CategoryDeclaration] (Name, Description) values (@CategoryDeclaration_SpecialAttributes,          @CategoryDeclaration_SpecialAttributes_Desc);
insert into [CodeAnalyzer].[CategoryDeclaration] (Name, Description) values (@CategoryDeclaration_SpacesVsTabs,               @CategoryDeclaration_SpacesVsTabs_Desc);


select @CategoryDeclaration_TryCatches_Id                 = Id from [CodeAnalyzer].[CategoryDeclaration] where Name = @CategoryDeclaration_TryCatches
select @CategoryDeclaration_StaticConstructions_Id        = Id from [CodeAnalyzer].[CategoryDeclaration] where Name = @CategoryDeclaration_StaticConstructions
select @CategoryDeclaration_Counters_Id                   = Id from [CodeAnalyzer].[CategoryDeclaration] where Name = @CategoryDeclaration_Counters
select @CategoryDeclaration_CheckBasicFileConstruction_Id = Id from [CodeAnalyzer].[CategoryDeclaration] where Name = @CategoryDeclaration_CheckBasicFileConstruction
select @CategoryDeclaration_FindSpecialComments_Id        = Id from [CodeAnalyzer].[CategoryDeclaration] where Name = @CategoryDeclaration_FindSpecialComments
select @CategoryDeclaration_SpecialAttributes_Id          = Id from [CodeAnalyzer].[CategoryDeclaration] where Name = @CategoryDeclaration_SpecialAttributes
select @CategoryDeclaration_SpacesVsTabs_Id               = Id from [CodeAnalyzer].[CategoryDeclaration] where Name = @CategoryDeclaration_SpacesVsTabs

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                         >> RULE DECLARATIONS <<                           ║
-- ╠═══════════════════════════════════════════════════════════════════════════╣    
-- ║                              "Try-Catches"                                ║    
-- ╚═══════════════════════════════════════════════════════════════════════════╝

declare @RuleDeclaration_ExpVsAppExp                              varchar(256)
declare @RuleDeclaration_ExpVsAppExp_Desc                         varchar(1024)
declare @RuleDeclaration_ExpVsAppExp_Expression                   varchar(1024)
declare @RuleDeclaration_ExpVsAppExp_Id                           int

declare @RuleDeclaration_NoExplicitExceptionIdentifier            varchar(256)
declare @RuleDeclaration_NoExplicitExceptionIdentifier_Desc       varchar(1024)
declare @RuleDeclaration_NoExplicitExceptionIdentifier_Expression varchar(1024)
declare @RuleDeclaration_NoExplicitExceptionIdentifier_Id         int

declare @RuleDeclaration_EmptyTryCatch                            varchar(256)
declare @RuleDeclaration_EmptyTryCatch_Desc                       varchar(1024)
declare @RuleDeclaration_EmptyTryCatch_Expression                 varchar(1024)
declare @RuleDeclaration_EmptyTryCatch_Id                         int

declare @RuleDeclaration_TryCatchVsThrowException                 varchar(256)
declare @RuleDeclaration_TryCatchVsThrowException_Desc            varchar(1024)
declare @RuleDeclaration_TryCatchVsThrowException_Expression      varchar(1024)
declare @RuleDeclaration_TryCatchVsThrowException_Id              int

set @RuleDeclaration_ExpVsAppExp                              = 'Exception vs. ApplicationException'
set @RuleDeclaration_ExpVsAppExp_Desc                         = 'Finds try-catch statements where the Exception class is used as the catch type identifier. If a general exception type should be used it should be the ApplicationException type and not the Exception type.'
set @RuleDeclaration_ExpVsAppExp_Expression                   = 'catch\s*\(\s*Exception\s*\w+\)'

set @RuleDeclaration_NoExplicitExceptionIdentifier            = 'No explicit exception identifier'
set @RuleDeclaration_NoExplicitExceptionIdentifier_Desc       = 'Finds try-catch statements where no explicit exception identifier has been added to the catch block.'
set @RuleDeclaration_NoExplicitExceptionIdentifier_Expression = 'catch\s*\(\s*Exception\s*\)'

set @RuleDeclaration_EmptyTryCatch                            = 'Empty try-catch'
set @RuleDeclaration_EmptyTryCatch_Desc                       = 'Finds empty try-catch statements.'
set @RuleDeclaration_EmptyTryCatch_Expression                 = 'catch\s*{\s*}'
														      
set @RuleDeclaration_TryCatchVsThrowException                 = 'Try-catch vs. throw exception'
set @RuleDeclaration_TryCatchVsThrowException_Desc            = 'Finds try-catch statements where the re-throwing of an exception deletes the original call stack.'
set @RuleDeclaration_TryCatchVsThrowException_Expression      = 'catch\s*\(\s*\w*Exception\s*(?<exceptionName>\w+)\s*\)\s*{\s*throw\s+\k<exceptionName>\s*;'


insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_ExpVsAppExp,                   @RuleDeclaration_ExpVsAppExp_Desc,                   @RuleSeverity_Fatal, @RuleDeclaration_ExpVsAppExp_Expression,                   @CategoryDeclaration_TryCatches_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NoExplicitExceptionIdentifier, @RuleDeclaration_NoExplicitExceptionIdentifier_Desc, @RuleSeverity_Fatal, @RuleDeclaration_NoExplicitExceptionIdentifier_Expression, @CategoryDeclaration_TryCatches_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_EmptyTryCatch,                 @RuleDeclaration_EmptyTryCatch_Desc,                 @RuleSeverity_Fatal, @RuleDeclaration_EmptyTryCatch_Expression,                 @CategoryDeclaration_TryCatches_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_TryCatchVsThrowException,      @RuleDeclaration_TryCatchVsThrowException_Desc,      @RuleSeverity_Fatal, @RuleDeclaration_TryCatchVsThrowException_Expression,      @CategoryDeclaration_TryCatches_Id);


select @RuleDeclaration_ExpVsAppExp_Id                   = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_ExpVsAppExp
select @RuleDeclaration_NoExplicitExceptionIdentifier_Id = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NoExplicitExceptionIdentifier
select @RuleDeclaration_EmptyTryCatch_Id                 = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_EmptyTryCatch
select @RuleDeclaration_TryCatchVsThrowException_Id      = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_TryCatchVsThrowException

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                         >> RULE DECLARATIONS <<                           ║
-- ╠═══════════════════════════════════════════════════════════════════════════╣    
-- ║                         "Static  constructions"                           ║    
-- ╚═══════════════════════════════════════════════════════════════════════════╝

declare @RuleDeclaration_StaticClasses               varchar(256)
declare @RuleDeclaration_StaticClasses_Desc          varchar(1024)
declare @RuleDeclaration_StaticClasses_Expression    varchar(1024)
declare @RuleDeclaration_StaticClasses_Id            int
												     
declare @RuleDeclaration_ClassVariables              varchar(256)
declare @RuleDeclaration_ClassVariables_Desc         varchar(1024)
declare @RuleDeclaration_ClassVariables_Expression   varchar(1024)
declare @RuleDeclaration_ClassVariables_Id           int

declare @RuleDeclaration_StaticProperties            varchar(256)
declare @RuleDeclaration_StaticProperties_Desc       varchar(1024)
declare @RuleDeclaration_StaticProperties_Expression varchar(1024)
declare @RuleDeclaration_StaticProperties_Id         int

declare @RuleDeclaration_StaticMethods               varchar(256)
declare @RuleDeclaration_StaticMethods_Desc          varchar(1024)
declare @RuleDeclaration_StaticMethods_Expression    varchar(1024)
declare @RuleDeclaration_StaticMethods_Id            int

set @RuleDeclaration_StaticClasses               = 'Static classes'
set @RuleDeclaration_StaticClasses_Desc          = 'Finds all classes that are declared static.'
set @RuleDeclaration_StaticClasses_Expression    = 'static\s+class\s+'
											     
set @RuleDeclaration_ClassVariables              = 'Class variables'
set @RuleDeclaration_ClassVariables_Desc         = 'Finds all class variables in the code base, ie. all class members defined static.'
set @RuleDeclaration_ClassVariables_Expression   = 'static\s+\w+\s+\w+\s*.*;'

set @RuleDeclaration_StaticProperties            = 'Static properties'
set @RuleDeclaration_StaticProperties_Desc       = 'Finds all properties that are declared static.'
set @RuleDeclaration_StaticProperties_Expression = 'static\s+\w+\s+\w+\s*{.*}'

set @RuleDeclaration_StaticMethods               = 'Static methods'
set @RuleDeclaration_StaticMethods_Desc          = 'Finds all methods that are declared static.'
set @RuleDeclaration_StaticMethods_Expression    = 'static\s+\w+\s+\w+\s*\(.*\)\s*{'


insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_StaticClasses,    @RuleDeclaration_StaticClasses_Desc,    @RuleSeverity_Critical, @RuleDeclaration_StaticClasses_Expression,    @CategoryDeclaration_StaticConstructions_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_ClassVariables,   @RuleDeclaration_ClassVariables_Desc,   @RuleSeverity_Critical, @RuleDeclaration_ClassVariables_Expression,   @CategoryDeclaration_StaticConstructions_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_StaticProperties, @RuleDeclaration_StaticProperties_Desc, @RuleSeverity_Critical, @RuleDeclaration_StaticProperties_Expression, @CategoryDeclaration_StaticConstructions_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_StaticMethods,    @RuleDeclaration_StaticMethods_Desc,    @RuleSeverity_Warning,  @RuleDeclaration_StaticMethods_Expression,    @CategoryDeclaration_StaticConstructions_Id);


select @RuleDeclaration_StaticClasses_Id    = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_StaticClasses
select @RuleDeclaration_ClassVariables_Id   = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_ClassVariables
select @RuleDeclaration_StaticProperties_Id = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_StaticProperties
select @RuleDeclaration_StaticMethods_Id    = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_StaticMethods

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                         >> RULE DECLARATIONS <<                           ║
-- ╠═══════════════════════════════════════════════════════════════════════════╣    
-- ║                                "Counters"                                 ║    
-- ╚═══════════════════════════════════════════════════════════════════════════╝

declare @RuleDeclaration_NumberOfPublicClasses                        varchar(256)
declare @RuleDeclaration_NumberOfPublicClasses_Desc                   varchar(1024)
declare @RuleDeclaration_NumberOfPublicClasses_Expression             varchar(1024)
declare @RuleDeclaration_NumberOfPublicClasses_Id                     int
															          
declare @RuleDeclaration_NumberOfInternalClasses                      varchar(256)
declare @RuleDeclaration_NumberOfInternalClasses_Desc                 varchar(1024)
declare @RuleDeclaration_NumberOfInternalClasses_Expression           varchar(1024)
declare @RuleDeclaration_NumberOfInternalClasses_Id                   int
															          
declare @RuleDeclaration_NumberOfPrivateClasses                       varchar(256)
declare @RuleDeclaration_NumberOfPrivateClasses_Desc                  varchar(1024)
declare @RuleDeclaration_NumberOfPrivateClasses_Expression            varchar(1024)
declare @RuleDeclaration_NumberOfPrivateClasses_Id                    int
															          
declare @RuleDeclaration_NumberOfPublicInterfaces                     varchar(256)
declare @RuleDeclaration_NumberOfPublicInterfaces_Desc                varchar(1024)
declare @RuleDeclaration_NumberOfPublicInterfaces_Expression          varchar(1024)
declare @RuleDeclaration_NumberOfPublicInterfaces_Id                  int
															          
declare @RuleDeclaration_NumberOfInternalInterfaces                   varchar(256)
declare @RuleDeclaration_NumberOfInternalInterfaces_Desc              varchar(1024)
declare @RuleDeclaration_NumberOfInternalInterfaces_Expression        varchar(1024)
declare @RuleDeclaration_NumberOfInternalInterfaces_Id                int
																      
declare @RuleDeclaration_NumberOfPublicAbstractClasses                varchar(256)
declare @RuleDeclaration_NumberOfPublicAbstractClasses_Desc           varchar(1024)
declare @RuleDeclaration_NumberOfPublicAbstractClasses_Expression     varchar(1024)
declare @RuleDeclaration_NumberOfPublicAbstractClasses_Id             int
																	  
declare @RuleDeclaration_NumberOfInternalAbstractClasses              varchar(256)
declare @RuleDeclaration_NumberOfInternalAbstractClasses_Desc         varchar(1024)
declare @RuleDeclaration_NumberOfInternalAbstractClasses_Expression   varchar(1024)
declare @RuleDeclaration_NumberOfInternalAbstractClasses_Id           int

declare @RuleDeclaration_NumberOfParallelForeachStatements            varchar(256)
declare @RuleDeclaration_NumberOfParallelForeachStatements_Desc       varchar(1024)
declare @RuleDeclaration_NumberOfParallelForeachStatements_Expression varchar(1024)
declare @RuleDeclaration_NumberOfParallelForeachStatements_Id         int

set @RuleDeclaration_NumberOfPublicClasses                        = 'Number of public classes'
set @RuleDeclaration_NumberOfPublicClasses_Desc                   = 'Finds all public class definitions.'
set @RuleDeclaration_NumberOfPublicClasses_Expression             = '\s*public\s*class\s*|\s*public\s*sealed\s*class\s*'
														          
set @RuleDeclaration_NumberOfInternalClasses                      = 'Number of internal classes'
set @RuleDeclaration_NumberOfInternalClasses_Desc                 = 'Finds all the internal class definitions.'
set @RuleDeclaration_NumberOfInternalClasses_Expression           = '\s*internal\s*class\s*|\s*internal\s*sealed\s*class\s*'
														          
set @RuleDeclaration_NumberOfPrivateClasses                       = 'Number of private classes'
set @RuleDeclaration_NumberOfPrivateClasses_Desc                  = 'Finds all private class definitions.'
set @RuleDeclaration_NumberOfPrivateClasses_Expression            = '\s*private\s*class\s*|\s*private\s*sealed\s*class\s*'
														          
set @RuleDeclaration_NumberOfPublicInterfaces                     = 'Number of public interfaces'
set @RuleDeclaration_NumberOfPublicInterfaces_Desc                = 'Finds all public interface definitions.'
set @RuleDeclaration_NumberOfPublicInterfaces_Expression          = 'public\s*interface\s*'
														          
set @RuleDeclaration_NumberOfInternalInterfaces                   = 'Number of internal interfaces'
set @RuleDeclaration_NumberOfInternalInterfaces_Desc              = 'Finds all internal interface definitions.'
set @RuleDeclaration_NumberOfInternalInterfaces_Expression        = 'internal\s*interface\s*'
															      
set @RuleDeclaration_NumberOfPublicAbstractClasses                = 'Number of public abstract classes'
set @RuleDeclaration_NumberOfPublicAbstractClasses_Desc           = 'Find all public abstract classes.'
set @RuleDeclaration_NumberOfPublicAbstractClasses_Expression     = '\s*public\s*abstract\s*class\s*'
																  
set @RuleDeclaration_NumberOfInternalAbstractClasses              = 'Number of internal abstract classes'
set @RuleDeclaration_NumberOfInternalAbstractClasses_Desc         = 'Find all abstract classes.'
set @RuleDeclaration_NumberOfInternalAbstractClasses_Expression   = '\s*internal\s*abstract\s*class\s*'

set @RuleDeclaration_NumberOfParallelForeachStatements            = 'Number of parallel foreach statements'
set @RuleDeclaration_NumberOfParallelForeachStatements_Desc       = 'Identify all placed in the code base where ''Parallel.ForEach'' loops are used.'
set @RuleDeclaration_NumberOfParallelForeachStatements_Expression = 'Parallel.ForEach'



insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NumberOfPublicClasses,             @RuleDeclaration_NumberOfPublicClasses_Desc,             @RuleSeverity_Info, @RuleDeclaration_NumberOfPublicClasses_Expression,             @CategoryDeclaration_Counters_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NumberOfInternalClasses,           @RuleDeclaration_NumberOfInternalClasses_Desc,           @RuleSeverity_Info, @RuleDeclaration_NumberOfInternalClasses_Expression,           @CategoryDeclaration_Counters_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NumberOfPrivateClasses,            @RuleDeclaration_NumberOfPrivateClasses_Desc,            @RuleSeverity_Info, @RuleDeclaration_NumberOfPrivateClasses_Expression,            @CategoryDeclaration_Counters_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NumberOfPublicInterfaces,          @RuleDeclaration_NumberOfPublicInterfaces_Desc,          @RuleSeverity_Info, @RuleDeclaration_NumberOfPublicInterfaces_Expression,          @CategoryDeclaration_Counters_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NumberOfInternalInterfaces,        @RuleDeclaration_NumberOfInternalInterfaces_Desc,        @RuleSeverity_Info, @RuleDeclaration_NumberOfInternalInterfaces_Expression,        @CategoryDeclaration_Counters_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NumberOfPublicAbstractClasses,     @RuleDeclaration_NumberOfPublicAbstractClasses_Desc,     @RuleSeverity_Info, @RuleDeclaration_NumberOfPublicAbstractClasses_Expression,     @CategoryDeclaration_Counters_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NumberOfInternalAbstractClasses,   @RuleDeclaration_NumberOfInternalAbstractClasses_Desc,   @RuleSeverity_Info, @RuleDeclaration_NumberOfInternalAbstractClasses_Expression,   @CategoryDeclaration_Counters_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NumberOfParallelForeachStatements, @RuleDeclaration_NumberOfParallelForeachStatements_Desc, @RuleSeverity_Info, @RuleDeclaration_NumberOfParallelForeachStatements_Expression, @CategoryDeclaration_Counters_Id);


select @RuleDeclaration_NumberOfPublicClasses_Id             = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NumberOfPublicClasses
select @RuleDeclaration_NumberOfInternalClasses_Id           = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NumberOfInternalClasses
select @RuleDeclaration_NumberOfPrivateClasses_Id            = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NumberOfPrivateClasses
select @RuleDeclaration_NumberOfPublicInterfaces_Id          = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NumberOfPublicInterfaces
select @RuleDeclaration_NumberOfInternalInterfaces_Id        = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NumberOfInternalInterfaces
select @RuleDeclaration_NumberOfPublicAbstractClasses_Id     = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NumberOfPublicAbstractClasses
select @RuleDeclaration_NumberOfInternalAbstractClasses_Id   = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NumberOfInternalAbstractClasses
select @RuleDeclaration_NumberOfParallelForeachStatements_Id = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NumberOfParallelForeachStatements

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                         >> RULE DECLARATIONS <<                           ║
-- ╠═══════════════════════════════════════════════════════════════════════════╣    
-- ║                     "Check basic file construction"                       ║    
-- ╚═══════════════════════════════════════════════════════════════════════════╝

declare @RuleDeclaration_UsingsVsNamespaceDeclaration                 varchar(256)
declare @RuleDeclaration_UsingsVsNamespaceDeclaration_Desc            varchar(1024)
declare @RuleDeclaration_UsingsVsNamespaceDeclaration_Expression      varchar(1024)
declare @RuleDeclaration_UsingsVsNamespaceDeclaration_Id              int

declare @RuleDeclaration_MissingNamespaceDeclarationInFile            varchar(256)
declare @RuleDeclaration_MissingNamespaceDeclarationInFile_Desc       varchar(1024)
declare @RuleDeclaration_MissingNamespaceDeclarationInFile_Expression varchar(1024)
declare @RuleDeclaration_MissingNamespaceDeclarationInFile_Id         int

set @RuleDeclaration_UsingsVsNamespaceDeclaration                 = 'Usings vs. namespace declaration'
set @RuleDeclaration_UsingsVsNamespaceDeclaration_Desc            = 'Look for all usings defined outside namespace.'
set @RuleDeclaration_UsingsVsNamespaceDeclaration_Expression      = 'using(?s).*namespace'
														          
set @RuleDeclaration_MissingNamespaceDeclarationInFile            = 'Missing namespace declaration in file'
set @RuleDeclaration_MissingNamespaceDeclarationInFile_Desc       = 'Identifies all C# files where the namespace declaration is missing.'
set @RuleDeclaration_MissingNamespaceDeclarationInFile_Expression = 'namespace\s*((\w+\.)|(\w+))+\s*{'
														          


insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_UsingsVsNamespaceDeclaration,      @RuleDeclaration_UsingsVsNamespaceDeclaration_Desc,      @RuleSeverity_Info,    @RuleDeclaration_UsingsVsNamespaceDeclaration_Expression,      @CategoryDeclaration_CheckBasicFileConstruction_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_MissingNamespaceDeclarationInFile, @RuleDeclaration_MissingNamespaceDeclarationInFile_Desc, @RuleSeverity_Warning, @RuleDeclaration_MissingNamespaceDeclarationInFile_Expression, @CategoryDeclaration_CheckBasicFileConstruction_Id);



select @RuleDeclaration_UsingsVsNamespaceDeclaration_Id      = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_UsingsVsNamespaceDeclaration
select @RuleDeclaration_MissingNamespaceDeclarationInFile_Id = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_MissingNamespaceDeclarationInFile

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                         >> RULE DECLARATIONS <<                           ║
-- ╠═══════════════════════════════════════════════════════════════════════════╣    
-- ║                         "Find special comments"                           ║    
-- ╚═══════════════════════════════════════════════════════════════════════════╝

declare @RuleDeclaration_ToDoComments            varchar(256)
declare @RuleDeclaration_ToDoComments_Desc       varchar(1024)
declare @RuleDeclaration_ToDoComments_Expression varchar(1024)
declare @RuleDeclaration_ToDoComments_Id         int

declare @RuleDeclaration_NoteComments            varchar(256)
declare @RuleDeclaration_NoteComments_Desc       varchar(1024)
declare @RuleDeclaration_NoteComments_Expression varchar(1024)
declare @RuleDeclaration_NoteComments_Id         int

set @RuleDeclaration_ToDoComments            = 'ToDo comments'
set @RuleDeclaration_ToDoComments_Desc       = 'Look for all ToDo comments in the code base.'
set @RuleDeclaration_ToDoComments_Expression = '//\s*todo'
									
set @RuleDeclaration_NoteComments            = 'Note comments'
set @RuleDeclaration_NoteComments_Desc       = 'Look for all ToDo comments in the code base.'
set @RuleDeclaration_NoteComments_Expression = '//\s*note'
														          

insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_ToDoComments, @RuleDeclaration_ToDoComments_Desc, @RuleSeverity_Info, @RuleDeclaration_ToDoComments_Expression, @CategoryDeclaration_FindSpecialComments_Id);
insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_NoteComments, @RuleDeclaration_NoteComments_Desc, @RuleSeverity_Info, @RuleDeclaration_NoteComments_Expression, @CategoryDeclaration_FindSpecialComments_Id);


select @RuleDeclaration_ToDoComments_Id = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_ToDoComments
select @RuleDeclaration_NoteComments_Id = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_NoteComments

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                         >> RULE DECLARATIONS <<                           ║
-- ╠═══════════════════════════════════════════════════════════════════════════╣    
-- ║                          "Special  attributes"                            ║    
-- ╚═══════════════════════════════════════════════════════════════════════════╝

declare @RuleDeclaration_InternalsVisibleTo            varchar(256)
declare @RuleDeclaration_InternalsVisibleTo_Desc       varchar(1024)
declare @RuleDeclaration_InternalsVisibleTo_Expression varchar(1024)
declare @RuleDeclaration_InternalsVisibleTo_Id         int

set @RuleDeclaration_InternalsVisibleTo            = 'InternalsVisibleTo'
set @RuleDeclaration_InternalsVisibleTo_Desc       = 'Look for all InternalsVisibleTo attributes in AssemblyInfo files.'
set @RuleDeclaration_InternalsVisibleTo_Expression = '\[assembly: InternalsVisibleTo\(\".*\"\)\]'
														          

insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_InternalsVisibleTo, @RuleDeclaration_InternalsVisibleTo_Desc, @RuleSeverity_Warning, @RuleDeclaration_InternalsVisibleTo_Expression, @CategoryDeclaration_SpecialAttributes_Id);


select @RuleDeclaration_InternalsVisibleTo_Id = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_InternalsVisibleTo

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                         >> RULE DECLARATIONS <<                           ║
-- ╠═══════════════════════════════════════════════════════════════════════════╣    
-- ║                            "Spaces vs. Tabs"                              ║    
-- ╚═══════════════════════════════════════════════════════════════════════════╝

declare @RuleDeclaration_SpacesForIndention            varchar(256)
declare @RuleDeclaration_SpacesForIndention_Desc       varchar(1024)
declare @RuleDeclaration_SpacesForIndention_Expression varchar(1024)
declare @RuleDeclaration_SpacesForIndention_Id         int

set @RuleDeclaration_SpacesForIndention            = 'Spaces for indention'
set @RuleDeclaration_SpacesForIndention_Desc       = 'Find spaces use for indention.'
set @RuleDeclaration_SpacesForIndention_Expression = '\n {2,}\w'
														          

insert into [CodeAnalyzer].[RuleDeclaration] (Name, Description, Severity, Expression, CategoryDeclaration_Id) values (@RuleDeclaration_SpacesForIndention, @RuleDeclaration_SpacesForIndention_Desc, @RuleSeverity_Info, @RuleDeclaration_SpacesForIndention_Expression, @CategoryDeclaration_SpacesVsTabs_Id);


select @RuleDeclaration_SpacesForIndention_Id = Id from [CodeAnalyzer].[RuleDeclaration] where Name = @RuleDeclaration_SpacesForIndention

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                                 >> USERS <<                               ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝

--   Declaring user data...
--   ───────────────────────────────────────────────────────────────────────────
declare @ProjectOwnerFirstName  varchar(30)
declare @ProjectOwnerMiddleName varchar(30)
declare @ProjectOwnerLastName   varchar(30)
declare @ProjectOwnerEmail      varchar(30)
declare @ProjectOwnerPassword   varchar(30)
declare @ProjectOwnerId         int

declare @ContributerFirstName  varchar(30)
declare @ContributerMiddleName varchar(30)
declare @ContributerLastName   varchar(30)
declare @ContributerEmail      varchar(30)
declare @ContributerPassword   varchar(30)
declare @ContributerId         int

declare @FollowerFirstName  varchar(30)
declare @FollowerMiddleName varchar(30)
declare @FollowerLastName   varchar(30)
declare @FollowerEmail      varchar(30)
declare @FollowerPassword   varchar(30)
declare @FollowerId         int

declare @FollowerFirstName2  varchar(30)
declare @FollowerMiddleName2 varchar(30)
declare @FollowerLastName2   varchar(30)
declare @FollowerEmail2      varchar(30)
declare @FollowerPassword2   varchar(30)
declare @FollowerId2         int

set @ProjectOwnerFirstName  = 'Claes'
set @ProjectOwnerMiddleName = ''
set @ProjectOwnerLastName   = 'Ryom'
set @ProjectOwnerEmail      = 'claesryom@gmail.com'
set @ProjectOwnerPassword   = 'claeser'

set @ContributerFirstName  = 'Kim'
set @ContributerMiddleName = 'Stig'
set @ContributerLastName   = 'Ryom'
set @ContributerEmail      = 'kim.ryom@gmail.com'
set @ContributerPassword   = 'kimmer'

set @FollowerFirstName  = 'Marianne'
set @FollowerMiddleName = ''
set @FollowerLastName   = 'Ryom'
set @FollowerEmail      = 'marianneryom@hotmail.com'
set @FollowerPassword   = 'Dulle'

set @FollowerFirstName2  = 'Lucas'
set @FollowerMiddleName2 = ''
set @FollowerLastName2   = 'Ryom'
set @FollowerEmail2      = 'lucasryom@gmail.com'
set @FollowerPassword2   = 'lucas1234'


--   Project owner(s)...
--   ───────────────────────────────────────────────────────────────────────────
insert into [CodeAnalyzer].[User] (FirstName, MiddleName, LastName, Email, Password) values (@ProjectOwnerFirstName, @ProjectOwnerMiddleName, @ProjectOwnerLastName, @ProjectOwnerEmail, @ProjectOwnerPassword);

--   Contributer(s)...
--   ───────────────────────────────────────────────────────────────────────────
insert into [CodeAnalyzer].[User] (FirstName, MiddleName, LastName, Email, Password) values (@ContributerFirstName, @ContributerMiddleName, @ContributerLastName, @ContributerEmail, @ContributerPassword);

--   Follower(s)...
--   ───────────────────────────────────────────────────────────────────────────
insert into [CodeAnalyzer].[User] (FirstName, MiddleName, LastName, Email, Password) values (@FollowerFirstName,  @FollowerMiddleName,  @FollowerLastName,  @FollowerEmail,  @FollowerPassword);
insert into [CodeAnalyzer].[User] (FirstName, MiddleName, LastName, Email, Password) values (@FollowerFirstName2, @FollowerMiddleName2, @FollowerLastName2, @FollowerEmail2, @FollowerPassword2);


--   Retrieve user Id's
--   ───────────────────────────────────────────────────────────────────────────
select @ProjectOwnerId = Id from [CodeAnalyzer].[User] where Email = @ProjectOwnerEmail
select @ContributerId  = Id from [CodeAnalyzer].[User] where Email = @ContributerEmail
select @FollowerId     = Id from [CodeAnalyzer].[User] where Email = @FollowerEmail
select @FollowerId2    = Id from [CodeAnalyzer].[User] where Email = @FollowerEmail2

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                             >> USER ROLES<<                               ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝

--   Project Administrator = '1', Contributer = '2', Follower = '3'...
--   ───────────────────────────────────────────────────────────────────────────
insert into [CodeAnalyzer].[UserRole] (Type, User_Id) values (1, @ProjectOwnerId); -- Administrator
insert into [CodeAnalyzer].[UserRole] (Type, User_Id) values (2, @ContributerId);  -- Contributer
insert into [CodeAnalyzer].[UserRole] (Type, User_Id) values (3, @FollowerId);     -- Follower

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                          >> PROJECT DEFINITIONS <<                        ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝
declare @ProjectDefinition_CodeAnalyser varchar(256)
declare @ProjectDefinition_CodeAnalyser_Id int

set @ProjectDefinition_CodeAnalyser = 'Code Analyser'


insert into [CodeAnalyzer].[ProjectDefinition] (Enabled, Name, Owner_Id, Contributers_Id, Followers_Id) values (1, @ProjectName, @ProjectOwnerId, @ContributerId, @ContributerId);


select @ProjectDefinition_CodeAnalyser_Id = Id from [CodeAnalyzer].[ProjectDefinition] where Name = @ProjectDefinition_CodeAnalyser

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║         >> FILES & DIRECTORIES FOR PROJECT "CODE ANALYSER" <<             ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝

--   Creating Directories...
--   ───────────────────────────────────────────────────────────────────────────
insert into [CodeAnalyzer].[Directory] (Enabled, Path, ProjectDefinition_Id)
values (1, 'C:\CLR\TFS\Tools\Code Analyzer\',            @ProjectDefinition_CodeAnalyser_Id),
       (0, 'C:\CLR\TFS\Tools\Code Analyzer\Statistics\', @ProjectDefinition_CodeAnalyser_Id),
       (0, 'C:\CLR\TFS\Tools\Code Analyzer\',                       @ProjectDefinition_CodeAnalyser_Id);

--   Creating Excluded Directories...
--   ───────────────────────────────────────────────────────────────────────────
insert into [CodeAnalyzer].[ExcludedDirectory] (Enabled, Path, ProjectDefinition_Id)
values (1, 'C:\CLR\TFS\Tools\Code Analyzer\Common\obj',        @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Configuration\obj', @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\ConsoleApp\obj',    @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Coordination\obj',  @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Engine\obj',        @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Libraries\obj',     @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Mediator\obj',      @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Output\obj',        @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Statistics\obj',    @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\UnitTest\obj',      @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Version\obj',       @ProjectDefinition_CodeAnalyser_Id);

--   Creating Files...
--   ───────────────────────────────────────────────────────────────────────────
insert into [CodeAnalyzer].[File] (Enabled, Path, ProjectDefinition_Id)
values (1, 'C:\CLR\TFS\Tools\Code Analyzer\Common\Properties\AssemblyInfo.cs',        @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Configuration\Properties\AssemblyInfo.cs', @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\ConsoleApp\Properties\AssemblyInfo.cs',    @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\Coordination\Properties\AssemblyInfo.cs',  @ProjectDefinition_CodeAnalyser_Id);

--   Creating Excluded Files...
--   ───────────────────────────────────────────────────────────────────────────
insert into [CodeAnalyzer].[ExcludedFile] (Enabled, Path, ProjectDefinition_Id)
values (1, 'C:\CLR\TFS\Tools\Code Analyzer\ngine\Properties\AssemblyInfo.cs',     @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\ibraries\Properties\AssemblyInfo.cs',  @ProjectDefinition_CodeAnalyser_Id),
	     (1, 'C:\CLR\TFS\Tools\Code Analyzer\ediator\Properties\AssemblyInfo.cs',   @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\utput\Properties\AssemblyInfo.cs',     @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\tatistics\Properties\AssemblyInfo.cs', @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\nitTest\Properties\AssemblyInfo.cs',   @ProjectDefinition_CodeAnalyser_Id),
       (1, 'C:\CLR\TFS\Tools\Code Analyzer\ersion\Properties\AssemblyInfo.cs',    @ProjectDefinition_CodeAnalyser_Id);

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                         >> CATEGORY DEFINITIONS <<                        ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝

insert into [CodeAnalyzer].[CategoryDefinition] (Enabled, ProjectDefinition_Id, CategoryDeclaration_Id)
values (1, @ProjectDefinition_CodeAnalyser_Id, @CategoryDeclaration_TryCatches_Id),
       (1, @ProjectDefinition_CodeAnalyser_Id, @CategoryDeclaration_StaticConstructions_Id),
       (1, @ProjectDefinition_CodeAnalyser_Id, @CategoryDeclaration_Counters_Id),
       (1, @ProjectDefinition_CodeAnalyser_Id, @CategoryDeclaration_CheckBasicFileConstruction_Id),
       (1, @ProjectDefinition_CodeAnalyser_Id, @CategoryDeclaration_FindSpecialComments_Id);

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                           >> RULE DEFINITIONS <<                          ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝

insert into [CodeAnalyzer].[RuleDefinition] (Enabled, CategoryDefinition_Id, RuleDeclaration_Id)
values (1, @CategoryDeclaration_TryCatches_Id, @RuleDeclaration_ExpVsAppExp_Id),
       (1, @CategoryDeclaration_TryCatches_Id, @RuleDeclaration_NoExplicitExceptionIdentifier_Id),
       (1, @CategoryDeclaration_TryCatches_Id, @RuleDeclaration_EmptyTryCatch_Id),
       (1, @CategoryDeclaration_TryCatches_Id, @RuleDeclaration_TryCatchVsThrowException_Id),
       
	   (1, @CategoryDeclaration_StaticConstructions_Id, @RuleDeclaration_StaticClasses_Id),
	   (1, @CategoryDeclaration_StaticConstructions_Id, @RuleDeclaration_ClassVariables_Id),
	   (1, @CategoryDeclaration_StaticConstructions_Id, @RuleDeclaration_StaticProperties_Id),
	   (1, @CategoryDeclaration_StaticConstructions_Id, @RuleDeclaration_StaticMethods_Id),
       
	   (1, @CategoryDeclaration_Counters_Id, @RuleDeclaration_NumberOfPublicClasses_Id),
	   (1, @CategoryDeclaration_Counters_Id, @RuleDeclaration_NumberOfPublicAbstractClasses_Id),
	   (1, @CategoryDeclaration_Counters_Id, @RuleDeclaration_NumberOfParallelForeachStatements_Id),
       
	   (1, @CategoryDeclaration_CheckBasicFileConstruction_Id, @RuleDeclaration_UsingsVsNamespaceDeclaration_Id),
	   (1, @CategoryDeclaration_CheckBasicFileConstruction_Id, @RuleDeclaration_MissingNamespaceDeclarationInFile_Id),
       
	   (1, @CategoryDeclaration_FindSpecialComments_Id, @RuleDeclaration_ToDoComments_Id),
	   (1, @CategoryDeclaration_FindSpecialComments_Id, @RuleDeclaration_NoteComments_Id);

-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                                  >> MATCH <<                              ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝

--declare @Batch_Id int
--set     @Batch_Id = 1

----insert into [CodeAnalyzer].[Batch] (TimeStamp) values (GetDate());

----select @Batch_Id = Id from [CodeAnalyzer].[Batch] where Id = 1


--insert into [CodeAnalyzer].[Match] (LineNumber, CodeExtract, RootDirectoryPath, Filename, Batch_Id)
--values (1234, 'This is a code extract, right!!! :-)', 'c:\', 'c:\filename.txt', @Batch_Id);


-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝







-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ║                 >> LanguageDeclarationRuleDeclaration <<                  ║
-- ╚═══════════════════════════════════════════════════════════════════════════╝

insert into [CodeAnalyzer].[LanguageDeclarationRuleDeclaration] (LanguageDeclaration_Id, Rules_Id)
values (@LanguageCSharp_Id, @RuleDeclaration_ExpVsAppExp_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NoExplicitExceptionIdentifier_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_EmptyTryCatch_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_TryCatchVsThrowException_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_StaticClasses_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_ClassVariables_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_StaticProperties_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_StaticMethods_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NumberOfPublicClasses_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NumberOfInternalClasses_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NumberOfPrivateClasses_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NumberOfPublicInterfaces_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NumberOfInternalInterfaces_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NumberOfPublicAbstractClasses_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NumberOfInternalAbstractClasses_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NumberOfParallelForeachStatements_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_UsingsVsNamespaceDeclaration_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_MissingNamespaceDeclarationInFile_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_ToDoComments_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_NoteComments_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_InternalsVisibleTo_Id),
       (@LanguageCSharp_Id, @RuleDeclaration_SpacesForIndention_Id);


-- ╔═══════════════════════════════════════════════════════════════════════════╗
-- ╚═══════════════════════════════════════════════════════════════════════════╝



-- ALT + 179 => '│'   ┌───┬───┐
-- ALT + 180 => '┤'	  │   │   │
-- ALT + 191 => '┐'   │   │   │
-- ALT + 192 => '└'   │   │   │
-- ALT + 193 => '┴'   │   │   │
-- ALT + 194 => '┬'	  ├───┼───┤	  
-- ALT + 195 => '├'   │   │   │
-- ALT + 196 => '─'   │   │   │
-- ALT + 197 => '┼'   │   │   │
-- ALT + 217 => '┘'   │   │   │
-- ALT + 218 => '┌'	  └───┴───┘

-- ALT + 185 => '╣'   ╔═══╦═══╗
-- ALT + 186 => '║'	  ║   ║   ║
-- ALT + 187 => '╗'	  ║   ║   ║
-- ALT + 188 => '╝'   ║   ║   ║
-- ALT + 200 => '╚'	  ║   ║   ║
-- ALT + 201 => '╔'	  ╠═══╬═══╣
-- ALT + 202 => '╩'	  ║   ║   ║
-- ALT + 203 => '╦'	  ║   ║   ║	  
-- ALT + 204 => '╠'	  ║   ║   ║
-- ALT + 205 => '═'	  ║   ║   ║
-- ALT + 206 => '╬'   ╚═══╩═══╝