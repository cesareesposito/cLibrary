// See https://aka.ms/new-console-template for more information


using cLibrary.Models.Base;
using cLibrary.Models;
using Test_dll;
using cLibrary.Helper;

List<int> list = new List<int> { 1, 2, 3, 4, 5 };
List<Person> people = new List<Person>
{
    new Person { Name = "Alice", Age = 25 },
    new Person { Name = "Bob", Age = 30 },
    new Person { Name = "Charlie", Age = 35 }
};

var numResultp = people.cForEach(it =>
{
    return 100;
});

var sResultp = list.cForEach(it =>
{
    return "1";
});

var oResultp = people.cForEach(it =>
{
    return new OperationResult();
});

people.cForEach(it =>
{
    // Fai qualcosa
});

people.cForEach(it =>
{
    if (it.Name == "") return it;


    return null;
    // Fai qualcosa
});

var numResult = list.cForEach(it =>
{
    return 100;
});

var sResult = list.cForEach(it =>
{
    return "1";
});

var oResult = list.cForEach(it =>
{
    return new OperationResult();
});

list.cForEach(it =>
{
    // Fai qualcosa
});


Console.WriteLine("Hello, World!");
