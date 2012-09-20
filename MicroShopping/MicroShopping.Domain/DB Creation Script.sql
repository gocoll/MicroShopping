/*
	Author: Sergio Tapia
	Copyright: 2012 Sergio Tapia
	License: MIT License

	Copyright (c) 2012 Sergio Tapia

	Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
	and associated documentation files (the "Software"), to deal in the Software without restriction, 
	including without limitation the rights to use, copy, modify, merge, publish, distribute, 
	sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
	furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in all copies or 
	substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
	BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
	NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
	DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

	------------------------------------------------------------------------------

	This is the main database creation schema for Microsoft SQL Server 2008 and up.
	Run this complete script against a fresh database and configure your connection 
	string to connect to it in order for MicroShopping to work. 
*/

create table Gender
(
	GenderId int primary key identity(1,1),
	Name nvarchar(32)
)
 
create table UserRole
(
	UserRoleId int primary key identity(1,1),
	Name nvarchar(32)
)
 
create table [User]
(
	UserId int primary key identity(1,1),
	UserRoleId int foreign key references UserRole(UserRoleId),
	Name nvarchar(64),
	AvatarUrl nvarchar(1024),
	Lastname nvarchar(64),
	GenderId int foreign key references Gender(GenderId),
	Email nvarchar(512),
	Telephone nvarchar(512),
	MobilePhone nvarchar(512),
	[Address] nvarchar(512),
	Carnet nvarchar(512),
	DateOfBirth datetime,
	DateOfRegistry datetime,
	LastDateLogin datetime,
	IsActive bit,
	LanceCreditBalance int,
	LancesSpent int,
	[Password] nvarchar(64),
	Nickname nvarchar(64),
	EmailVerificationCode nvarchar(64)
)
 
create table ProductBrand
(
	ProductBrandId int primary key identity(1,1),
	Name nvarchar(64)
)
 
create table ProductCategory
(
	ProductCategoryId int primary key identity(1,1),
	Name nvarchar(64)
)
 
create table Product
(
	ProductId int primary key identity(1,1),
	ProductBrandId int foreign key references ProductBrand(ProductBrandId),
	ProductCategoryId int foreign key references ProductCategory(ProductCategoryId),
	Name nvarchar(64),
	Description nvarchar(4000),
	WeightInMilligrams decimal(16,2),
	SuggestedPrice decimal(16,2)
)
 
create table ProductPictures
(
	ProductPicturesId int primary key identity(1,1),
	ProductId int foreign key references Product(ProductId),
	ImageUrl nvarchar(1024)
)
 
create table AuctionCategory
(
	AuctionCategoryId int primary key identity(1,1),
	Name nvarchar(64)
)
 
create table Auction
(
	AuctionId int primary key identity(1,1),
	ProductId int foreign key references Product(ProductId),
	AuctionCategoryId int foreign key references AuctionCategory(AuctionCategoryId),
	SerialNumber nvarchar(1024),
	IsActive bit,
	StartTime datetime,
	EndTime datetime,
	LastBidTime datetime,
	AvailableForBuyNow bit,
	BuyNowCost decimal(15,2),
	LanceCost decimal(15,2),
	ClosingLanceCount int,
	WonByUser int,
	LancesSpentByWinner decimal(15,2),
	RegularCost decimal(15,2),
	PercentageSaving int
)
 
create table UserAuctionLance
(
	UserAuctionLanceId int primary key identity(1,1),
	UserId int foreign key references [User](UserId),
	AuctionId int foreign key references Auction(AuctionId),
	DateTimeOfLance datetime,
	LanceCost decimal(16,2),
	UserName nvarchar(256)
)

create table LancePackage
(
	LancePackageId int primary key identity(1,1),
	Name nvarchar(256) not null,
	CreditAmount int not null,
	Costo decimal(18,2) not null,
)

create table BoughtPackage
(
	BoughtPackageId int primary key identity(1,1),
	UserId int foreign key references [User](UserId),
	LancePackageId int foreign key references LancePackage(LancePackageId),
	DateOfPurchase datetime not null,
	Total decimal(18,2) not null
)

/* Insert some default values for Gender. */
insert into Gender (Name)
values ('Male');

insert into Gender (Name)
values ('Female');

insert into Gender (Name)
values ('Unkown');

/* Insert some default Auction Category types. */
insert into AuctionCategory (Name)
values ('New Users');

insert into AuctionCategory (Name)
values ('Bronze');

insert into AuctionCategory (Name)
values ('Silver');

insert into AuctionCategory (Name)
values ('Gold');

insert into AuctionCategory (Name)
values ('Platinum');

/* Insert some default User Roles. */

insert into UserRole (Name)
values ('Regular');

insert into UserRole (Name)
values ('Auction Administrator');

insert into UserRole (Name)
values ('User Administrator');

insert into UserRole (Name)
values ('Finance Administrator');

insert into UserRole (Name)
values ('Admin');

insert into UserRole (Name)
values ('God');