// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Sep. 11, 2020
// BlockIt s.r.l.
// 
/// Allows to easily create a CommercioDocReceipt
/// and perform common related operations
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

    public class CommercioDocReceiptHelper
    {
        #region Instance Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        /// Creates a CommercioDoc from the given [wallet],
        /// [recipient], [txHash], [documentId]
        /// and optionally [proof].
        public static CommercioDocReceipt fromWallet(
            Wallet wallet,
            String recipient,
            String txHash,
            String documentId,
            String proof = ""
        )
        {
            return new CommercioDocReceipt(
                uuid: Guid.NewGuid().ToString(), // *** This should be equivalent to Uuid().v4() in Dart
                senderDid: wallet.bech32Address,
                recipientDid: recipient,
                txHash: txHash,
                documentUuid: documentId,
                proof: proof
            );

        }

        #endregion

        #region Helpers
        #endregion
    }
}
