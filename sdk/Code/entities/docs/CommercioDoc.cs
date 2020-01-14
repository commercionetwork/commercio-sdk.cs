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
using Newtonsoft.Json;

namespace commercio.sdk
{
    public enum CommercioDocChecksumAlgorithm
    {
        [JsonProperty("md5")]
        MD5,
        [JsonProperty("sha-1")]
        SHA1,
        [JsonProperty("sha-224")]
        SHA224,
        [JsonProperty("sha-384")]
        SHA384,
        [JsonProperty("sha-512")]
        SHA512
    }

    // *** This is inherited by Equatable in Dart Package!
    //  There is no such Class in C# - we include Compare-Net-Objects Nuget package for the purpose - see https://github.com/GregFinzer/Compare-Net-Objects
    public class CommercioDoc
    {
        #region Properties
        [JsonProperty("sender")]
        public String senderDid { get; set; }
        [JsonProperty("recipients")]
        public List<String> recipientDids { get; set; }
        [JsonProperty("uuid")]
        public String uuid { get; set; }
        [JsonProperty("content_uri")]
        public String contentUri { get; set; }
        [JsonProperty("metadata")]
        public CommercioDocMetadata metadata { get; set; }
        [JsonProperty("checksum")]
        public CommercioDocChecksum checksum { get; set; }
        [JsonProperty("encryption_data")]
        public CommercioDocEncryptionData encryptionData { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDoc(String uuid,
                            String senderDid,
                            List<String> recipientDids, 
                            String contentUri,
                            CommercioDocMetadata metadata,
                            CommercioDocChecksum checksum,
                            CommercioDocEncryptionData encryptionData)
        {
            Trace.Assert(uuid != null);
            Trace.Assert(senderDid != null);
            Trace.Assert(recipientDids != null);
            Trace.Assert(recipientDids.Count > 0);
            Trace.Assert(contentUri != null);
            Trace.Assert(metadata != null);
            this.uuid = uuid;
            this.senderDid = senderDid;
            this.recipientDids = recipientDids;
            this.contentUri = contentUri;
            this.metadata = metadata;
            this.checksum = checksum;
            this.encryptionData = encryptionData;
        }

        // Alternate constructor from Json Dictionary
        public CommercioDoc(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("sender", out outValue))
                this.senderDid = outValue as String;
            if (json.TryGetValue("recipients", out outValue))
                this.recipientDids = outValue as List<String>;
            if (json.TryGetValue("uuid", out outValue))
                this.uuid = outValue as String;
            if (json.TryGetValue("content_uri", out outValue))
                this.contentUri = outValue as String;
            if (json.TryGetValue("metadata", out outValue))
                this.metadata = outValue as CommercioDocMetadata;
            if (json.TryGetValue("checksum", out outValue))
                this.checksum = outValue as CommercioDocChecksum;
            if (json.TryGetValue("encryption_data", out outValue))
                this.encryptionData = outValue as CommercioDocEncryptionData;
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
        [JsonProperty("content_uri")]
        public String contentUri { get; set; }
        [JsonProperty("schema")]
        public CommercioDocMetadataSchema schema { get; set; }
        [JsonProperty("schema_type")]
        public String schemaType { get; set; }

        #endregion

        #region Constructors
        [JsonConstructor]
        public CommercioDocMetadata(String contentUri,
                            CommercioDocMetadataSchema schema,
                            String schemaType)
        {
            Trace.Assert(contentUri != null);
            Trace.Assert(schema != null);
            Trace.Assert(!String.IsNullOrEmpty(schemaType));
            this.contentUri = contentUri;
            this.schema = schema;
            this.schemaType = schemaType;
        }

        // Alternate constructor from Json Dictionary
        public CommercioDocMetadata(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("content_uri", out outValue))
                this.contentUri = outValue as String;
            if (json.TryGetValue("schema", out outValue))
                this.schema = outValue as CommercioDocMetadataSchema;
            if (json.TryGetValue("schema_type", out outValue))
                this.schemaType = outValue as String;
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
        [JsonProperty("uri")]
        public String uri { get; set; }
        [JsonProperty("version")]
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

        // Alternate constructor from Json Dictionary
        public CommercioDocMetadataSchema(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("uri", out outValue))
                this.uri = outValue as String;
            if (json.TryGetValue("version", out outValue))
                this.version = outValue as String;
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
        [JsonProperty("value")]
        public String value { get; set; }
        [JsonProperty("algorithm")]
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

        // Alternate constructor from Json Dictionary
        public CommercioDocChecksum(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("value", out outValue))
                this.value = outValue as String;
            if (json.TryGetValue("algorithm", out outValue))
                this.algorithm = (CommercioDocChecksumAlgorithm) outValue;
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
        [JsonProperty("keys")]
        public List<CommercioDocEncryptionDataKey> keys { get; set; }
        [JsonProperty("encrypted_data")]
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

        // Alternate constructor from Json Dictionary
        public CommercioDocEncryptionData(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("keys", out outValue))
                this.keys = outValue as List<CommercioDocEncryptionDataKey>;
            if (json.TryGetValue("encrypted_data", out outValue))
                this.encryptedData = outValue as List<String>;
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
        [JsonProperty("recipient")]
        public String recipientDid { get; set; }
        [JsonProperty("value")]
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

        // Alternate constructor from Json Dictionary
        public CommercioDocEncryptionDataKey(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("recipient", out outValue))
                this.recipientDid = outValue as String;
            if (json.TryGetValue("value", out outValue))
                this.value = outValue as String;
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

    /*
    @JsonSerializable()
    class CommercioDoc extends Equatable {
      @JsonKey(name: "sender")
      final String senderDid;

      @JsonKey(name: "recipients")
      final List<String> recipientDids;

      @JsonKey(name: "uuid")
      final String uuid;

      @JsonKey(name: "content_uri")
      final String contentUri;

      @JsonKey(name: "metadata")
      final CommercioDocMetadata metadata;

      @JsonKey(name: "checksum")
      final CommercioDocChecksum checksum;

      @JsonKey(name: "encryption_data")
      final CommercioDocEncryptionData encryptionData;

      CommercioDoc({
        @required this.uuid,
        @required this.senderDid,
        @required this.recipientDids,
        @required this.contentUri,
        @required this.metadata,
        @required this.checksum,
        @required this.encryptionData,
      })  : assert(senderDid != null),
            assert(recipientDids != null),
            assert(recipientDids.isNotEmpty),
            assert(uuid != null),
            assert(contentUri != null),
            assert(metadata != null);

      @override
      List<Object> get props {
        return [
          uuid,
          senderDid,
          recipientDids,
          contentUri,
          metadata,
          checksum,
          encryptionData,
        ];
      }

      factory CommercioDoc.fromJson(Map<String, dynamic> json) =>
          _$CommercioDocFromJson(json);

      Map<String, dynamic> toJson() => _$CommercioDocToJson(this);
    }

    @JsonSerializable()
    class CommercioDocMetadata extends Equatable {
      @JsonKey(name: "content_uri")
      final String contentUri;

      @JsonKey(name: "schema")
      final CommercioDocMetadataSchema schema;

      @JsonKey(name: "schema_type")
      final String schemaType;

      CommercioDocMetadata({
        @required this.contentUri,
        this.schema,
        this.schemaType = "",
      })  : assert(contentUri != null),
            assert(schemaType != null),
            assert(schema != null || schemaType.isNotEmpty);

      @override
      List<Object> get props {
        return [contentUri, schema, schemaType];
      }

      factory CommercioDocMetadata.fromJson(Map<String, dynamic> json) =>
          _$CommercioDocMetadataFromJson(json);

      Map<String, dynamic> toJson() => _$CommercioDocMetadataToJson(this);
    }

    @JsonSerializable()
    class CommercioDocMetadataSchema extends Equatable {
      @JsonKey(name: "uri")
      final String uri;

      @JsonKey(name: "version")
      final String version;

      CommercioDocMetadataSchema({
        @required this.uri,
        @required this.version,
      })  : assert(uri != null),
            assert(version != null);

      @override
      List<Object> get props {
        return [uri, version];
      }

      factory CommercioDocMetadataSchema.fromJson(Map<String, dynamic> json) =>
          _$CommercioDocMetadataSchemaFromJson(json);

      Map<String, dynamic> toJson() => _$CommercioDocMetadataSchemaToJson(this);
    }

    @JsonSerializable()
    class CommercioDocChecksum extends Equatable {
      @JsonKey(name: "value")
      final String value;

      @JsonKey(name: "algorithm")
      final CommercioDocChecksumAlgorithm algorithm;

      CommercioDocChecksum({
        @required this.value,
        @required this.algorithm,
      })  : assert(value != null),
            assert(algorithm != null);

      @override
      List<Object> get props {
        return [value, algorithm];
      }

      factory CommercioDocChecksum.fromJson(Map<String, dynamic> json) =>
          _$CommercioDocChecksumFromJson(json);

      Map<String, dynamic> toJson() => _$CommercioDocChecksumToJson(this);
    }

    enum CommercioDocChecksumAlgorithm {
      @JsonValue("md5")
      MD5,

      @JsonValue("sha-1")
      SHA1,

      @JsonValue("sha-224")
      SHA224,

      @JsonValue("sha-384")
      SHA384,

      @JsonValue("sha-512")
      SHA512,
    }

    @JsonSerializable()
    class CommercioDocEncryptionData extends Equatable {
      @JsonKey(name: "keys")
      final List<CommercioDocEncryptionDataKey> keys;

      @JsonKey(name: "encrypted_data")
      final List<String> encryptedData;

      CommercioDocEncryptionData({
        @required this.keys,
        @required this.encryptedData,
      })  : assert(keys != null),
            assert(encryptedData != null);

      List<Object> get props {
        return [keys, encryptedData];
      }

      factory CommercioDocEncryptionData.fromJson(Map<String, dynamic> json) =>
          _$CommercioDocEncryptionDataFromJson(json);

      Map<String, dynamic> toJson() => _$CommercioDocEncryptionDataToJson(this);
    }

    @JsonSerializable()
    class CommercioDocEncryptionDataKey extends Equatable {
      @JsonKey(name: "recipient")
      final String recipientDid;

      @JsonKey(name: "value")
      final String value;

      CommercioDocEncryptionDataKey({
        @required this.recipientDid,
        @required this.value,
      })  : assert(recipientDid != null),
            assert(value != null);

      List<Object> get props {
        return [recipientDid, value];
      }

      factory CommercioDocEncryptionDataKey.fromJson(Map<String, dynamic> json) =>
          _$CommercioDocEncryptionDataKeyFromJson(json);

      Map<String, dynamic> toJson() => _$CommercioDocEncryptionDataKeyToJson(this);
    }

    */
}
