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
    // public class KeyPair<P, S> where P : PublicKey where S : PrivateKey
    public class KeyPair
    {
        public PublicKey publicKey { get; set; }
        public PrivateKey privateKey { get; set; }

        public KeyPair(PublicKey P, PrivateKey S)
        {
            this.publicKey = P;
            this.privateKey = S;
        }
    }
}
