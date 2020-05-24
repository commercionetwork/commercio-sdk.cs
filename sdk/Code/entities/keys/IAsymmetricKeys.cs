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
using System.Text;
using System.Collections.Generic;

namespace commercio.sdk
{
    public interface PrivateKey
    {
    }

    public interface PublicKey
    {
        String getEncoded();
        String getType();
    }

}
