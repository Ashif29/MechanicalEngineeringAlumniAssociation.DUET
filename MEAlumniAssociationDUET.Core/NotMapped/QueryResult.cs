using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAlumniAssociationDUET.Core.NotMapped
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IList<T> Items { get; set; }
    }
    public class QueryResultSpecial<T>
    {
        public int TotalItems { get; set; }
        public float Total1 { get; set; }
        public float Total2 { get; set; }
        public IList<T> Items { get; set; }
    }
}
