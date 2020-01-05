// 
// commercio.sdk - Sdk for Commercio Network
//
// Riccardo Costacurta
// Dec. 30, 2019
// BlockIt s.r.l.
// 
// Interfaces for asymmetric public and private keys
// 

using System;
using System.Collections.Generic;
using System.Text;

namespace commercio.sdk
{
    public interface PrivateKey
    {
    }

    public interface PublicKey
    {
        byte[] getEncoded();
    }

}
