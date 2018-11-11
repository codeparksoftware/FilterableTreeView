using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FilterableTreeView
{
    public static class MyClass
    {
        public static void SerializeObject(this List<CategoryClass> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<CategoryClass>));
            using (var stream = File.OpenWrite(fileName))
            {
                serializer.Serialize(stream, list);
            }
        }

        public static void Deserialize(this List<CategoryClass> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<CategoryClass>));
            using (var stream = File.OpenRead(fileName))
            {
                var other = (List<CategoryClass>)(serializer.Deserialize(stream));
                list.Clear();
                list.AddRange(other);
            }
        }
    }
}
