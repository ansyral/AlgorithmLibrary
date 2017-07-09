namespace XuanLibrary.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using XuanLibrary.Fx;
    using XuanLibrary.Test.TestDataStructure;
    using XuanLibrary.Utility;

    using Xunit;

    public class HybridStreamSerializationTest
    {
        [Fact]
        public void TestBasic()
        {
            var model = new TestModel(
                "hi",
                new List<int> { 1, 2 },
                new List<Complex>
                {
                    new Complex { ComplexPropertyA = 3, ComplexPropertyB = new List<string> { "complex31", "complex32" } },
                    new Complex { ComplexPropertyA = 4, ComplexPropertyB = new List<string> { "complex41", "complex42" } },
                },
                new Dictionary<string, List<string>>
                {
                    { "property1", new List<string> { "complex51" } },
                    { "property2", new List<string>() },
                    { "property3", new List<string> { "complex51", "complex52" } },
                },
                new PropertyItem
                {
                    Property = new Dictionary<string, Complex>
                    {
                        { "property4", new Complex { ComplexPropertyA = 8, ComplexPropertyB = new List<string> { "complex81", "what" } } },
                        { "property5", new Complex { ComplexPropertyA = 9, ComplexPropertyB = new List<string> { "are", "the", "person" } } },
                    }
                },
                new PropertyItem
                {
                    Property = new Dictionary<string, Complex>
                    {
                        { "property6", new Complex { ComplexPropertyA = 10, ComplexPropertyB = new List<string>() } },
                    }
                });

            // serialize to file
            var file = Path.GetRandomFileName();
            using (var fs = File.Create(file))
            {
                Serialization.Serialize(
                    model,
                    fs,
                    (obj, stream) =>
                    {
                        // leave stream open as we still need to write other content into the stream.
                        using (var sw = new StreamWriter(stream, Encoding.UTF8, 0x100, true))
                        {
                            JsonUtility.Serialize(sw, obj);
                        }
                    },
                    (obj, stream) =>
                    {
                        using (var sw = new StreamWriter(stream, Encoding.UTF8, 0x100, true))
                        {
                            JsonUtility.Serialize(sw, obj);
                        }
                    });
            }

            // deserialize file
            using (var fs = File.OpenRead(file))
            {
                var deserializedModel = Serialization.Deserialize(
                    fs,
                    stream =>
                    {
                        using (var sr = new StreamReader(stream, Encoding.UTF8, false, 0x100, true))
                        {
                            return JsonUtility.Deserialize<PropertyItem>(sr);
                        }
                    },
                    stream =>
                    {
                        using (var sr = new StreamReader(stream, Encoding.UTF8, false, 0x100, true))
                        {
                            return JsonUtility.Deserialize<PropertyItem>(sr);
                        }
                    });

                Assert.Equal(JsonUtility.ToJsonString(model), JsonUtility.ToJsonString(deserializedModel));
            }
        }
    }

    internal static class Serialization
    {
        public static void Serialize(TestModel model, Stream stream, Action<object, Stream> serializer1, Action<object, Stream> serializer2)
        {
            var writer = new HybridStreamWriter(stream);
            writer.Write(new TestModelSerializationCollection(model));
            writer.Write(str => serializer1(model.PropertyE, str));
            writer.Write(str => serializer2(model.PropertyF, str));
        }

        public static TestModel Deserialize(Stream stream, Func<Stream, object> deserializer1, Func<Stream, object> deserializer2)
        {
            var reader = new HybridStreamReader(stream);
            var propertyCollection = reader.Read() as Dictionary<string, object>;
            var seg = reader.ReadSegment();
            var part1 = deserializer1(reader.ReadStream(seg));
            var part2 = deserializer2(reader.ReadNext(seg) as Stream);
            return new TestModel(
                (string)propertyCollection[nameof(TestModel.PropertyA)],
                ((List<object>)propertyCollection[nameof(TestModel.PropertyB)]).OfType<int>().ToList(),
                JsonUtility.FromJsonString<List<Complex>>((string)propertyCollection[nameof(TestModel.PropertyC)]),
                ((Dictionary<string, object>)propertyCollection[nameof(TestModel.PropertyD)]).ToDictionary(p => p.Key, p => ((List<object>)p.Value).OfType<string>().ToList()),
                (PropertyItem)part1,
                (PropertyItem)part2);
        }
    }
}
