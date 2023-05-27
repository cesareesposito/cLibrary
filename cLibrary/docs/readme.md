The cLibrary is a C# library that provides utilities to simplify the conversion between data types, 
create Excel files, and facilitate the usage of queries with LINQ.

Installation
You can install the cLibrary using the NuGet package manager.
Run the following command in the Visual Studio package manager console:

Install-Package cLibrary


##License

The cLibrary library is distributed under the MIT license. Please refer to the LICENSE file for more information.


##Contact

For any questions or support requests, you can reach us at cesareuni88@gmail.com.
We hope you find the cLibrary library useful in your project. Thank you for using it!


##HOW TO USE

##cCovert
//using csLibrary.extensions;
//
//string numberString = "10";
//int n1 = numberString.ToInt();
//Console.WriteLine(n1);  // Output: 10
//
//string date = "2023-05-27";
//DateTime dateTime = date.ToDateTime();
//Console.WriteLine(dateTime);  // Output: 2023-05-27 00:00:00

##cQueryable
//class Dto
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//}
//class Filter : FilterBase { }
//class Service
//{
//    private List<Dto> dtos = new()
//    {
//        new Dto { Id = 1, Name = "Mario" },
//        new Dto { Id = 2, Name = "Rossi" }
//    };
//    async Task<DataSource<Dto>> Get()
//    {
//        var filter = new Filter()
//        {
//            PageSize = 10,
//            SortField = "Id",
//            //cLibrary.models.Enums.SortDirection.Descending = 1
//            //cLibrary.models.Enums.SortDirection.Ascending = 0
//            //SortOrder = cLibrary.models.Enums.SortDirection.Descending,
//            //or
//            SortOrder = (cLibrary.models.Enums.SortDirection)1
//        };
//        var result = dtos
//            .AsQueryable()
//            .ApplyBaseFilterAsync(filter);
//
//        return await result;
//        // Output: 
//        //"DataSource": {
//        //    "TotalItems": 2,
//        //    "Items": [
//        //      {
//        //        "Id": 2,
//        //        "Name": "Rossi"
//        //      },
//        //      {
//        //        "Id": 1,
//        //        "Name": "Mario"
//        //      }
//        //    ] 
//        //  }
//    }
//}