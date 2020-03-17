using System;
using System.Collections.Generic;
using System.Text;

namespace NSOAddDataToMongo.Models
{
    public class ResultModel
    {
        public string Zippath { get; set; }
        public IEnumerable<string> ContainerName { get; set; }
    }
}
