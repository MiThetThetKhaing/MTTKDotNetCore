# MTTKDotNetCore

C# .NET

C# Language .NET

Console App Windows Forms ASP.NET Core Web API ASP.NET Core Web MVC Blazor Web Assembly Blazor Web Server

.NET framework (1, 2, 3, 3.5, 4, 4.5, 4.6, 4.7, 4.8) windows .NET Core (1, 2, 2.2, 3, 3.1) vs2019, vs2022 - windows, linux, macos .NET (5 - vs2019, 6 - vs2022, 7, 8 - windows, linux, macos

vscode visual studio 2022

windows

UI + Business Logic + Data Access => Database

Kpay

Mobile No => Transfer

Mobile No Check 10000

SLH => Collin

10000 => 0

-5000 => +5000

Bank + 5000

SLHDotNetCore

SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog]

GO

select * from Tbl_Blog where DeleteFlag = 0

update Tbl_Blog set BlogTitle = 'heehee2' where BlogId = 1
update Tbl_Blog set DeleteFlag = 1 where BlogId = 2

delete from Tbl_Blog where BlogId = 1

-- Product Apple 1000, Orange 1000
-- Staff Apple 2, Orange 1
-- 3000, 2000, 1000


oracle

select * from tbl_blog with (nolock)

commit data / uncommit data

insert into commit

update tbl_blog commit

1 - mg mg 1 
2 - mg mg 2 
3 - mg mg 3 
4 - mg mg 4 
5 - mg mg 5

1 - mg mg 1 
2 - mg mg 2 
3 - mg mg 6 
4 - mg mg 4 
5 - mg mg 5

efcore database first (manual, auto) / code first

dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch5;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f

## My efcore command
dotnet ef dbcontext scaffold "Server=DESKTOP-6QTP69L\MSSQLSERVER2022;Database=DotNetTrainingBatch5;User Id=sa;Password=sa123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f --table

API

HttpMethod HttpStatusCode Request / Response

## Kpay

Mobile No

Me - Another One

Id
FullName
MobileNo
Balance
Pin => 000000

Bank => Deposit / Withdraw

### Deposit

Deposit API => MobileNo, Balance (+) => 1000 + (-1000)

### Withdraw

Withdraw API => MobileNo, Balance (+) => 1000 - (-1000)
at least 10,000 MMK

### Transfer 

Transfer API => 

FromMobileNo
ToMobileNo
Amount
Pin
Notes

- FromMobile check
- ToMobileNo check
- FromMobileNo != ToMobileNo
- Pin ==
- Balance
- FromMobileNo Balance -
- ToMobileNo Balance +
- Message (Complete)
- Transaction History

- Balance

- Create Wallet User
- Login
- Change Pin
- Phone No Change
- Forget Password
- Reset Password
- First Time Login
