# Deso + Unity = Desonity

A Unity3D package for interacting with the DeSo blockchain 💎

## See [Installation](Installation/install.md) to get started

## Examples

To use Desonity you must import `Desonity` namespace

```cs
using UnityEngine;
using Desonity;
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
string nftPostHashHex = "";

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
    UserPublicKeyBase58Check: anNftPostHash,
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

Any method starting with `get` is an `IEnumberator` and must be called through `StartCoroutine(nameOfTheMethod())`

**`get` methods return a JSON string which the users need to pars on their own as of version `1.0.0`**

Any method **not** starting with a `get` can be called like a normal method, e.g.

```cs
Debug.Log(profile.avatarUrl());
// returns avatar image url for a public key
```

## Thank You!

If you are using Desonity to buidl something consider supporting me on Deso [@weeblet](https://diamondapp.com/u/weeblet) and feel free to show me your crative implementaitons of Desonity ;)
