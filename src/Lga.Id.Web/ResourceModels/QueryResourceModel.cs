using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.ResourceModels
{


    //Base class for other queries
    public class QueryResourceModel
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
