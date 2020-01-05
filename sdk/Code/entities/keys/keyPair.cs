// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
//

using System;

namespace commercio.sdk
{
    public class KeyPair
    {
        public PublicKey publicKey { get; set; }
        public PrivateKey privateKey { get; set; }

        KeyPair(PublicKey P, PrivateKey S)
        {
            this.publicKey = P;
            this.privateKey = S;
        }
    }
}
