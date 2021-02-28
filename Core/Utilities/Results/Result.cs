using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool succes, string message):this(succes)
        {
            this.Message = message;
        }

        public Result(bool succes)
        {
            this.Success = succes;
        }

        public bool Success { get; }
        public string Message { get; }

        
    }
}
