using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DataAccessLayer.Context.Models
{
    public class Unit
    {
        public int UnitId { get; set; }  // int
        public string UnitCode { get; set; }   // nvarchar(400)
        public string UnitName { get; set; }    // nvarchar(400)
    }
}
