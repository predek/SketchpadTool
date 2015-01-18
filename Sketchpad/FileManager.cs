using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SketchpadTool
{
    public class FileManager
    {
        public List<Segment> Read(String filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Segment>));
            List<Segment> content;

            FileStream fs = new FileStream(@filename, FileMode.Open);
            content = (List<Segment>)serializer.Deserialize(fs);
            fs.Close();
            return content;
        }

        public void Write(String filename, List<Segment> content)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Segment>));
            FileStream fs = new FileStream(@filename, FileMode.Create);            
            serializer.Serialize(fs, content);
            fs.Close();
            
        }
    }
}
