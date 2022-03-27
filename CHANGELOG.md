# CHANGELOG

All notable changes for Desonity package will be documented in this file

## 1.0.2 - 2022-03-27

### Changed

- Added Serializable Classes for each deso endpoint under `Endpoints.cs`, this will be used to create the Json string that will be posted to the backend. This will make the code more readable and easier to modify the post json.
- Replaced UnityEngines JsonUtility with Newtonsoft.Json to Serialize endpoint classes in `Endpoint.cs`

## 1.0.1 - 2022-03-25

### Added

- Deso Identity Login

### Fixed

- Fixed minor bug in `Profile.getNftsForUser` which did not have nullable bool as a parameter

## 1.0.0 - 2022-03-24

### Added

- get Profile
- get owned NFTs
- get Single NFT
- get avatar url
