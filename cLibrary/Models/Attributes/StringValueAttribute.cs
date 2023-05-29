using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cLibrary.Models.Attributes
{
    public class StringValueAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string Value { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.Value = value;
        }

        #endregion
    }
}
