// See https://aka.ms/new-console-template for more information


using Test;
using cLibrary.Helper;

var list =await new Service().Get();
list.Items.cForEach(it =>
{
    Console.WriteLine($"Id : {it.Id} - Name :{it.Name}");
});
Console.ReadLine();

var test = new cTaskManager_Example();
test.Run();