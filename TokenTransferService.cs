// Yes, we can use RPC
// Yes, there is a way of wrapping
// any existent or upcoming protocols
// - it's an open research area with MainNet
// find lowest Tx fees in general; ERC20 tends to be too high ...

using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Contracts;
using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Linq;

/// <summary>
/// Service for transferring tokens and finding transactions with lowest fees on the Ethereum blockchain.
/// </summary>
public class TokenTransferService
{
    private readonly Web3 _web3;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenTransferService"/> class with the specified RPC URL.
    /// </summary>
    /// <param name="rpcUrl">The RPC URL of the Ethereum node.</param>
    public TokenTransferService(string rpcUrl)
    {
        _web3 = new Web3(rpcUrl);
    }

    /// <summary>
    /// Transfers tokens from one address to another asynchronously.
    /// </summary>
    /// <param name="senderPrivateKey">The private key of the sender's Ethereum account.</param>
    /// <param name="receiverAddress">The address of the recipient.</param>
    /// <param name="contractAddress">The address of the token contract.</param>
    /// <param name="tokens">The amount of tokens to transfer.</param>
    /// <returns>The transaction hash of the token transfer.</returns>
    public async Task<string> TransferTokensAsync(string senderPrivateKey, string receiverAddress, string contractAddress, BigInteger tokens)
    {
        try
        {
            var account = new Account(senderPrivateKey);
            var transferFunction = new TransferFunction { To = receiverAddress, AmountToSend = tokens };
            var transferHandler = _web3.Eth.GetContractTransactionHandler<TransferFunction>();
            var transactionHash = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transferFunction, account.Address);
            return transactionHash;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error transferring tokens: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Finds the transaction with the lowest gas fees among the specified transaction hashes asynchronously.
    /// </summary>
    /// <param name="transactionHashes">An array of transaction hashes to search through.</param>
    /// <returns>The transaction hash of the transaction with the lowest gas fees.</returns>
    public async Task<string> FindTransactionWithLowestFeesAsync(string[] transactionHashes)
    {
        try
        {
            var lowestFeeTransactionHash = "";

            decimal lowestFee = decimal.MaxValue;

            foreach (var hash in transactionHashes)
            {
                var transaction = await _web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(hash);
                var gasPrice = Web3.Convert.FromWei(transaction.GasPrice.Value);

                if (gasPrice < lowestFee)
                {
                    lowestFee = gasPrice;
                    lowestFeeTransactionHash = hash;
                }
            }

            return lowestFeeTransactionHash;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error finding transaction with lowest fees: {ex.Message}");
            return null;
        }
    }
}

/// <summary>
/// Represents a function message for token transfers.
/// </summary>
[Function("transfer", "bool")]
public class TransferFunction : FunctionMessage
{
    /// <summary>
    /// Gets or sets the address of the recipient.
    /// </summary>
    [Parameter("address", "to", 1)]
    public string To { get; set; }

    /// <summary>
    /// Gets or sets the amount of tokens to transfer.
    /// </summary>
    [Parameter("uint256", "value", 2)]
    public BigInteger AmountToSend { get; set; }
}
