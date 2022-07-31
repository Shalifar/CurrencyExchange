using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeApp
{
    public class ResultModel
    {
        private ResultModel() { }

        public static ResultModel Success(decimal result)
        {
            return new ResultModel
            {
                IsSuccess = true,
                Result = result,
            };
        }

        public static ResultModel Failed(string errorMessage)
        {
            return new ResultModel 
            { 
                IsSuccess = false, 
                ErrorMessage = errorMessage 
            };
        }

        public bool IsSuccess { get; private set; }
        public decimal Result { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}
