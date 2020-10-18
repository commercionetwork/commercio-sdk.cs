// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public enum EncryptedData
    {

        [Description("content_uri")]
        CONTENT_URI,
        [Description("metadata.content_uri")]
        METADATA_CONTENT_URI,
        [Description("metadata.schema.uri")]
        METADATA_SCHEMA_URI
    };

    public class DocsUtils
    {
        /// Represents a pair that associates a Did document to its encryption key.
        /// This class is internal to DocsUtils
        private class _Pair
        {
            public DidDocument document;
            public RSAPublicKey pubKey;

            public _Pair(DidDocument first, RSAPublicKey second)
            {
                this.document = first;
                this.pubKey = second;
            }

            /// creates a _Pair from a didDocument. *** Refactor first and second to better names
            public _Pair(DidDocument didDocument)
            {
                RSAPublicKey key = didDocument.encryptionKey();
                this.document = didDocument;
                this.pubKey = key;
            }

        }

        // Here we have to address the Dart direct methods - to be encapsulated in a class in C#
        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        /// Transforms [this] document into one having the proper fields encrypted as
        /// specified inside the [encryptedData] list.
        /// All the fields will be encrypted using the specified [aesKey].
        /// This key will later be encrypted for each and every Did specified into
        /// the [recipients] list.
        /// The overall encrypted data will be put inside the proper document field.
        public static async Task<CommercioDoc> encryptField(
            CommercioDoc doc,
            KeyParameter aesKey,
            List<EncryptedData> encryptedData,
            List<String> recipients,
            Wallet wallet
        )
        {
            // -----------------
            // --- Encryption
            // -----------------

            // Encrypt the contents
            String encryptedContentUri = null;
            if (encryptedData.Contains(EncryptedData.CONTENT_URI))
            {
                encryptedContentUri = HexEncDec.ByteArrayToString(EncryptionHelper.encryptStringWithAes(doc.contentUri, aesKey));
            }

            String encryptedMetadataContentUri = null;
            if (encryptedData.Contains(EncryptedData.METADATA_CONTENT_URI))
            {
                encryptedMetadataContentUri = HexEncDec.ByteArrayToString(EncryptionHelper.encryptStringWithAes(doc.metadata.contentUri, aesKey));
            }

            String encryptedMetadataSchemaUri = null;
            if (encryptedData.Contains(EncryptedData.METADATA_SCHEMA_URI))
            {
                String schemaUri = doc.metadata.schema?.uri;
                if (schemaUri != null)
                {
                    encryptedMetadataSchemaUri = HexEncDec.ByteArrayToString(EncryptionHelper.encryptStringWithAes(schemaUri, aesKey));
                }
            }

            // ---------------------
            // --- Keys creation
            // ---------------------

            // I will trasform all the Dart function instructions in imperative loops
            // Get the recipients Did Documents
            List<DidDocument> recipientsDidDocs = new List<DidDocument>();
            foreach (String recipient in recipients)
            {
                recipientsDidDocs.Add(await IdHelper.getDidDocument(recipient, wallet));
            }

            // Get a list of al the Did Documents and the associated encryption key
            List<_Pair> keys = new List<_Pair>();
            foreach(DidDocument didDoc in recipientsDidDocs)
            {
                if (didDoc != null)
                {
                    _Pair p = new _Pair(didDoc);
                    if (p.pubKey != null)
                    {
                        keys.Add(p);
                    }
                }
            }

            // Create the encryption key field
            List<CommercioDocEncryptionDataKey> encryptionKeys = new List<CommercioDocEncryptionDataKey>();
            foreach (_Pair pair in keys)
            {
                byte[] encryptedAesKey = EncryptionHelper.encryptBytesWithRsa(
                    aesKey.GetKey(),
                    pair.pubKey
                );
                CommercioDocEncryptionDataKey dataKey = new CommercioDocEncryptionDataKey(
                    recipientDid: pair.document.id,
                    value: HexEncDec.ByteArrayToString(encryptedAesKey)
                );
                encryptionKeys.Add(dataKey);
            }

            // Copy the metadata
            CommercioDocMetadataSchema metadataSchema = doc.metadata?.schema;
            if (metadataSchema != null)
            {
                metadataSchema = new CommercioDocMetadataSchema(
                    version: metadataSchema.version,
                    uri: encryptedMetadataSchemaUri ?? metadataSchema.uri
                );
            }

            // Return a copy of the document
            return new CommercioDoc(
                senderDid: doc.senderDid,
                recipientDids: doc.recipientDids,
                uuid: doc.uuid,
                checksum: doc.checksum,
                contentUri: encryptedContentUri ?? doc.contentUri,
                metadata: new CommercioDocMetadata(
                    contentUri: encryptedMetadataContentUri ?? doc.metadata.contentUri,
                    schema: metadataSchema,
                    schemaType: doc.metadata.schemaType
                ),
                encryptionData: new CommercioDocEncryptionData(
                    keys: encryptionKeys,
                    encryptedData: encryptedData.Select((e) => e.ToEnumMemberAttrValue()).ToList()
                )
            );
        }

        #endregion

        #region Helpers


        #endregion
    }
}
