using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University
{
    public class ApiReturn<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int Code { get; set; }
    }

    public class ApiPagedReturn<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int Code { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }
    }
}
