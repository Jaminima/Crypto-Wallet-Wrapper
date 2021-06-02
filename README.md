# Crypto Wallet Wrapper

This project aims to provide simplified interaction with [Bitcoin Core](https://github.com/bitcoin/bitcoin) like wallets. Enabling the writing of C# apps that can exploit the wallet libray or web enabled apps via the provided simple API.

## Important Notes

## No Security

There is no real secuirty built into the current version of the API. A session key is provided on Login/Register that identifies a users current session. User does hold a placeholder password field but no validation is performed against this.

Said session key is stored in plain text in the API's memory. You **WILL** want to improve security if used in any production enviroment.

## Flat File Storage

Confirmed txID's (Wrapper Library) and User (API) data is currently stored in a flat file format. In any production environment it is highly recommended that you use a proper dB service. Chosing not to do so increases the risk of data corruption, especially at higher loads when write collisions may cause a crash.

# Set-Up

In this section we will go through setting up a Core wallet example along with changing the config to work with said wallet.

Download your Crypto's core files.

- [Bitcoin Core](https://github.com/bitcoin/bitcoin/releases)

- [Dogecoin Core](https://github.com/dogecoin/dogecoin/releases)

- [Garlicoin Core](https://github.com/GarlicoinOrg/Garlicoin/releases) - [Tutorial](https://guide.garli.co.in/wallet-win.html)

- [Litecoin Core](https://github.com/litecoin-project/litecoin/releases)

Once downloaded and followed the typical steps to download and setup your core. You must ensure RPC is enabled in your cores config, typically located in a file like **%appdata%/Garlicoin/garlicoin.conf**. Ensure the **Server** setting is set to 1, also set the **rpcuser**, **rpcpassword** & **rpcport** to sensible values of your choice.

Find the config.json located within your project (requires inital run to generate). Change your **corepath**, **coindName** to match those in your core folder. Also change the **rpcAddress**, **username** & **password** to match what you set in the core config.

When using Garlicoin Core my config ends up looking like:

| ![](https://github.com/Jaminima/Crypto-Wallet-Wrapper/blob/main/Imgs/FolderContents.png) | ![](https://github.com/Jaminima/Crypto-Wallet-Wrapper/blob/main/Imgs/Config.png) |
| ---------------------------------------------------------------------------------------- |:-------------------------------------------------------------------------------- |

# Working with the Libray

The Wallet-Wrapper can be imported like any other library. On your apps start the function must be called.

```csharp
using Wallet_Wrapper;

private static void Main(string[] args){
    Cli_Manager.Start(true); //By setting to true, the app wont throw an error if the core is already running
}
```

Key functions you may want to run are located in the <u>Cli-Payments</u> and <u>Cli-Gets</u> classes.

## Payments

```csharp
await Cli_Payments.PayOut("ADDRESS", 0.5f); //Pay to a given address
await Cli_Payments.ConfirmPayment("receiveAddress", "txID"); //Confirm receipt of a payment
```

## Gets

```csharp
Objects.Wallet wallet = await GetWalletInfo(); //Wallets full details
Objects.Address address = await VerifyAddress("ADDRESS");
Objects.Transaction transaction = await GetTransaction("txID");

string network = await GetNetworkInfo();
string blockchain = await GetBlockChainInfo();
string newAddress = await GetNewWalletAddress();

bool networkRunning = await IsNetworkRunning();
```

# Working with the API

An [Insomnia](https://insomnia.rest/download) api doc file is provided in the repos root directory. The api is built using ASP.net so expanding to use a scaffolded database or adding aditional functionality should be easily acheived.

The **Important Notes** section has imprtant details about the API.

# Known Issues

- Sometimes API requests stall out indefinitely. Typically when using the GetTransaction function.

# Contributing

I welcome all contributions to the project. The addition of endpoints and wrapper functions should be listed and added into docs where possible.
