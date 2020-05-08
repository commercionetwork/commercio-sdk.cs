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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace commercio.sdk
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommercioDocChecksumAlgorithm
    {
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
            if (json.TryGetValue("do_sign", out outValue))
                this.doSign = outValue as CommercioDoSign;
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
                            String schemaType)
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
        [JsonProperty("vcr_Id", Order = 5)]
        public String vcrId { get; set; }
        [JsonProperty("certificate_Profile", Order = 1)]
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

        // Alternate constructor from Json Dictionary
        public CommercioDoSign(Dictionary<String, Object> json)
        {
            Object outValue;
            if (json.TryGetValue("storage_uri", out outValue))
                this.storageUri = outValue as String;
            if (json.TryGetValue("signer_instance", out outValue))
                this.signerInstance = outValue as String;
            if (json.TryGetValue("sdn_data", out outValue))
                this.sdnData = outValue as List<CommercioSdnData>;
            if (json.TryGetValue("vcr_Id", out outValue))
                this.vcrId = vcrId as String;
            if (json.TryGetValue("certificate_Profile", out outValue))
                this.certificateProfile = outValue as String;
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
            output.Add("vcr_Id", this.vcrId);
            output.Add("certificate_Profile", this.certificateProfile);
            return (output);
        }

        #endregion

        #region Helpers
        #endregion
    }
}
