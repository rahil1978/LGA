using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.ResourceModels
{
    public class QueryResultResourceModel<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; } = 0;
       
    }
}
