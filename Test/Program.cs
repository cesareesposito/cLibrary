// See https://aka.ms/new-console-template for more information

using cLibrary.models;
using cLibrary.models.Base;
using csLibrary.extensions;

string numberString = "10";
int n1 = numberString.ToInt();
Console.WriteLine(n1);  // Output: 10

string date = "2023-05-27";
DateTime dateTime = date.ToDateTime();
Console.WriteLine(dateTime);  // Output: 2023-05-27 00:00:00
Console.ReadLine();

class Dto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
class Filter : FilterBase { }
class Service
{
    private List<Dto> dtos = new()
    {
        new Dto { Id = 1, Name = "Mario" },
        new Dto { Id = 2, Name = "Rossi" }
    };
    async Task<DataSource<Dto>> Get()
    {
        var filter = new Filter()
        {
            PageSize = 10,
            SortField = "Id",
            //cLibrary.models.Enums.SortDirection.Descending = 1
            //cLibrary.models.Enums.SortDirection.Ascending = 0
            //SortOrder = cLibrary.models.Enums.SortDirection.Descending,
            //or
            SortOrder = (cLibrary.models.Enums.SortDirection)1
        };
        var result = dtos
            .AsQueryable()
            .ApplyBaseFilterAsync(filter);

        return await result;
        // Output: 
        //"DataSource": {
        //    "TotalItems": 2,
        //    "Items": [
        //      {
        //        "Id": 2,
        //        "Name": "Rossi"
        //      },
        //      {
        //        "Id": 1,
        //        "Name": "Mario"
        //      }
        //    ] 
        //  }
    }
}




