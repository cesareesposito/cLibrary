// See https://aka.ms/new-console-template for more information

using Test_dll;
using cLibrary.Helper;
using cLibrary.Models;
using ECHO.BLL;

List<int> list = new List<int> { 1, 2, 3, 4, 5 };

List<Person> people = new List<Person>
{
    new Person { Name = "Alice", Age = 25 },
    new Person { Name = "Bob", Age = 30 },
    new Person { Name = "Charlie", Age = 35 }
};
Person p= null;
var e = p.ToQueryString();

var bll = new CompanyBll();
var x = bll.GetAsync(new PersonFilter());

Console.ReadLine();
