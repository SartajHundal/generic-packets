using Microsoft.EntityFrameworkCore;
using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class MyContractService
{
    private readonly Web3 _web3;
    private readonly MyDbContext _dbContext;

    public MyContractService(string rpcUrl)
    {
        _web3 = new Web3(rpcUrl);
        _dbContext = new MyDbContext();
    }

    public async Task MapProtocolWithCoinMetadata()
    {
        // Retrieve protocol data from smart contract
        var protocolValue = await GetValueFromSmartContract();

        // Retrieve coin metadata from API
        var coinMetadata = await GetCoinMetadata();

        // Map protocol data with coin metadata
        var protocol = new Protocol
        {
            Value = protocolValue,
            Coin = coinMetadata
        };

        // Save to database
        _dbContext.Protocols.Add(protocol);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<BigInteger> GetValueFromSmartContract()
    {
        // Assuming the smart contract has a function named getProtocolValue(uint256)
        var contractFunction = new Functions.GetProtocolValueFunction();
        var result = await _web3.Eth.CallAsync<Functions.GetProtocolValueFunction>(contractFunction, "smart_contract_address", fromBlock: null, toBlock: null);
        return result.Value;
    }

    private async Task<Coin> GetCoinMetadata()
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync("https://api.coingecko.com/api/v3/coins/{coin_id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var coinMetadataJson = JsonSerializer.Deserialize<CoinMetadata>(content);
            return new Coin
            {
                Name = coinMetadataJson.Name,
                Symbol = coinMetadataJson.Symbol,
                Description = coinMetadataJson.Description
            };
        }
    }
}
