using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;

namespace Desonity
{
    public class Nft
    {
        string JSONStr;

        private string postHash;
        private string readerKey;
        private string seedHex;
        public Nft(string ReaderPublicKeyBase58Check)
        {
            readerKey = ReaderPublicKeyBase58Check;
        }

        public Nft(string ReaderPublicKeyBase58Check, string SeedHex)
        {
            readerKey = ReaderPublicKeyBase58Check;
            seedHex = SeedHex;
        }

        public IEnumerator getSingleNft(Action<string> onComplete, string PostHashHex)
        {
            string endpoint = "/get-nft-entries-for-nft-post";
            var endpointClass = new Endpoints.getNftEntriesForNftPost
            {
                PostHashHex = PostHashHex,
                ReaderPublicKeyBase58Check = readerKey
            };
            string postData = JsonConvert.SerializeObject(endpointClass);

            yield return Route.POST(endpoint, postData, onComplete);
        }

        public IEnumerator getNftsForUser(Action<string> onComplete, string UserPublicKeyBase58Check, Nullable<bool> forSale = null)
        {

            // forSale:true  -> only nfts for sale
            // forSale:false -> only nfts not for sale
            // forSale:null  -> all owned nfts

            string endpoint = "/get-nfts-for-user";
            var endpointClass = new Endpoints.getNftsForUser
            {
                ReaderPublicKeyBase58Check = readerKey,
                UserPublicKeyBase58Check = UserPublicKeyBase58Check,
            };
            if (forSale.HasValue) { endpointClass.IsForSale = forSale; }
            string postData = JsonConvert.SerializeObject(endpointClass);
            yield return Route.POST(endpoint, postData, onComplete);
        }
        public IEnumerator mint()
        {
            yield return "OK";
        }
    }
}