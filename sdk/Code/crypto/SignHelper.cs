﻿// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to easily perform signature-related operations.
//
using System;
using System.Text;
using System.Collections.Generic;
using commercio.sacco.lib;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;


namespace commercio.sdk
{
    public class SignHelper
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Takes the given [data], converts it to an alphabetically sorted
        /// JSON object and signs its content using the given [wallet].
        public static byte[] signSorted(Object data, Wallet wallet)
        {
            Dictionary<String, Object> sorted = null;
            if (data is Dictionary<String, Object>)
            {
                sorted = MapSorter.sort((Dictionary < String, Object >) data);
            }
            // Encode the sorted JSON to a string
            // String jsonData = JsonConvert.SerializeObject(sorted);
            String jsonData = JsonConvert.SerializeObject(data);
            // Create a Sha256 of the message
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(jsonData);
            // Sign and return the message
            return wallet.sign(utf8Bytes);
        }

        /// Takes [senderDid], [pairwiseDid], [timestamp] and:
        /// 1. Concatenate senderDid, pairwiseDid and timestamp as payload
        /// 2. Returns the RSA PKCS1v15 (the SHA256 digest is calculated inside the
        ///    signer) and sign using the [rsaPrivateKey]
        public static byte[] signPowerUpSignature(String senderDid, String pairwiseDid, String timestamp, RSAPrivateKey rsaPrivateKey)
        {
            String concat = senderDid + pairwiseDid + timestamp;

            ISigner sig = SignerUtilities.GetSigner("SHA256WithRSA");
            // Populate key 
            sig.Init(true, rsaPrivateKey.secretKey);
            // Get the bytes to be signed from the string
            byte[] buffer = Encoding.UTF8.GetBytes(concat);
            // Calc the signature 
            sig.BlockUpdate(buffer, 0, buffer.Length);
            byte[] signature = sig.GenerateSignature();
            return signature;
        }

        #endregion

        #region Helpers
        #endregion

    }
}
