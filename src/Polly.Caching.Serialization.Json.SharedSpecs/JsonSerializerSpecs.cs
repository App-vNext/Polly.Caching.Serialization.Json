using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Polly.Caching.Serialization.Json;

namespace Polly.Specs.Caching.Serialization.Json
{
    public class JsonSerializerSpecs
    {
        readonly Newtonsoft.Json.JsonSerializerSettings StandardSettings = new Newtonsoft.Json.JsonSerializerSettings()
        {
            StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeHtml
        };

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
            JsonSerializer<SampleClass> serializer = new JsonSerializer<SampleClass>(StandardSettings);

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
            JsonSerializer<SampleClass> serializer = new JsonSerializer<SampleClass>(StandardSettings);

            string serialized = "{\"StringProperty\":\"\\u003chtml\\u003e\\u003c/html\\u003e\",\"IntProperty\":1}";

            SampleClass deserialized = serializer.Deserialize(serialized);

            deserialized.ShouldBeEquivalentTo(new
            {
                IntProperty = 1,
                StringProperty = "<html></html>"
            });
        }

        [Fact]
        public void Throws_on_deserialize_null()
        {
            JsonSerializer<object> serializer = new JsonSerializer<object>(StandardSettings);

            Action deserializeNull = () => serializer.Deserialize(null);

            deserializeNull.ShouldThrow<ArgumentNullException>();
        }

        #endregion

        #region Round-trip

        [Theory]
        [MemberData(nameof(SampleClassData))]
        public void Should_roundtrip_all_variants_of_reference_type(SampleClass testValue)
        {
            JsonSerializer<SampleClass> serializer = new JsonSerializer<SampleClass>(StandardSettings);

            serializer.Deserialize(serializer.Serialize(testValue)).ShouldBeEquivalentTo(testValue);
        }

        public static TheoryData<SampleClass> SampleClassData =>
            new TheoryData<SampleClass>
            {
               new SampleClass(),
               new SampleClass()
               {
                   StringProperty = "<html></html>",
                   IntProperty = 1
               },
               (SampleClass)null,
               default(SampleClass)
            };

        public class SampleClass
        {
            public string StringProperty { get; set; }
            public int IntProperty { get; set; }
        }

        [Theory]
        [MemberData(nameof(SampleStringData))]
        public void Should_roundtrip_all_variants_of_string(String testValue)
        {
            JsonSerializer<String> serializer = new JsonSerializer<String>(StandardSettings);

            serializer.Deserialize(serializer.Serialize(testValue)).Should().Be(testValue);
        }

        public static TheoryData<String> SampleStringData =>
            new TheoryData<String>
            {
                "some string",
                "",
                null,
                default(string),
                "null"
            };

        [Theory]
        [MemberData(nameof(SampleNumericData))]
        public void Should_roundtrip_all_variants_of_numeric(int testValue)
        {
            JsonSerializer<int> serializer = new JsonSerializer<int>(StandardSettings);

            serializer.Deserialize(serializer.Serialize(testValue)).Should().Be(testValue);
        }


        public static TheoryData<int> SampleNumericData =>
            new TheoryData<int>
            {
               -1,
               0,
               1,
               default(int)
            };

        [Theory]
        [MemberData(nameof(SampleEnumData))]
        public void Should_roundtrip_all_variants_of_enum(SampleEnum testValue)
        {
            JsonSerializer<SampleEnum> serializer = new JsonSerializer<SampleEnum>(StandardSettings);

            serializer.Deserialize(serializer.Serialize(testValue)).Should().Be(testValue);
        }


        public static TheoryData<SampleEnum> SampleEnumData =>
            new TheoryData<SampleEnum>
            {
              SampleEnum.FirstValue,
              SampleEnum.SecondValue,
              default(SampleEnum),
            };

        public enum SampleEnum
        {
            FirstValue,
            SecondValue,
        }

        [Theory]
        [MemberData(nameof(SampleBoolData))]
        public void Should_roundtrip_all_variants_of_bool(bool testValue)
        {
            JsonSerializer<bool> serializer = new JsonSerializer<bool>(StandardSettings);

            serializer.Deserialize(serializer.Serialize(testValue)).Should().Be(testValue);
        }

        public static TheoryData<bool> SampleBoolData =>
            new TheoryData<bool>
            {
               true,
               false,
               default(bool),
            };

        [Theory]
        [MemberData(nameof(SampleNullableBoolData))]
        public void Should_roundtrip_all_variants_of_nullable_bool(bool? testValue)
        {
            JsonSerializer<bool?> serializer = new JsonSerializer<bool?>(StandardSettings);

            serializer.Deserialize(serializer.Serialize(testValue)).Should().Be(testValue);
        }

        public static TheoryData<bool?> SampleNullableBoolData =>
            new TheoryData<bool?>
            {
                true,
                false,
                null,
                default(bool?),
            };
        #endregion
    }

}
