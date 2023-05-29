using cLibrary.Helper;
using cLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{

    class Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    class Filter : cLibrary.Models.Base.Filter { }
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
                SortOrder = cLibrary.Models.Enums.SortDirection.Descending
            };

            dtos.cForEach(it => it.Name = "3333");

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

}
