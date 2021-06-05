using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        

        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        //public bool Success => throw new NotImplementedException(); // bu şekilde sadece return eder.

        public bool Success { get; }

        public string Message { get; }
    }
}
