﻿namespace cLibrary.models.Base
{
    public abstract class MessageModel
    {
        public string Severity { get; protected set; } = "success";
        public string Summary { get; protected set; } = "Success";

        private string _deatail = OperationResult.DefaultMessage;
        public string Detail
        {
            get
            {
                if (!_deatail.EndsWith(Environment.NewLine))
                    _deatail += Environment.NewLine;
                return _deatail;
            }
            set
            {
                if (!value.EndsWith(Environment.NewLine))
                    value += Environment.NewLine;
                _deatail = value;
            }
        }
    }
}
