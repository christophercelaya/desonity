using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Desonity
{
    public class Profile
    {
        private string publicKey;

        public Profile(string PublicKeyBase58Check)
        {
            publicKey = PublicKeyBase58Check;
        }

        public IEnumerator getProfile(Action<string> onComplete)
        {
            string endpoint = "/get-single-profile";
            string postData = "{\"NoErrorOnMissing\":false,\"PublicKeyBase58Check\":\"" + publicKey + "\"}";

            yield return Route.POST(endpoint, postData, onComplete);
        }

        public IEnumerator getNftsForUser(Action<string> onComplete, string UserPublicKeyBase58Check = null,Nullable<bool> forSale = null)
        {
            var nft = new Nft(publicKey);
            if (UserPublicKeyBase58Check == null)
            {
                UserPublicKeyBase58Check = publicKey;
            }
            yield return nft.getNftsForUser(onComplete, UserPublicKeyBase58Check, forSale);
        }

        public string avatarUrl()
        {
            return Route.getRoute() + "/get-single-profile-picture/" + publicKey + "?fallback=https://bitclout.com/assets/img/default_profile_pic.png";
        }
    }
}
