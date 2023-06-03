using cLibrary.Models;
using cLibrary.Models.Base;
using ECHO.DB.Context;
using ECHO.DB.Models;
using Microsoft.EntityFrameworkCore;
using cLibrary.Helper;

namespace ECHO.BLL
{
    public class CompanyBll
    {
        private readonly EchoContext _ctx;
        public CompanyBll()
        {
            _ctx = new EchoContext();
        }        

        public async Task<DataSource<Company>> Get(Filter filter)
        {
            try
            {
                var query = _ctx.Companies.AsQueryable();

                if (filter.SearchText.IsNotNullOrEmpty())
                    query = query.Where(it => it.name.Contains(filter.SearchText));

                var dataSource = query.ApplyBaseFilter(filter);
                return dataSource;

            }
            catch { return new DataSource<Company>(); }
        }

        public async Task<DataSource<Company>> GetAsync(Filter filter)
        {
            try
            {
                var query = _ctx.Companies.AsQueryable();

                if (filter.SearchText.IsNotNullOrEmpty())
                    query = query.Where(it => it.name.Contains(filter.SearchText));

                var dataSource = await query.ApplyBaseFilterAsync(filter);
                return dataSource;

            }
            catch { return new DataSource<Company>(); }
        }

        public async Task<Company> Get(int id)
        {
            try
            {
                var model = await _ctx.Companies
                             .FirstOrDefaultAsync(it => it.id == id);

                if (model == null) throw new Exception("Not found");

                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }       
    }
}
