// See https://aka.ms/new-console-template for more information

using Test_dll;
using cLibrary.Helper;
using cLibrary.Models;

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

Console.WriteLine(numResultp);

var sResultp = list.cForEach(it =>
{
    return "1";
});
Console.WriteLine(sResultp);

var oResultp = people.cForEach(it =>
{
    return new OperationResult();
});

Console.WriteLine(oResultp.);

people.cForEach(it =>
{
    // Fai qualcosa
});

var res = people.cForEach(it =>
{
    if (it.Name == "") return it;

    return null;
    // Fai qualcosa
});

Console.WriteLine(res.Name);

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
