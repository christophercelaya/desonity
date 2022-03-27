using System;
using System.Collections;
using UnityEngine;

namespace Desonity.Endpoints
{
    [Serializable]
    public class getSingleProfile
    {
        public bool NoErrorOnMissing = false;
        public string PublicKeyBase58Check { get; set; }
    }

    [Serializable]
    public class getNftEntriesForNftPost
    {
        public string PostHashHex { get; set; }
        public string ReaderPublicKeyBase58Check { get; set; }

    }

    [Serializable]
    public class getNftsForUser
    {
        public string ReaderPublicKeyBase58Check { get; set; }
        public string UserPublicKeyBase58Check { get; set; }
        public Nullable<bool> IsForSale { get; set; }
    }

}