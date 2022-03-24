using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Desonity
{
    public class Nft
    {
        string JSONStr;

        private string postHash, readerKey;
        public Nft(string ReaderPublicKeyBase58Check)
        {
            readerKey = ReaderPublicKeyBase58Check;
        }

        public IEnumerator getSingleNft(Action<string> onComplete, string PostHashHex)
        {
            string endpoint = "/get-nft-entries-for-nft-post";
            string postData = "{\"PostHashHex\":\"" + PostHashHex + "\",\"ReaderPublicKeyBase58Check\":\"" + readerKey + "\"}";

            yield return Route.POST(endpoint, postData, onComplete);
        }

        public IEnumerator getNftsForUser(Action<string> onComplete, string UserPublicKeyBase58Check, Nullable<bool> forSale = null)
        {

            // forSale:true  -> only nfts for sale
            // forSale:false -> only nfts not for sale
            // forSale:null  -> all owned nfts

            string endpoint = "/get-nfts-for-user";
            string postData;
            if (forSale.HasValue)
            {
                postData = "{\"ReaderPublicKeyBase58Check\":\"" + readerKey + "\",\"UserPublicKeyBase58Check\":\"" + UserPublicKeyBase58Check + "\",\"IsForSale\":" + forSale.Value.ToString().ToLower() + "}";
            }
            else
            {
                postData = "{\"ReaderPublicKeyBase58Check\":\"" + readerKey + "\",\"UserPublicKeyBase58Check\":\"" + UserPublicKeyBase58Check + "\",\"IsForSale\": null}";
            }
            yield return Route.POST(endpoint, postData, onComplete);
        }
    }
}