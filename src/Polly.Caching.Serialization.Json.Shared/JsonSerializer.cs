using System;
using Newtonsoft.Json;

namespace Polly.Caching.Serialization.Json
{
    /// <summary>
    /// A serializer for serializing items of type <typeparamref name="TResult"/> to JSON, for the Polly <see cref="CachePolicy"/>
    /// </summary>
    /// <typeparam name="TResult"/>
    public class JsonSerializer<TResult> : ICacheItemSerializer<TResult, string>
    {
        private readonly JsonSerializerSettings _serializerSettings;

        /// <summary>
        /// Constructs a new <see cref="JsonSerializer{TResult}"/> using the given <see cref="Newtonsoft.Json.JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="serializerSettings">The <see cref="JsonSerializerSettings"/> to use for serialization and deserialization.</param>
        public JsonSerializer(Newtonsoft.Json.JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings ?? throw new ArgumentNullException(nameof(serializerSettings));
        }

        /// <summary>
        /// Deserializes the passed json-serialization of an object, to type <typeparamref name="TResult"/>, using the <see cref="Newtonsoft.Json.JsonSerializerSettings"/> passed when the <see cref="JsonSerializer{TResult}"/> was constructed.
        /// </summary>
        /// <param name="objectToDeserialize">The object to deserialize</param>
        /// <returns>The deserialized object</returns>
        public TResult Deserialize(string objectToDeserialize)
        {
            return JsonConvert.DeserializeObject<TResult>(objectToDeserialize, _serializerSettings);
        }

        /// <summary>
        /// Serializes the specified object to JSON, using the <see cref="Newtonsoft.Json.JsonSerializerSettings"/> passed when the <see cref="JsonSerializer{TResult}"/> was constructed.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <returns>The serialized object</returns>
        public string Serialize(TResult objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize, _serializerSettings);
        }
    }
}
