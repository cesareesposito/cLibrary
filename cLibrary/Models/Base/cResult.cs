using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cLibrary.Models.Base
{
    public class cResult<TResult>
    {
        public TResult Value { get; }
        public bool HasValue { get; }

        public cResult(TResult value)
        {
            Value = value;
            HasValue = true;
        }
    }
}
