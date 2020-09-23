// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
//
/// Contains all the data related to a document that is sent to the chain when
/// a user wants to share a document with another user.
//
using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace commercio.sdk
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommercioDocChecksumAlgorithm
    {
        // Be careful - if updated, also ParseCommercioDocChecksumAlgorithm needs to be aligned!
        [EnumMember(Value = "md5")]
        MD5,
        [EnumMember(Value = "sha-1")]
        SHA1,
        [EnumMember(Value = "sha-224")]
        SHA224,
        [EnumMember(Value = "sha-256")]     // RC 20200311 - Added checksum type per user request
        SHA256,
        [EnumMember(Value = "sha-384")]
        SHA384,
        [EnumMember(Value = "sha-512")]
        SHA512
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommercioSdnData
    {
        // Be careful - if updated, also ParseCommercioSdnData needs to be aligned!
        [EnumMember(Value = "common_name")]
        COMMON_NAME,
        [EnumMember(Value = "surname")]
        SURNAME,
        [EnumMember(Value = "serial_number")]
        SERIAL_NUMBER,
        [EnumMember(Value = "given_name")]
        GIVEN_NAME,
        [EnumMember(Value = "organization")]
        ORGANIZATION,
        [EnumMember(Value = "country")]
        COUNTRY
    }


    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class CommercioDoc
    {
        // Recover the enum from the string value - CommercioDocChecksumAlgorithm
        public static CommercioDocChecksumAlgorithm ParseCommercioDocChecksumAlgorithm(String algoritm)
        {
            CommercioDocChecksumAlgorithm res = CommercioDocChecksumAlgorithm.MD5;

            switch (algoritm)
            {
                case "md5":
                    res = CommercioDocChecksumAlgorithm.MD5;
                    break;
                case "sha-1":
                    res = CommercioDocChecksumAlgorithm.SHA1;
                    break;
                case "sha-224":
                    res = CommercioDocChecksumAlgorithm.SHA224;
                    break;
                case "sha-256":
                    res = CommercioDocChecksumAlgorithm.SHA256;
                    break;
                case "sha-384":
                    res = CommercioDocChecksumAlgorithm.SHA384;
                    break;
                case "sha-512":
                    res = CommercioDocChecksumAlgorithm.SHA512;
                    break;
                default:
                    // EXception - Unknown
                    System.ArgumentException argEx = new System.ArgumentException($"CommercioDocChecksumAlgorithm: unknown Algoritm '{algoritm}'");
                    throw argEx;
            }

            return res;
        }

        // Recover the enum from the string value - CommercioSdnData
        public static CommercioSdnData ParseCommercioSdnData(String snd)
        {
            CommercioSdnData res = CommercioSdnData.COMMON_NAME;

            switch (snd)
            {
                case "common_name":
                    res = CommercioSdnData.COMMON_NAME;
                    break;
                case "surname":
                    res = CommercioSdnData.SURNAME;
                    break;
                case "serial_number":
                    res = CommercioSdnData.SERIAL_NUMBER;
                    break;
                case "given_name":
                    res = CommercioSdnData.GIVEN_NAME;
                    break;
                case "organization":
                    res = CommercioSdnData.ORGANIZATION;
                    break;
                case "country":
                    res = CommercioSdnData.COUNTRY;
                    break;
                default:
                    // EXception - Unknown
                    System.ArgumentException argEx = new System.ArgumentException($"CommercioSdnData: unknown sndData '{snd}'");
                    throw argEx;
            }

            return res;
        }

        #region Properties
        [JsonProperty("sender", Order = 7)]
        public String senderDid { get; set; }
        [JsonProperty("recipients", Order = 6)]
        public List<String> recipientDids { get; set; }
        [JsonProperty("uuid", Order = 8)]
        public String uuid { get; set; }
        [JsonProperty("content_uri", Order = 2)]
        public String contentUri { get; set; }
        [JsonProperty("metadata", Order = 5)]
        public CommercioDocMetadata metadata { get; set; }
        [JsonProperty("checksum", Order = 1)]
        public CommercioDocChecksum checksum { get; set; }
        [JsonProperty("encryption_data", Order = 4)]
        public CommercioDocEncryptionData encryptionData { get; set; }
        [JsonProperty("do_sign", Order = 3)]
        public CommercioDoSign doSign { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDoc(String senderDid,
                            List<String> recipientDids,
                            String uuid,
                            CommercioDocMetadata metadata,
                            String contentUri = null,
                            CommercioDocChecksum checksum = null,
                            CommercioDocEncryptionData encryptionData = null,
                            CommercioDoSign doSign = null)
        {
            Trace.Assert(senderDid != null);
            Trace.Assert(recipientDids != null);
            Trace.Assert(recipientDids.Count > 0);
            Trace.Assert(uuid != null);
            Trace.Assert(metadata != null);
            // Trace.Assert(contentUri != null); Removed - conflict with opt param
            this.uuid = uuid;
            this.senderDid = senderDid;
            this.recipientDids = recipientDids;
            this.contentUri = contentUri;
            this.metadata = metadata;
            this.checksum = checksum;
            this.encryptionData = encryptionData;
            this.doSign = doSign;
        }

        // Alternate constructor from Json JObject
        public CommercioDoc(JObject json)
        {
            this.uuid = (String)json["uuid"];
            this.senderDid = (String)json["sender"];
            this.recipientDids = ((JArray)json["recipients"]).Select(elem => (elem.ToString())).ToList();
            this.contentUri = (String)json["content_uri"];
            this.metadata = new CommercioDocMetadata(json["metadata"] as JObject);
            this.checksum = new CommercioDocChecksum(json["checksum"] as JObject);
            this.encryptionData = new CommercioDocEncryptionData(json["encryption_data"] as JObject);
            this.doSign = new CommercioDoSign(json["do_sign"] as JObject);

            //Object outValue;
            //if (json.TryGetValue("sender", out outValue))
            //    this.senderDid = outValue as String;
            //if (json.TryGetValue("recipients", out outValue))
            //    this.recipientDids = outValue as List<String>;
            //if (json.TryGetValue("uuid", out outValue))
            //    this.uuid = outValue as String;
            //if (json.TryGetValue("content_uri", out outValue))
            //    this.contentUri = outValue as String;
            //if (json.TryGetValue("metadata", out outValue))
            //    this.metadata = outValue as CommercioDocMetadata;
            //if (json.TryGetValue("checksum", out outValue))
            //    this.checksum = outValue as CommercioDocChecksum;
            //if (json.TryGetValue("encryption_data", out outValue))
            //    this.encryptionData = outValue as CommercioDocEncryptionData;
            //if (json.TryGetValue("do_sign", out outValue))
            //    this.doSign = outValue as CommercioDoSign;
        }

        #endregion

        #region Public Methods

        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("sender", this.senderDid);
            output.Add("recipients", this.recipientDids);
            output.Add("uuid", this.uuid);
            output.Add("content_uri", this.contentUri);
            output.Add("metadata", this.metadata);
            output.Add("checksum", this.checksum);
            output.Add("encryption_data", this.encryptionData);
            output.Add("do_sign", this.doSign);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }

    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class CommercioDocMetadata
    {
        #region Properties
        [JsonProperty("content_uri", Order = 1)]
        public String contentUri { get; set; }
        [JsonProperty("schema", Order = 2)]
        public CommercioDocMetadataSchema schema { get; set; }
        [JsonProperty("schema_type", Order = 3)]
        public String schemaType { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDocMetadata(String contentUri,
                            CommercioDocMetadataSchema schema,
                            String schemaType = "")
        {
            Trace.Assert(contentUri != null);
            //Trace.Assert(schema != null);
            // Trace.Assert(!String.IsNullOrEmpty(schemaType));
            Trace.Assert(schemaType != null);
            Trace.Assert((schema != null) || (!String.IsNullOrEmpty(schemaType)));
            this.contentUri = contentUri;
            this.schema = schema;
            this.schemaType = schemaType;
        }

        // Alternate constructor from Json JObject
        public CommercioDocMetadata(JObject json)
        {
            this.contentUri = (String)json["content_uri"];
            this.schema = new CommercioDocMetadataSchema(json["schema"] as JObject);
            this.schemaType = (String)json["schema_type"];

            //Object outValue;
            //if (json.TryGetValue("content_uri", out outValue))
            //    this.contentUri = outValue as String;
            //if (json.TryGetValue("schema", out outValue))
            //    this.schema = outValue as CommercioDocMetadataSchema;
            //if (json.TryGetValue("schema_type", out outValue))
            //    this.schemaType = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("content_uri", this.contentUri);
            output.Add("schema", this.schema);
            output.Add("schema_type", this.schemaType);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }

    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class CommercioDocMetadataSchema
    {
        #region Properties
        [JsonProperty("uri", Order = 1)]
        public String uri { get; set; }
        [JsonProperty("version", Order = 2)]
        public String version { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDocMetadataSchema(String uri, String version)
        {
            Trace.Assert(uri != null);
            Trace.Assert(version != null);
            this.uri = uri;
            this.version = version;
        }

        // Alternate constructor from Json JObject
        public CommercioDocMetadataSchema(JObject json)
        {
            this.uri = (String)json["uri"];
            this.version = (String)json["version"];

            //Object outValue;
            //if (json.TryGetValue("uri", out outValue))
            //    this.uri = outValue as String;
            //if (json.TryGetValue("version", out outValue))
            //    this.version = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("uri", this.uri);
            output.Add("version", this.version);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }

    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class CommercioDocChecksum
    {
        #region Properties
        [JsonProperty("value", Order = 2)]
        public String value { get; set; }
        [JsonProperty("algorithm", Order = 1)]
        public CommercioDocChecksumAlgorithm algorithm { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDocChecksum(String value, CommercioDocChecksumAlgorithm algorithm)
        {
            Trace.Assert(value != null);
            // Trace.Assert(algorithm != null); Cannot be Null - it is an Enum! Different from Dart?
            this.value = value;
            this.algorithm = algorithm;
        }

        // Alternate constructor from Json JObject
        public CommercioDocChecksum(JObject json)
        {
            this.value = (String)json["value"];
            this.algorithm = CommercioDoc.ParseCommercioDocChecksumAlgorithm((String)json["algorithm"]);

            //Object outValue;
            //if (json.TryGetValue("value", out outValue))
            //    this.value = outValue as String;
            //if (json.TryGetValue("algorithm", out outValue))
            //    this.algorithm = (CommercioDocChecksumAlgorithm) outValue;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("value", this.value);
            output.Add("algorithm", this.algorithm);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }

    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class CommercioDocEncryptionData
    {
        #region Properties
        [JsonProperty("keys", Order = 2)]
        public List<CommercioDocEncryptionDataKey> keys { get; set; }
        [JsonProperty("encrypted_data", Order = 1)]
        public List<String> encryptedData { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDocEncryptionData(List<CommercioDocEncryptionDataKey> keys, List<String> encryptedData)
        {
            Trace.Assert(keys != null);
            Trace.Assert(encryptedData != null);
            this.keys = keys;
            this.encryptedData = encryptedData;
        }

        // Alternate constructor from Json Jobject
        public CommercioDocEncryptionData(JObject json)
        {
            this.keys = ((JArray)json["keys"]).Select(elem => (new CommercioDocEncryptionDataKey((JObject)elem))).ToList();
            this.encryptedData = ((JArray)json["encrypted_data"]).Select(elem => (elem.ToString())).ToList();

            //Object outValue;
            //if (json.TryGetValue("keys", out outValue))
            //    this.keys = outValue as List<CommercioDocEncryptionDataKey>;
            //if (json.TryGetValue("encrypted_data", out outValue))
            //    this.encryptedData = outValue as List<String>;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("keys", this.keys);
            output.Add("encrypted_data", this.encryptedData);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }

    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class CommercioDocEncryptionDataKey
    {
        #region Properties
        [JsonProperty("recipient", Order = 1)]
        public String recipientDid { get; set; }
        [JsonProperty("value", Order = 2)]
        public String value { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDocEncryptionDataKey(String recipientDid, String value)
        {
            Trace.Assert(recipientDid != null);
            Trace.Assert(value != null);
            this.recipientDid = recipientDid;
            this.value = value;
        }

        // Alternate constructor from Json JObject
        public CommercioDocEncryptionDataKey(JObject json)
        {
            this.recipientDid = (String)json["recipient"];
            this.value = (String)json["value"];

            //Object outValue;
            //if (json.TryGetValue("recipient", out outValue))
            //    this.recipientDid = outValue as String;
            //if (json.TryGetValue("value", out outValue))
            //    this.value = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("recipient", this.recipientDid);
            output.Add("value", this.value);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }

    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class CommercioDoSign
    {
        #region Properties
        [JsonProperty("storage_uri", Order = 4)]
        public String storageUri { get; set; }
        [JsonProperty("signer_instance", Order = 3)]
        public String signerInstance { get; set; }
        [JsonProperty("sdn_data", Order = 2)]
        public List<CommercioSdnData> sdnData { get; set; }
        [JsonProperty("vcr_id", Order = 5)]
        public String vcrId { get; set; }
        [JsonProperty("certificate_profile", Order = 1)]
        public String certificateProfile { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDoSign(String storageUri, String signerInstance, List<CommercioSdnData> sdnData = null, String vcrId = null, String certificateProfile = null)
        {
            Trace.Assert(storageUri != null);
            Trace.Assert(signerInstance != null);
            // Trace.Assert(vcrId != null); // RC 20200508 - Commented this, as it conflicts with optional param - different from Dart version, maybe a bug there.
            this.storageUri = storageUri;
            this.signerInstance = signerInstance;
            this.sdnData = sdnData;
            this.vcrId = vcrId;
            this.certificateProfile = certificateProfile;
        }

        // Alternate constructor from Json JObject
        public CommercioDoSign(JObject json)
        {
            this.storageUri = (String)json["storage_uri"];
            this.signerInstance = (String)json["signer_instance"];
            this.sdnData = ((JArray)json["sdn_data"]).Select(elem => (CommercioDoc.ParseCommercioSdnData(elem.ToString()))).ToList();
            this.vcrId = (String)json["vcr_id"];
            this.certificateProfile = (String)json["certificate_profile"];

            //Object outValue;
            //if (json.TryGetValue("storage_uri", out outValue))
            //    this.storageUri = outValue as String;
            //if (json.TryGetValue("signer_instance", out outValue))
            //    this.signerInstance = outValue as String;
            //if (json.TryGetValue("sdn_data", out outValue))
            //    this.sdnData = outValue as List<CommercioSdnData>;
            //if (json.TryGetValue("vcr_id", out outValue))
            //    this.vcrId = vcrId as String;
            //if (json.TryGetValue("certificate_profile", out outValue))
            //    this.certificateProfile = outValue as String;
        }

        #endregion

        #region Public Methods
        public Dictionary<String, Object> toJson()
        {
            Dictionary<String, Object> output;

            output = new Dictionary<String, Object>();
            output.Add("storage_uri", this.storageUri);
            output.Add("signer_instance", this.signerInstance);
            output.Add("sdn_data", this.sdnData);
            output.Add("vcr_id", this.vcrId);
            output.Add("certificate_profile", this.certificateProfile);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
