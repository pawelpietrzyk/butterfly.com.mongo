using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace butterfly.com.mongo
{
    
    [DataContract]
    public class MongoId
    {
        [DataMember(Name="$oid")]
        public string oid { get; set; }

        public override string ToString()
        {
            return oid;
        }

    }
    [DataContract]
    public class MongoDocument
    {
        [DataMember(Name="_id")]
        public MongoId Id { get; set; }

        public override string ToString()
        {
            if (Id != null)
            {
                return Id.ToString();
            }
            return String.Empty;
        }
    }
}
