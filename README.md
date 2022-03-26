# Deso + Unity = Desonity

A Unity3D package for interacting with the DeSo blockchain 💎

## See [Installation](Installation/install.md) to get started

## Examples

To use Desonity you must import `Desonity` namespace

```cs
using UnityEngine;
using Desonity;
```

### Login with Deso Identity

You will need to setup your own backend with python or javascript that will handle Deso identity.

The Unity project will require the url for the backend and a friendly name for your project which will be shown to users during login.

Checkout example python flask webapp [here](Login%20Backend%20Flask)

```cs
string appName = "My App";
string backendUrl = "http://localhost:5000"; // either localhost or domain url

var login = new Desonity.Identity(appName,backendUrl);

// This will open the default browser and prompt the user to login with level 2 access
StartCoroutine(login.getLogin(
    onComplete: res => {
        Debug.Log (res); // res will be the public key of the logged in account
    }
));
```

### Getting a Profile from Public Key

```cs
string publicKey = "BC1YLhF5DHfgqM7rwYK8PVnfKDmMXyVeQqJyeJ8YGsmbVb14qTm123G";

var profile = new Desonity.Profile(publicKey);

StartCoroutine(profile.getProfile(
    // any action related data returned from deso must be done within this block of code
    onComplete: res => { Debug.Log(res); }
));
```

### Getting NFT data

```cs
string readerKey = "BC1YLhF5DHfgqM7rwYK8PVnfKDmMXyVeQqJyeJ8YGsmbVb14qTm123G";
string nftPostHashHex = "3a8cd1ae7d727247b95b6d1e0baf0b6f2ddcb992cb72d113548a3e504b707526";

var nft = new Desonity.Nft(readerKey);

StartCoroutine(nft.getSingleNft(
    PostHashHex: nftPostHashHex,
    onComplete: res => { Debug.Log(res); }
));
```

### Getting owned NFTs of a public key

```cs
var nft = new Desonity.Nft(readerKey);

StartCoroutine(profile.getNftsForUser(
    UserPublicKeyBase58Check: "a deso public key",
    onComplete: s => { Debug.Log(s); }
));
```

OR

```cs
string myKey = "BC1YLhF5DHfgqM7rwYK8PVnfKDmMXyVeQqJyeJ8YGsmbVb14qTm123G";

var profile = new Desonity.Profile(myKey);

StartCoroutine(profile.getNftsForUser(
    UserPublicKeyBase58Check: "Someones Public Key", // No need to pass this If you want NFTs for `myKey` 
    onComplete: s => { Debug.Log(s); }
));
```

## Important

Any method starting with `get` is an `IEnumerator` and must be called through `StartCoroutine(nameOfTheMethod())`

**`get` methods return a JSON string which the users need to pars on their own as of version `1.0.0`**

Any method **not** starting with a `get` can be called like a normal method, e.g.

```cs
Debug.Log(profile.avatarUrl());
// returns avatar image url for a public key
```

## Thank You!

If you are using Desonity to build something consider supporting me on Deso [@weeblet](https://diamondapp.com/u/weeblet) and feel free to show me your creative implementaitons of Desonity ;)
