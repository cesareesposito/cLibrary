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
int nump = numResultp.HasValue ? numResultp.Value : 0;

var sResultp = list.cForEach(it =>
{
    return "1";
});
string sp = sResultp.HasValue ? sResultp.Value : "";

var oResultp = people.cForEach(it =>
{
    return new OperationResult();
});
OperationResult op = oResultp.HasValue ? oResultp.Value : null;

people.cForEach(it =>
{
    // Fai qualcosa
});

var foundResultp = people.cForEach(it =>
{
    if (it.Age == 35)
    {
        return "trovato";
    }
    return null;
});



var numResult = list.cForEach(it =>
{
    return 100;
});
int num = numResult.HasValue ? numResult.Value : 0;

var sResult = list.cForEach(it =>
{
    return "1";
});
string s = sResult.HasValue ? sResult.Value : "";

var oResult = list.cForEach(it =>
{
    return new OperationResult();
});
OperationResult o = oResult.HasValue ? oResult.Value : null;

list.cForEach(it =>
{
    // Fai qualcosa
});

var foundResult = list.cForEach(it =>
{
    if (it == 1)
    {
        return "trovato";
    }
    return null;
});
string found = foundResult.HasValue ? foundResult.Value : "";

Console.WriteLine("Hello, World!");
