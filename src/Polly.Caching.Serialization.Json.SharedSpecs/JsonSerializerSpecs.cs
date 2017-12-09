using System;
using FluentAssertions;
using Xunit;
using Polly.Caching.Serialization.Json;

namespace Polly.Specs.Caching.Serialization.Json
{
    public class JsonSerializerSpecs
    {
        #region Configuration

        [Fact]
        public void Should_throw_when_JsonSerializerSettings_is_null()
        {
            Action configure = () => new JsonSerializer<object>(null);

            configure.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("serializerSettings");
        }

        [Fact]
        public void Should_not_throw_when_configured_with_non_null_JsonSerializerSettings()
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();

            Action configure = () => new JsonSerializer<object>(settings);

            configure.ShouldNotThrow();
        }

        #endregion

        #region Serialize

        [Fact]
        public void Should_serialize_using_configured_JsonSerializerSettings()
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeHtml
            };

            JsonSerializer<SampleClass> serializer = new JsonSerializer<SampleClass>(settings);

            SampleClass objectToSerialize = new SampleClass()
            {
                StringProperty = "<html></html>",
                IntProperty = 1
            };

            string serialized = serializer.Serialize(objectToSerialize);

            serialized.Should().Be("{\"StringProperty\":\"\\u003chtml\\u003e\\u003c/html\\u003e\",\"IntProperty\":1}");
        }

        #endregion

        #region Deserialize

        [Fact]
        public void Should_deserialize_using_configured_JsonSerializerSettings()
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeHtml
            };

            JsonSerializer<SampleClass> serializer = new JsonSerializer<SampleClass>(settings);

            string serialized = "{\"StringProperty\":\"\\u003chtml\\u003e\\u003c/html\\u003e\",\"IntProperty\":1}";

            SampleClass deserialized = serializer.Deserialize(serialized);

            deserialized.ShouldBeEquivalentTo(new
            {
                IntProperty = 1,
                StringProperty = "<html></html>"
            });
        }

        #endregion
    }

    internal class SampleClass
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}
