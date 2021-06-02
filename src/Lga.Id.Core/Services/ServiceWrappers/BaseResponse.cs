using System;
using System.Collections.Generic;
using System.Text;

namespace Lga.Id.Core.Services.ServiceWrappers
{
    public abstract class BaseResponse<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }

        protected BaseResponse(T data)
        {
            IsSuccess = true;
            Message = string.Empty;
            Data = data;
        }

        protected BaseResponse(string message)
        {
            IsSuccess = false;
            Message = message;
            Data = default;
        }
    }
}
