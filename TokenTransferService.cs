// Yes, we can use RPC
// Yes, there is a way of wrapping
// any existent or upcoming protocols
// - it's an open research area with MainNet
// Create the project struct / redoc tomorrow
// ERC-20 stuff & key-value implementations
// are tending towards spaghetti

using Nethereum.Web3;
using Nethereum.Contracts;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;
using System.Threading.Tasks;

public class TokenTransferService
{
    private readonly Web3 _web3;

    public TokenTransferService(string rpcUrl)
    {
        _web3 = new Web3(rpcUrl);
    }

    public async Task<string> TransferTokensAsync(string senderPrivateKey, string receiverAddress, string contractAddress, BigInteger tokens)
    {
        var account = new Nethereum.Web3.Accounts.Account(senderPrivateKey);
        var transferFunction = new TransferFunction { FromAddress = account.Address, To = receiverAddress, AmountToSend = tokens };
        var transferHandler = _web3.Eth.GetContractTransactionHandler<TransferFunction>();
        var transactionHash = await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, transferFunction, account.Address);
        return transactionHash;
    }
}

[Function("transfer", "bool")]
public class TransferFunction : FunctionMessage
{
    [Parameter("address", "to", 1)]
    public string To { get; set; }

    [Parameter("uint256", "value", 2)]
    public BigInteger AmountToSend { get; set; }
}
