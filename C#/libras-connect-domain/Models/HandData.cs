using Newtonsoft.Json;
using libras_connect_domain.Enums;
using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using libras_connect_domain.Serializer;

namespace libras_connect_domain.Models
{
    public class HandData
    {
        public HandData(PXCMHandData.IHand ihand, int handEnum)
        {
            HandEnum = (HandEnum)(handEnum - 2);
            TimeStamp = ihand.QueryTimeStamp();
            HandTrackingStatusEnum = (HandTrackingStatusEnum)ihand.QueryTrackingStatus();
            BodySideEnum = (BodySideEnum)ihand.QueryBodySide();
            Openness = ihand.QueryOpenness();
            PalmRadiusImage = ihand.QueryPalmRadiusImage();
            PalmRadiusWorld = ihand.QueryPalmRadiusWorld();
            BoundingBox = new BoundingBox(ihand.QueryBoundingBoxImage());
            MassCenterImage = new MassCenterImage(ihand.QueryMassCenterImage());
            MassCenterWorld = new MassCenterWorld(ihand.QueryMassCenterWorld());
            PalmOrientation = new PalmOrientation(ihand.QueryPalmOrientation());
        }

        public HandData()
        {
        }

        [JsonProperty("he")]
        public HandEnum HandEnum { get; set; }
        [JsonProperty("t")]
        public long TimeStamp { get; set; }
        [JsonProperty("htse")]
        public HandTrackingStatusEnum HandTrackingStatusEnum { get; set; }
        [JsonProperty("bse")]
        public BodySideEnum BodySideEnum { get; set; }
        [JsonProperty("o")]
        public int Openness { get; set; }
        [JsonProperty("pri")]
        public Single PalmRadiusImage { get; set; }
        [JsonProperty("prw")]
        public Single PalmRadiusWorld { get; set; }
        [JsonProperty("bb")]
        public BoundingBox BoundingBox { get; set; }
        [JsonProperty("mci")]
        public MassCenterImage MassCenterImage { get; set; }
        [JsonProperty("mcw")]
        public MassCenterWorld MassCenterWorld { get; set; }
        [JsonProperty("po")]
        public PalmOrientation PalmOrientation { get; set; }
        [JsonProperty("jd")]
        [BsonSerializer(typeof(JointDataSerializer))]
        public IDictionary<JointEnum, JointData> JointDatas { get; set; }
        [JsonProperty("fd")]
        public ICollection<FingerData> FingerDatas { get; set; }

        public override string ToString()
        {
            return string.Format("HandEnum=[{0}]; TimeStamp=[{1}]; HandTrackingStatusEnum=[{2}]; BodySideEnum=[{3}]; Openness=[{4}]; PalmRadiusImage=[{5}]; PalmRadiusWorld=[{6}]; BoundingBox=[{7}]; MassCenterImage=[{8}]; MassCenterWorld=[{9}]; PalmOrientation=[{10}];",
                HandEnum, TimeStamp, HandTrackingStatusEnum, BodySideEnum, Openness, PalmRadiusImage, PalmRadiusWorld, BoundingBox, MassCenterImage, MassCenterWorld, PalmOrientation);
        }
    }
}
