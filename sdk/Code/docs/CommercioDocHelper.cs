// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 11, 2020
// BlockIt s.r.l.
// 
/// Allows to easily create a CommercioDoc and perform common related operations
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Newtonsoft.Json;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class CommercioDocHelper
    {
        #region Instance Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Creates a CommercioDoc from the given [wallet],
        /// [recipients], [id], [metadata]
        /// and optionally [contentUri], [checksum],
        /// [doSign], [encryptedData], [aesKey].
        public static async Task<CommercioDoc> fromWallet(
            Wallet wallet,
            List<String> recipients,
            String id,
            CommercioDocMetadata metadata,
            String contentUri = null,
            CommercioDocChecksum checksum = null,
            CommercioDoSign doSign = null,
            List<EncryptedData> encryptedData = null,
            KeyParameter aesKey = null
        )
        {
            // Build a generic document
            CommercioDoc commercioDocument = new CommercioDoc(
                senderDid: wallet.bech32Address,
                recipientDids: recipients,
                uuid: id,
                contentUri: contentUri,
                metadata: metadata,
                checksum: checksum,
                encryptionData: null,
                doSign: doSign
            );

            // Encrypt its contents, if necessary
            if ((encryptedData != null) && (encryptedData.Count > 0))
            {
                // Get a default aes key for encryption if needed
                if (aesKey == null)
                {
                    aesKey = KeysHelper.generateAesKey();
                }


                // Encrypt its contents, if necessary
                commercioDocument = await DocsUtils.encryptField(
                    commercioDocument,
                    aesKey,
                    encryptedData,
                    recipients,
                    wallet
                );
            }
            return commercioDocument;
        }

        #endregion

        #region Helpers
        #endregion
    }
}
