using System;

namespace Pharmacy.Service.Exceptions
{
    public class PharmacyException : Exception
    {
        public int Code { get; set; }

        public PharmacyException(int code, string message) :
            base(message) =>
            this.Code = code;
    }
}
