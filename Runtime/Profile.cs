using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;

namespace Desonity
{
    public class Profile
    {
        private string publicKey;
        private string seedHex;

        public Profile(string PublicKeyBase58Check)
        {
            publicKey = PublicKeyBase58Check;
        }

        public Profile(string PublicKeyBase58Check, string SeedHex)
        {
            publicKey = PublicKeyBase58Check;
            seedHex = SeedHex;
        }

        public IEnumerator getProfile(Action<string> onComplete)
        {
            string endpoint = "/get-single-profile";
            var endpointClass = new Endpoints.getSingleProfile
            {
                PublicKeyBase58Check = publicKey
            };
            string postData = JsonConvert.SerializeObject(endpointClass);

            yield return Route.POST(endpoint, postData, onComplete);
        }

        public IEnumerator getNftsForUser(Action<string> onComplete, string UserPublicKeyBase58Check = null, Nullable<bool> forSale = null)
        {
            // forSale:true  -> only nfts for sale
            // forSale:false -> only nfts not for sale
            // forSale:null  -> all owned nfts
            var nft = new Nft(publicKey);
            if (UserPublicKeyBase58Check == null) { UserPublicKeyBase58Check = publicKey; }
            yield return nft.getNftsForUser(onComplete, UserPublicKeyBase58Check, forSale);
        }

        // public IEnumerator createPost(string body, string jsonData, Action<string> onComplete)
        // {
        //     string endpoint = "/submit-post";
        //     string postData = "";
        //     yield return Route.POST(endpoint, postData, onComplete);
        // }

        public string avatarUrl()
        {
            return Route.getRoute() + "/get-single-profile-picture/" + publicKey + "?fallback=https://bitclout.com/assets/img/default_profile_pic.png";
        }
    }
}
