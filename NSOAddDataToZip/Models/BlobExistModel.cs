using System;
using System.Collections.Generic;
using System.Text;

namespace NSOAddDataToMongo.Models
{
    public class BlobExistModel
    {
        public string _id { get; set; }
        public List<string> listBlob { get; set; }
        public List<string> listEA { get; set; }
        public bool IsRun { get; set; }
    }
}
