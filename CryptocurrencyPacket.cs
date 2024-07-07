public class CryptocurrencyPacket : Packet
{
    public string Cryptocurrency { get; set; }
    public string Blockchain { get; set; }
    public string Hash { get; set; }
    public string Type { get; set; } // E.g., Bitcoin-like, Ethereum-like
    public string ConsensusMechanism { get; set; } // E.g., PoW, PoS, DPoS
    public int BlockTime { get; set; } // In seconds
    public bool HasMaxSupply { get; set; }
    public decimal? MaxSupply { get; set; } // Nullable for cryptocurrencies without a defined max supply
    public bool IsFungible { get; set; }
    public bool SupportsSmartContracts { get; set; }
    public bool OffersPrivacy { get; set; }
}
