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
using System.Linq;
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

        /// Creates a Did Document from the given [wallet], [pubKeys] and optional [service].
        public static DidDocument fromWallet(Wallet wallet, List<PublicKey> pubKeys, List<DidDocumentService> service = null)
        {
            if (pubKeys.Count < 2)
            {
                System.ArgumentException argEx = new System.ArgumentException("Argument Exception: DidDocument fromWallet - At least two keys are required ");
                throw argEx;
            }

            //String authKeyId = $"{wallet.bech32Address}#keys-1"; // *** CHeck this !!!
            //DidDocumentPublicKey authKey = new DidDocumentPublicKey(
            //    id: authKeyId,
            //    type: DidDocumentPubKeyType.SECP256K1,
            //    controller: wallet.bech32Address,
            //    publicKeyHex: HexEncDec.ByteArrayToString(wallet.publicKey)
            //);

            //final otherKeys = mapIndexed(
            //    pubKeys, (index, item) => _convertKey(item, index + 2, wallet))
            //    .toList();
            // No need to have an util here, I think Linq will do
            List<DidDocumentPublicKey> otherKeys = pubKeys.Select(item => _convertKey(item, pubKeys.IndexOf(item) + 1, wallet)).ToList();

            DidDocumentProofSignatureContent proofContent = new DidDocumentProofSignatureContent(
                context: "https://www.w3.org/ns/did/v1",
                id: wallet.bech32Address,
                publicKeys: otherKeys
            );

            String verificationMethod = wallet.bech32PublicKey;

            DidDocumentProof proof = _computeProof(proofContent.id, verificationMethod, proofContent, wallet);

            return new DidDocument(
                context: proofContent.context,
                id: proofContent.id,
                publicKeys: proofContent.publicKeys,
                proof: proof,
                service: service
            );
        }

        /// Converts the given [pubKey] into a [DidDocumentPublicKey] placed at position [index],
        /// [wallet] used to get the controller field of each [DidDocumentPublicKey].
        private static DidDocumentPublicKey _convertKey(PublicKey pubKey, int index, Wallet wallet)
        {
            return new DidDocumentPublicKey(
                id: $"{wallet.bech32Address}#keys-{index}", // *** CHeck this !!!
                type: pubKey.getType(),
                controller: wallet.bech32Address,
                publicKeyPem: pubKey.getEncoded()
            );
        }

        /// Computes the [DidDocumentProof] based on the given [controller], [verificationMethod] and [proofSignatureContent]
        static DidDocumentProof _computeProof(
            String controller, 
            String verificationMethod, 
            DidDocumentProofSignatureContent proofSignatureContent, 
            Wallet wallet, 
            String proofPurpose = "authentication")
        {

            return new DidDocumentProof(
              type: "EcdsaSecp256k1VerificationKey2019",
              timestamp: GenericUtils.getTimeStamp(),
              proofPurpose: proofPurpose,
              controller: controller,
              verificationMethod: verificationMethod,
              signatureValue: Convert.ToBase64String(SignHelper.signSorted(proofSignatureContent.toJson(), wallet))
            );
        }

    #endregion

    #region Helpers
    #endregion

    }
}
