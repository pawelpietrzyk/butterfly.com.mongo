using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace butterfly.com.mongo
{
    [DataContract]
    public class MongoDayOff : MongoDocument
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "date")]
        public string Date { get; set; }
        [DataMember(Name = "state")]
        public int State { get; set; }        
    }

    public class MongoDayOffs : List<MongoDayOff>
    {

    }    
}
