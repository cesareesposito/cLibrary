using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class cEnumerable_Example
    {
        public void Example() {
            cResult<int> numResult = list.cForEach(it =>
            {
                return 100;
            });
            int num = numResult.HasValue ? numResult.Value : 0;

            cResult<string> sResult = list.cForEach(it =>
            {
                return "1";
            });
            string s = sResult.HasValue ? sResult.Value : "";

            cResult<OperationResult> oResult = list.cForEach(it =>
            {
                return new OperationResult();
            });
            OperationResult o = oResult.HasValue ? oResult.Value : null;

            list.cForEach(it =>
            {
                // Fai qualcosa
            });

            cResult<string> foundResult = list.cForEach(it =>
            {
                if (it == 1)
                {
                    return "trovato";
                }
                return null;
            });
            string found = foundResult.HasValue ? foundResult.Value : "";

        }
    }
}
