using libras_connect_domain.Enums;
using libras_connect_domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;

namespace libras_connect_domain.Serializer
{
    public class JointDataSerializer : IBsonSerializer<IDictionary<JointEnum, JointData>>
    {
        public Type ValueType
        {
            get
            {
                return typeof(IDictionary<JointEnum, JointData>);
            }
        }

        public IDictionary<JointEnum, JointData> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            Dictionary<JointEnum, JointData> dictionary = new Dictionary<JointEnum, JointData>();

            IBsonArraySerializer serialize = new BsonArraySerializer();

            BsonArray bsonArray = serialize.Deserialize(context, args) as BsonArray;
            
            foreach (BsonValue bsonValue in bsonArray.Values)
            {
                JointData jointData = BsonSerializer.Deserialize<JointData>(bsonValue.ToBsonDocument());
                dictionary.Add(jointData.JointEnum, jointData);
            }
            
            return dictionary;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            this.Serialize(context, args, value as IDictionary<JointEnum, JointData>);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, IDictionary<JointEnum, JointData> value)
        {
            IBsonArraySerializer serialize = new BsonArraySerializer();
            BsonArray bsonArray = new BsonArray();

            foreach (JointData jointData in value.Values)
            {
                bsonArray.Add(jointData.ToBsonDocument());
            }

            serialize.Serialize(context, bsonArray);
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return this.Deserialize(context, args);
        }
    }
}
