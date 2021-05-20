
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/09/2014 00:25:26
-- Generated from EDMX file: C:\Project\Hyundai\Initial\HyundaiPortal.Business\Hyundai.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_9B2D14_hyundai];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_R_11]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[INVOICEHEADER] DROP CONSTRAINT [FK_R_11];
GO
IF OBJECT_ID(N'[dbo].[FK_R_12]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[INVOICEDETAIL] DROP CONSTRAINT [FK_R_12];
GO
IF OBJECT_ID(N'[dbo].[FK_R_14]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OTHERCHARGE] DROP CONSTRAINT [FK_R_14];
GO
IF OBJECT_ID(N'[dbo].[FK_R_16]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PRODUCTITEM] DROP CONSTRAINT [FK_R_16];
GO
IF OBJECT_ID(N'[dbo].[FK_R_18]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CUSTOMER] DROP CONSTRAINT [FK_R_18];
GO
IF OBJECT_ID(N'[dbo].[FK_R_19]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HBL] DROP CONSTRAINT [FK_R_19];
GO
IF OBJECT_ID(N'[dbo].[FK_R_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HBL] DROP CONSTRAINT [FK_R_2];
GO
IF OBJECT_ID(N'[dbo].[FK_R_21]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HBL] DROP CONSTRAINT [FK_R_21];
GO
IF OBJECT_ID(N'[dbo].[FK_R_22]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HBL] DROP CONSTRAINT [FK_R_22];
GO
IF OBJECT_ID(N'[dbo].[FK_R_23]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HBL] DROP CONSTRAINT [FK_R_23];
GO
IF OBJECT_ID(N'[dbo].[FK_R_24]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HBL] DROP CONSTRAINT [FK_R_24];
GO
IF OBJECT_ID(N'[dbo].[FK_R_25]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RATE] DROP CONSTRAINT [FK_R_25];
GO
IF OBJECT_ID(N'[dbo].[FK_R_27]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MBL] DROP CONSTRAINT [FK_R_27];
GO
IF OBJECT_ID(N'[dbo].[FK_R_28]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MBL] DROP CONSTRAINT [FK_R_28];
GO
IF OBJECT_ID(N'[dbo].[FK_R_29]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[USER] DROP CONSTRAINT [FK_R_29];
GO
IF OBJECT_ID(N'[dbo].[FK_R_30]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RATE] DROP CONSTRAINT [FK_R_30];
GO
IF OBJECT_ID(N'[dbo].[FK_R_31]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[USER] DROP CONSTRAINT [FK_R_31];
GO
IF OBJECT_ID(N'[dbo].[FK_R_33]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HBL] DROP CONSTRAINT [FK_R_33];
GO
IF OBJECT_ID(N'[dbo].[FK_R_34]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MBL] DROP CONSTRAINT [FK_R_34];
GO
IF OBJECT_ID(N'[dbo].[FK_R_35]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MBL] DROP CONSTRAINT [FK_R_35];
GO
IF OBJECT_ID(N'[dbo].[FK_R_5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[USER] DROP CONSTRAINT [FK_R_5];
GO
IF OBJECT_ID(N'[dbo].[FK_R_6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RATE] DROP CONSTRAINT [FK_R_6];
GO
IF OBJECT_ID(N'[dbo].[FK_R_9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HBL] DROP CONSTRAINT [FK_R_9];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CODE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CODE];
GO
IF OBJECT_ID(N'[dbo].[CUSTOMER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CUSTOMER];
GO
IF OBJECT_ID(N'[dbo].[HBL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HBL];
GO
IF OBJECT_ID(N'[dbo].[HBLNO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HBLNO];
GO
IF OBJECT_ID(N'[dbo].[INVOICEDETAIL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[INVOICEDETAIL];
GO
IF OBJECT_ID(N'[dbo].[INVOICEHEADER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[INVOICEHEADER];
GO
IF OBJECT_ID(N'[dbo].[MBL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MBL];
GO
IF OBJECT_ID(N'[dbo].[OTHERCHARGE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OTHERCHARGE];
GO
IF OBJECT_ID(N'[dbo].[PRODUCTITEM]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PRODUCTITEM];
GO
IF OBJECT_ID(N'[dbo].[RATE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RATE];
GO
IF OBJECT_ID(N'[dbo].[USER]', 'U') IS NOT NULL
    DROP TABLE [dbo].[USER];
GO
IF OBJECT_ID(N'[HyundaiModelStoreContainer].[ZIPCODE]', 'U') IS NOT NULL
    DROP TABLE [HyundaiModelStoreContainer].[ZIPCODE];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CODE'
CREATE TABLE [dbo].[CODE] (
    [cdidx] int IDENTITY(1,1) NOT NULL,
    [GROUPCD] int  NULL,
    [CD] varchar(10)  NULL,
    [CDNAME] nvarchar(30)  NULL,
    [REMARK] nvarchar(50)  NULL,
    [SORT] smallint  NULL
);
GO

-- Creating table 'CUSTOMER'
CREATE TABLE [dbo].[CUSTOMER] (
    [cidx] int IDENTITY(1,1) NOT NULL,
    [CustName] nvarchar(30)  NULL,
    [CustAddress1] nvarchar(50)  NULL,
    [CustAddress2] nvarchar(50)  NULL,
    [CustCity] nvarchar(30)  NULL,
    [CustState] nvarchar(20)  NULL,
    [CustZip] varchar(10)  NULL,
    [OwnerName] nvarchar(20)  NULL,
    [TaxId] varchar(11)  NULL,
    [WPhone] varchar(20)  NULL,
    [Fax] varchar(20)  NULL,
    [CPhone] varchar(20)  NULL,
    [SpecialClearanceNo] varchar(20)  NULL,
    [CustomerType] int  NULL,
    [CustEngName] varchar(30)  NULL,
    [CustomerCd] varchar(10)  NULL,
    [isDelete] bit  NOT NULL,
    [CustFullAddress] varchar(max)  NULL
);
GO

-- Creating table 'HBL'
CREATE TABLE [dbo].[HBL] (
    [hidx] int IDENTITY(1,1) NOT NULL,
    [midx] int  NULL,
    [Status] int  NULL,
    [cidx] int  NULL,
    [MblNo] varchar(14)  NULL,
    [HblNo] varchar(20)  NULL,
    [OnBoardDate] datetime  NULL,
    [ShipperCd] varchar(20)  NULL,
    [ShipperName] nvarchar(50)  NULL,
    [ShipperPhone] varchar(20)  NULL,
    [ShipperAddress] varchar(100)  NULL,
    [RefNo] varchar(20)  NULL,
    [ConsigneeName] nvarchar(30)  NULL,
    [ConsigneePhone] varchar(20)  NULL,
    [ConsigneeCellPhone] varchar(20)  NULL,
    [ShipperZipCode] varchar(5)  NULL,
    [ShipperZipAddress] nvarchar(100)  NULL,
    [ConsigneeAddress] nvarchar(100)  NULL,
    [EngZipaddress] varchar(150)  NULL,
    [EngAddress] varchar(150)  NULL,
    [juminNo] varchar(14)  NULL,
    [Memo] nvarchar(max)  NULL,
    [ConsigneeType] int  NULL,
    [Carton] int  NULL,
    [IsGeneralClearance] int  NULL,
    [ShipperEngName] varchar(50)  NULL,
    [TransportType] int  NULL,
    [WeightType] int  NULL,
    [Weight] decimal(19,4)  NULL,
    [listClearanceCode] varchar(20)  NULL,
    [SpecialClearanceNo] varchar(20)  NULL,
    [CreateId] int  NULL,
    [CreateDate] datetime  NULL,
    [ShipperCity] varchar(30)  NULL,
    [ShipperState] varchar(20)  NULL,
    [ConsigneeZipCode] varchar(7)  NULL,
    [ConsigneeZipAddress] nvarchar(100)  NULL,
    [ConsigneeEngName] varchar(30)  NULL,
    [value] decimal(18,0)  NULL
);
GO

-- Creating table 'HBLNO'
CREATE TABLE [dbo].[HBLNO] (
    [HBLNO1] char(18)  NOT NULL,
    [ISACTIVATE] char(18)  NULL
);
GO

-- Creating table 'INVOICEDETAIL'
CREATE TABLE [dbo].[INVOICEDETAIL] (
    [InvoiceNo] int  NOT NULL,
    [hidx] char(18)  NOT NULL
);
GO

-- Creating table 'INVOICEHEADER'
CREATE TABLE [dbo].[INVOICEHEADER] (
    [cidx] int  NULL,
    [InvoiceNo] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'OTHERCHARGE'
CREATE TABLE [dbo].[OTHERCHARGE] (
    [midx] int  NOT NULL,
    [chargeCd] varchar(10)  NULL,
    [ChargeAmt] decimal(19,4)  NULL,
    [ocidx] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'PRODUCTITEM'
CREATE TABLE [dbo].[PRODUCTITEM] (
    [pidx] int IDENTITY(1,1) NOT NULL,
    [itemName] varchar(200)  NULL,
    [itemAmt] decimal(19,4)  NULL,
    [itemQty] smallint  NULL,
    [ItemTotalAmt] decimal(19,4)  NULL,
    [hidx] int  NULL,
    [itemBrand] varchar(20)  NULL,
    [url] varchar(200)  NULL
);
GO

-- Creating table 'RATE'
CREATE TABLE [dbo].[RATE] (
    [ridx] int IDENTITY(1,1) NOT NULL,
    [cidx] int  NULL,
    [applyDate] datetime  NULL,
    [baseRate] decimal(19,4)  NULL,
    [WeightType] int  NULL,
    [extraRate] decimal(19,4)  NULL,
    [RegId] int  NULL,
    [RegDate] datetime  NULL
);
GO

-- Creating table 'USER'
CREATE TABLE [dbo].[USER] (
    [uidx] int IDENTITY(1,1) NOT NULL,
    [userid] varchar(20)  NULL,
    [password] varchar(20)  NULL,
    [cidx] int  NULL,
    [UserName] nvarchar(20)  NULL,
    [UserEmail] varchar(50)  NULL,
    [RegId] int  NULL,
    [RegDate] datetime  NULL,
    [UserType] int  NULL,
    [isDelete] bit  NOT NULL
);
GO

-- Creating table 'ZIPCODE'
CREATE TABLE [dbo].[ZIPCODE] (
    [Zipcode1] varchar(7)  NULL,
    [Address_Kor] nvarchar(300)  NULL,
    [Address_Eng] varchar(300)  NULL,
    [ZipCodeID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'MBL'
CREATE TABLE [dbo].[MBL] (
    [midx] int IDENTITY(1,1) NOT NULL,
    [mblNo] char(14)  NULL,
    [status] int  NULL,
    [ShipperCd] int  NULL,
    [ShipperName] varchar(50)  NULL,
    [ShipperAddress1] varchar(50)  NULL,
    [ShipperAddress2] varchar(50)  NULL,
    [ShipperCity] varchar(30)  NULL,
    [ShipperState] varchar(20)  NULL,
    [ShipperZip] varchar(10)  NULL,
    [CneeCd] int  NULL,
    [CneeName] varchar(50)  NULL,
    [CneeAddress1] varchar(50)  NULL,
    [CneeAddress2] varchar(50)  NULL,
    [CneeCity] varchar(30)  NULL,
    [CneeState] varchar(20)  NULL,
    [CneeZip] varchar(10)  NULL,
    [DptrCd] char(3)  NULL,
    [DptrName] varchar(30)  NULL,
    [DestCd] char(3)  NULL,
    [DestName] varchar(30)  NULL,
    [FltNo] varchar(10)  NULL,
    [To1] varchar(10)  NULL,
    [By1] varchar(10)  NULL,
    [To2] varchar(10)  NULL,
    [By2] varchar(10)  NULL,
    [PKGS] int  NULL,
    [GrossWT] decimal(19,4)  NULL,
    [WTType] bit  NULL,
    [RateClass] char(1)  NULL,
    [VWT] decimal(19,4)  NULL,
    [CWT] decimal(19,4)  NULL,
    [RateChange] decimal(19,4)  NULL,
    [FltTotalAmt] decimal(19,4)  NULL,
    [AccountingInfo] varchar(200)  NULL,
    [HandlingInfo] varchar(200)  NULL,
    [SignatureOfShipperOrAgent] varchar(100)  NULL,
    [NatureQuantityods] varchar(1000)  NULL,
    [ByFirstCarrier] varchar(10)  NULL,
    [IssuingCarrierName] varchar(50)  NULL,
    [IATA] varchar(20)  NULL,
    [AccountNo] varchar(20)  NULL,
    [FltDate] datetime  NULL,
    [NotNetiableCustCd] int  NULL,
    [Currency] char(3)  NULL,
    [ChagsCode] varchar(5)  NULL,
    [WTVALPPD] char(2)  NULL,
    [WTVALCOL] char(2)  NULL,
    [OTHERPPD] char(2)  NULL,
    [OTHERCOL] char(2)  NULL,
    [DeclaredValueForCarriage] varchar(10)  NULL,
    [DeclaredValueForCustoms] varchar(10)  NULL,
    [TotalAmt] decimal(19,4)  NULL,
    [SignatureOfIssuingCarrierOrAgent] varchar(100)  NULL,
    [CreateId] int  NULL,
    [CreateDate] datetime  NULL,
    [ArrivalDate] datetime  NULL,
    [CneePhone] varchar(20)  NULL,
    [ShipperFullAddress] varchar(max)  NULL,
    [CneeFullAddress] varchar(max)  NULL,
    [NotNetiableText] varchar(max)  NULL,
    [value] decimal(19,4)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [cdidx] in table 'CODE'
ALTER TABLE [dbo].[CODE]
ADD CONSTRAINT [PK_CODE]
    PRIMARY KEY CLUSTERED ([cdidx] ASC);
GO

-- Creating primary key on [cidx] in table 'CUSTOMER'
ALTER TABLE [dbo].[CUSTOMER]
ADD CONSTRAINT [PK_CUSTOMER]
    PRIMARY KEY CLUSTERED ([cidx] ASC);
GO

-- Creating primary key on [hidx] in table 'HBL'
ALTER TABLE [dbo].[HBL]
ADD CONSTRAINT [PK_HBL]
    PRIMARY KEY CLUSTERED ([hidx] ASC);
GO

-- Creating primary key on [HBLNO1] in table 'HBLNO'
ALTER TABLE [dbo].[HBLNO]
ADD CONSTRAINT [PK_HBLNO]
    PRIMARY KEY CLUSTERED ([HBLNO1] ASC);
GO

-- Creating primary key on [InvoiceNo], [hidx] in table 'INVOICEDETAIL'
ALTER TABLE [dbo].[INVOICEDETAIL]
ADD CONSTRAINT [PK_INVOICEDETAIL]
    PRIMARY KEY CLUSTERED ([InvoiceNo], [hidx] ASC);
GO

-- Creating primary key on [InvoiceNo] in table 'INVOICEHEADER'
ALTER TABLE [dbo].[INVOICEHEADER]
ADD CONSTRAINT [PK_INVOICEHEADER]
    PRIMARY KEY CLUSTERED ([InvoiceNo] ASC);
GO

-- Creating primary key on [ocidx] in table 'OTHERCHARGE'
ALTER TABLE [dbo].[OTHERCHARGE]
ADD CONSTRAINT [PK_OTHERCHARGE]
    PRIMARY KEY CLUSTERED ([ocidx] ASC);
GO

-- Creating primary key on [pidx] in table 'PRODUCTITEM'
ALTER TABLE [dbo].[PRODUCTITEM]
ADD CONSTRAINT [PK_PRODUCTITEM]
    PRIMARY KEY CLUSTERED ([pidx] ASC);
GO

-- Creating primary key on [ridx] in table 'RATE'
ALTER TABLE [dbo].[RATE]
ADD CONSTRAINT [PK_RATE]
    PRIMARY KEY CLUSTERED ([ridx] ASC);
GO

-- Creating primary key on [uidx] in table 'USER'
ALTER TABLE [dbo].[USER]
ADD CONSTRAINT [PK_USER]
    PRIMARY KEY CLUSTERED ([uidx] ASC);
GO

-- Creating primary key on [ZipCodeID] in table 'ZIPCODE'
ALTER TABLE [dbo].[ZIPCODE]
ADD CONSTRAINT [PK_ZIPCODE]
    PRIMARY KEY CLUSTERED ([ZipCodeID] ASC);
GO

-- Creating primary key on [midx] in table 'MBL'
ALTER TABLE [dbo].[MBL]
ADD CONSTRAINT [PK_MBL]
    PRIMARY KEY CLUSTERED ([midx] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerType] in table 'CUSTOMER'
ALTER TABLE [dbo].[CUSTOMER]
ADD CONSTRAINT [FK_R_18]
    FOREIGN KEY ([CustomerType])
    REFERENCES [dbo].[CODE]
        ([cdidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_18'
CREATE INDEX [IX_FK_R_18]
ON [dbo].[CUSTOMER]
    ([CustomerType]);
GO

-- Creating foreign key on [Status] in table 'HBL'
ALTER TABLE [dbo].[HBL]
ADD CONSTRAINT [FK_R_19]
    FOREIGN KEY ([Status])
    REFERENCES [dbo].[CODE]
        ([cdidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_19'
CREATE INDEX [IX_FK_R_19]
ON [dbo].[HBL]
    ([Status]);
GO

-- Creating foreign key on [ConsigneeType] in table 'HBL'
ALTER TABLE [dbo].[HBL]
ADD CONSTRAINT [FK_R_21]
    FOREIGN KEY ([ConsigneeType])
    REFERENCES [dbo].[CODE]
        ([cdidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_21'
CREATE INDEX [IX_FK_R_21]
ON [dbo].[HBL]
    ([ConsigneeType]);
GO

-- Creating foreign key on [TransportType] in table 'HBL'
ALTER TABLE [dbo].[HBL]
ADD CONSTRAINT [FK_R_22]
    FOREIGN KEY ([TransportType])
    REFERENCES [dbo].[CODE]
        ([cdidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_22'
CREATE INDEX [IX_FK_R_22]
ON [dbo].[HBL]
    ([TransportType]);
GO

-- Creating foreign key on [WeightType] in table 'HBL'
ALTER TABLE [dbo].[HBL]
ADD CONSTRAINT [FK_R_23]
    FOREIGN KEY ([WeightType])
    REFERENCES [dbo].[CODE]
        ([cdidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_23'
CREATE INDEX [IX_FK_R_23]
ON [dbo].[HBL]
    ([WeightType]);
GO

-- Creating foreign key on [IsGeneralClearance] in table 'HBL'
ALTER TABLE [dbo].[HBL]
ADD CONSTRAINT [FK_R_24]
    FOREIGN KEY ([IsGeneralClearance])
    REFERENCES [dbo].[CODE]
        ([cdidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_24'
CREATE INDEX [IX_FK_R_24]
ON [dbo].[HBL]
    ([IsGeneralClearance]);
GO

-- Creating foreign key on [WeightType] in table 'RATE'
ALTER TABLE [dbo].[RATE]
ADD CONSTRAINT [FK_R_25]
    FOREIGN KEY ([WeightType])
    REFERENCES [dbo].[CODE]
        ([cdidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_25'
CREATE INDEX [IX_FK_R_25]
ON [dbo].[RATE]
    ([WeightType]);
GO

-- Creating foreign key on [UserType] in table 'USER'
ALTER TABLE [dbo].[USER]
ADD CONSTRAINT [FK_R_29]
    FOREIGN KEY ([UserType])
    REFERENCES [dbo].[CODE]
        ([cdidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_29'
CREATE INDEX [IX_FK_R_29]
ON [dbo].[USER]
    ([UserType]);
GO

-- Creating foreign key on [cidx] in table 'INVOICEHEADER'
ALTER TABLE [dbo].[INVOICEHEADER]
ADD CONSTRAINT [FK_R_11]
    FOREIGN KEY ([cidx])
    REFERENCES [dbo].[CUSTOMER]
        ([cidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_11'
CREATE INDEX [IX_FK_R_11]
ON [dbo].[INVOICEHEADER]
    ([cidx]);
GO

-- Creating foreign key on [cidx] in table 'USER'
ALTER TABLE [dbo].[USER]
ADD CONSTRAINT [FK_R_5]
    FOREIGN KEY ([cidx])
    REFERENCES [dbo].[CUSTOMER]
        ([cidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_5'
CREATE INDEX [IX_FK_R_5]
ON [dbo].[USER]
    ([cidx]);
GO

-- Creating foreign key on [cidx] in table 'RATE'
ALTER TABLE [dbo].[RATE]
ADD CONSTRAINT [FK_R_6]
    FOREIGN KEY ([cidx])
    REFERENCES [dbo].[CUSTOMER]
        ([cidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_6'
CREATE INDEX [IX_FK_R_6]
ON [dbo].[RATE]
    ([cidx]);
GO

-- Creating foreign key on [cidx] in table 'HBL'
ALTER TABLE [dbo].[HBL]
ADD CONSTRAINT [FK_R_9]
    FOREIGN KEY ([cidx])
    REFERENCES [dbo].[CUSTOMER]
        ([cidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_9'
CREATE INDEX [IX_FK_R_9]
ON [dbo].[HBL]
    ([cidx]);
GO

-- Creating foreign key on [hidx] in table 'PRODUCTITEM'
ALTER TABLE [dbo].[PRODUCTITEM]
ADD CONSTRAINT [FK_R_16]
    FOREIGN KEY ([hidx])
    REFERENCES [dbo].[HBL]
        ([hidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_16'
CREATE INDEX [IX_FK_R_16]
ON [dbo].[PRODUCTITEM]
    ([hidx]);
GO

-- Creating foreign key on [CreateId] in table 'HBL'
ALTER TABLE [dbo].[HBL]
ADD CONSTRAINT [FK_R_33]
    FOREIGN KEY ([CreateId])
    REFERENCES [dbo].[USER]
        ([uidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_33'
CREATE INDEX [IX_FK_R_33]
ON [dbo].[HBL]
    ([CreateId]);
GO

-- Creating foreign key on [InvoiceNo] in table 'INVOICEDETAIL'
ALTER TABLE [dbo].[INVOICEDETAIL]
ADD CONSTRAINT [FK_R_12]
    FOREIGN KEY ([InvoiceNo])
    REFERENCES [dbo].[INVOICEHEADER]
        ([InvoiceNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RegId] in table 'RATE'
ALTER TABLE [dbo].[RATE]
ADD CONSTRAINT [FK_R_30]
    FOREIGN KEY ([RegId])
    REFERENCES [dbo].[USER]
        ([uidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_30'
CREATE INDEX [IX_FK_R_30]
ON [dbo].[RATE]
    ([RegId]);
GO

-- Creating foreign key on [RegId] in table 'USER'
ALTER TABLE [dbo].[USER]
ADD CONSTRAINT [FK_R_31]
    FOREIGN KEY ([RegId])
    REFERENCES [dbo].[USER]
        ([uidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_31'
CREATE INDEX [IX_FK_R_31]
ON [dbo].[USER]
    ([RegId]);
GO

-- Creating foreign key on [status] in table 'MBL'
ALTER TABLE [dbo].[MBL]
ADD CONSTRAINT [FK_R_35]
    FOREIGN KEY ([status])
    REFERENCES [dbo].[CODE]
        ([cdidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_35'
CREATE INDEX [IX_FK_R_35]
ON [dbo].[MBL]
    ([status]);
GO

-- Creating foreign key on [ShipperCd] in table 'MBL'
ALTER TABLE [dbo].[MBL]
ADD CONSTRAINT [FK_R_27]
    FOREIGN KEY ([ShipperCd])
    REFERENCES [dbo].[CUSTOMER]
        ([cidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_27'
CREATE INDEX [IX_FK_R_27]
ON [dbo].[MBL]
    ([ShipperCd]);
GO

-- Creating foreign key on [CneeCd] in table 'MBL'
ALTER TABLE [dbo].[MBL]
ADD CONSTRAINT [FK_R_28]
    FOREIGN KEY ([CneeCd])
    REFERENCES [dbo].[CUSTOMER]
        ([cidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_28'
CREATE INDEX [IX_FK_R_28]
ON [dbo].[MBL]
    ([CneeCd]);
GO

-- Creating foreign key on [midx] in table 'HBL'
ALTER TABLE [dbo].[HBL]
ADD CONSTRAINT [FK_R_2]
    FOREIGN KEY ([midx])
    REFERENCES [dbo].[MBL]
        ([midx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_2'
CREATE INDEX [IX_FK_R_2]
ON [dbo].[HBL]
    ([midx]);
GO

-- Creating foreign key on [midx] in table 'OTHERCHARGE'
ALTER TABLE [dbo].[OTHERCHARGE]
ADD CONSTRAINT [FK_R_14]
    FOREIGN KEY ([midx])
    REFERENCES [dbo].[MBL]
        ([midx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_14'
CREATE INDEX [IX_FK_R_14]
ON [dbo].[OTHERCHARGE]
    ([midx]);
GO

-- Creating foreign key on [CreateId] in table 'MBL'
ALTER TABLE [dbo].[MBL]
ADD CONSTRAINT [FK_R_34]
    FOREIGN KEY ([CreateId])
    REFERENCES [dbo].[USER]
        ([uidx])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_34'
CREATE INDEX [IX_FK_R_34]
ON [dbo].[MBL]
    ([CreateId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------