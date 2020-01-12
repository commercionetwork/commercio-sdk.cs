// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Allows to easily create a Did Document and perform common related operations
//
using System;
using System.Collections.Generic;
using System.Text;
using commercio.sacco.lib;

namespace commercio.sdk
{
    public class DidDocumentHelper
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        public static DidDocument fromWallet(Wallet wallet, List<PublicKey> pubKeys)
        {
            String authKeyId = $"{wallet.bech32Address}#keys-1";
            DidDocumentPublicKey authKey = new DidDocumentPublicKey(
                id: authKeyId,
                type: DidDocumentPubKeyType.SECP256K1,
                controller: wallet.bech32Address,
                publicKeyHex: HexEncDec.ByteArrayToString(wallet.publicKey)
            );

            //final otherKeys = mapIndexed(
            //    pubKeys, (index, item) => _convertKey(item, index + 2, wallet))
            //    .toList();

            DidDocumentProofSignatureContent proofContent = new DidDocumentProofSignatureContent(
                context: "https://www.w3.org/ns/did/v1",
                did: wallet.bech32Address,
                publicKeys: new List<DidDocumentPublicKey> { authKey }, // + otherKeys, *** To Be Reviewed
                authentication: new List<string> { authKeyId }
            );

            DidDocumentProof proof = _computeProof(authKeyId, proofContent, wallet);

            return new DidDocument(
                context: proofContent.context,
                id: proofContent.did,
                publicKeys: proofContent.publicKeys,
                authentication: proofContent.authentication,
                proof: proof,
                services: null
            );
        }

        /// Converts the given [pubKey] into a [DidDocumentPublicKey] placed at position [index],
        /// [wallet] used to get the controller field of each [DidDocumentPublicKey].
        static DidDocumentPublicKey _convertKey(PublicKey pubKey, int index, Wallet wallet)
        {
            DidDocumentPubKeyType keyType = new DidDocumentPubKeyType();
            if (pubKey is RSAPublicKey)
            {
                keyType = DidDocumentPubKeyType.RSA;
            }
            else if (pubKey is ECPublicKey)
            {
                keyType = DidDocumentPubKeyType.SECP256K1;
            }
            else if (pubKey is Ed25519PublicKey)
            {
                keyType = DidDocumentPubKeyType.ED25519;
            }

            return new DidDocumentPublicKey(
                id: $"{wallet.bech32Address}#keys-$index",
                type: keyType,
                controller: wallet.bech32Address,
                publicKeyHex: HexEncDec.ByteArrayToString(pubKey.getEncoded())
            );
        }

        /// Computes the [DidDocumentProof] based on the given [authKeyId] and [proofSignatureContent]
        static DidDocumentProof _computeProof(String authKeyId, DidDocumentProofSignatureContent proofSignatureContent, Wallet wallet)
        {
            return new DidDocumentProof(
                type: "LinkedDataSignature2015",
                iso8601creationTimestamp: System.DateTime.UtcNow.ToString("o"), // This get a Iso8601 Time stamp - to be checked
                creatorKeyId: authKeyId,
                signatureValue: HexEncDec.ByteArrayToString(SignHelper.signSorted(proofSignatureContent.toJson(), wallet))
            );
        }
        
        #endregion

        #region Helpers
        #endregion

        /*
        class DidDocumentHelper {
          /// Creates a Did Document from the given [wallet] and optional [pubKeys].
          static DidDocument fromWallet(Wallet wallet, List<PublicKey> pubKeys) {
            final authKeyId = '${wallet.bech32Address}#keys-1';
            final authKey = DidDocumentPublicKey(
              id: authKeyId,
              type: DidDocumentPubKeyType.SECP256K1,
              controller: wallet.bech32Address,
              publicKeyHex: HEX.encode(wallet.publicKey),
            );

            final otherKeys = mapIndexed(
                    pubKeys, (index, item) => _convertKey(item, index + 2, wallet))
                .toList();

            final proofContent = DidDocumentProofSignatureContent(
              context: "https://www.w3.org/ns/did/v1",
              did: wallet.bech32Address,
              publicKeys: [authKey] + otherKeys,
              authentication: [authKeyId],
            );

            final proof = _computeProof(authKeyId, proofContent, wallet);

            return DidDocument(
              context: proofContent.context,
              id: proofContent.did,
              publicKeys: proofContent.publicKeys,
              authentication: proofContent.authentication,
              proof: proof,
              services: null,
            );
          }

          /// Converts the given [pubKey] into a [DidDocumentPublicKey] placed at position [index],
          /// [wallet] used to get the controller field of each [DidDocumentPublicKey].
          static DidDocumentPublicKey _convertKey(
              PublicKey pubKey, int index, Wallet wallet) {
            var keyType;
            if (pubKey is RSAPublicKey) {
              keyType = DidDocumentPubKeyType.RSA;
            } else if (pubKey is ECPublicKey) {
              keyType = DidDocumentPubKeyType.SECP256K1;
            } else if (pubKey is Ed25519PublicKey) {
              keyType = DidDocumentPubKeyType.ED25519;
            }

            return DidDocumentPublicKey(
              id: '${wallet.bech32Address}#keys-$index',
              type: keyType,
              controller: wallet.bech32Address,
              publicKeyHex: HEX.encode(pubKey.getEncoded()),
            );
          }

          /// Computes the [DidDocumentProof] based on the given [authKeyId] and [proofSignatureContent]
          static DidDocumentProof _computeProof(
            String authKeyId,
            DidDocumentProofSignatureContent proofSignatureContent,
            Wallet wallet,
          ) {
            return DidDocumentProof(
              type: "LinkedDataSignature2015",
              iso8601creationTimestamp: getTimeStamp(),
              creatorKeyId: authKeyId,
              signatureValue: HEX.encode(
                SignHelper.signSorted(proofSignatureContent.toJson(), wallet),
              ),
            );
          }
        }

         */
    }
}
