use AdventureWorks2019
select * from HumanResources.Department

 --For the Chosen department display Employees BusinessEntityID, Firstname, Gender, MaritalStatus, HireDate in a table.

 select * from HumanResources.EmployeeDepartmentHistory
 select * from Person.Person
 select * from HumanResources.Employee

 go
 create proc sp_GetEmpByDept
 @Name varchar(50)
 as
 begin
 select dept.Name,dept.DepartmentID,emp.BusinessEntityID,FirstName,Gender,MaritalStatus,HireDate
 from HumanResources.Department dept 
 join HumanResources.EmployeeDepartmentHistory empDept
 on dept.DepartmentID=empDept.DepartmentID
 join Person.Person per
 on per.BusinessEntityID=empDept.BusinessEntityID
 join HumanResources.Employee emp
 on emp.BusinessEntityID=empDept.BusinessEntityID
 where dept.Name=@Name
 end

 drop proc sp_GetEmpByDept
 exec sp_GetEmpByDept 'Engineering'



 go
 create proc sp_GetAllEmpByDept
 as
 begin
 select dept.Name,dept.DepartmentID,emp.BusinessEntityID,FirstName,Gender,MaritalStatus,HireDate
 from HumanResources.Department dept 
 join HumanResources.EmployeeDepartmentHistory empDept
 on dept.DepartmentID=empDept.DepartmentID
 join Person.Person per
 on per.BusinessEntityID=empDept.BusinessEntityID
 join HumanResources.Employee emp
 on emp.BusinessEntityID=empDept.BusinessEntityID
 end

  exec  sp_GetAllEmpByDept 

