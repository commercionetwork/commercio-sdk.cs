// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
/// Represents a document receipt that indicates that the document having
/// the given [documentUuid] present inside the transaction with has [txHash]
/// and sent by [recipientDid] has been received from the [senderDid].
//

using System;
using System.Collections.Generic;
using System.Text;

namespace commercio.sdk
{
    public class CommercioDocReceipt
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Helpers
        #endregion

        /*
        @JsonSerializable()
       class CommercioDocReceipt extends Equatable {
         @JsonKey(name: "uuid")
         final String uuid;

         @JsonKey(name: "sender")
         final String senderDid;

         @JsonKey(name: "recipient")
         final String recipientDid;

         @JsonKey(name: "tx_hash")
         final String txHash;

         @JsonKey(name: "document_uuid")
         final String documentUuid;

         /// Optional reading proof
         @JsonKey(name: "proof")
         final String proof;

         CommercioDocReceipt({
           @required this.uuid,
           @required this.senderDid,
           @required this.recipientDid,
           @required this.txHash,
           @required this.documentUuid,
           this.proof = "",
         })  : assert(uuid != null),
               assert(senderDid != null),
               assert(recipientDid != null),
               assert(txHash != null),
               assert(documentUuid != null),
               assert(proof != null);

         List<Object> get props {
           return [senderDid, recipientDid, txHash, documentUuid, proof];
         }

         factory CommercioDocReceipt.fromJson(Map<String, dynamic> json) =>
             _$CommercioDocReceiptFromJson(json);

         Map<String, dynamic> toJson() => _$CommercioDocReceiptToJson(this);
       }

        */
    }
}
