using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;


namespace butterfly.com.mongo
{
    [DataContract]
    public class MongoInfo : MongoDocument
    {
        [DataMember(Name="content")]
        public string Content { get; set; }

        [DataMember(Name="updated")]
        public DateTime Updated { get; set; }

        [DataMember(Name="version")]
        public int Version { get; set; }

        public MongoInfo()
        {
            this.Version = 1;
        }
        public override string ToString()
        {
            return String.Format("{0} ({1:d} {1:T}). Version {2}", new object[] {this.Id, this.Updated, this.Version });
        }
        public void save(string fileName)
        {
            try
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(fileStream);
                string data = this.serialize();
                writer.Write(data);
                writer.Close();
                fileStream.Close();
            }
            catch { }
        }
        public static MongoInfo ReadFromText(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open);
                    StreamReader reader = new StreamReader(fs);
                    string data = reader.ReadToEnd();
                    reader.Close();
                    fs.Close();
                    return MongoInfo.deserialize(data);
                }
                catch { }                
            }
            return null;
        }
        
        public string serialize()
        {
            string ret = String.Empty;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MongoInfo));
            MemoryStream stream = new MemoryStream();
            ser.WriteObject(stream, this);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }
        public static MongoInfo deserialize(string content)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MongoInfo));
            byte[] bytes = Encoding.ASCII.GetBytes(content);
            MemoryStream memory = new MemoryStream(bytes);

            MongoInfo obj = (MongoInfo)ser.ReadObject(memory);
            return obj;
        }
    }

    public class MongoInfoList : List<MongoInfo>
    {
        public string serialize()
        {
            string ret = String.Empty;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MongoInfoList));
            MemoryStream stream = new MemoryStream();
            ser.WriteObject(stream, this);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            ret = reader.ReadToEnd();
            reader.Close();
            return ret;           
        }
        public static MongoInfoList deserialize(string content)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MongoInfoList));
            byte[] bytes = Encoding.ASCII.GetBytes(content);
            MemoryStream memory = new MemoryStream(bytes);

            MongoInfoList obj = (MongoInfoList)ser.ReadObject(memory);
            return obj;
        }
    }
}
