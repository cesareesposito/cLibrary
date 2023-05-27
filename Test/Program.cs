// See https://aka.ms/new-console-template for more information

using csLibrary.extensions;

string numberString = "10";
int n1 = numberString.ToInt();
Console.WriteLine(n1);  // Output: 10

string date = "2023-05-27";
DateTime dateTime = date.ToDateTime();
Console.WriteLine(dateTime);  // Output: 2023-05-27 00:00:00

Console.ReadLine();
