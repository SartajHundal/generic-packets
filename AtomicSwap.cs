using System;
using System.Security.Cryptography;
using System.Text;

public class AtomicSwap
{
    public string SourceChainId { get; set; }
    public string TargetChainId { get; set; }
    public string SourceAssetId { get; set; }
    public string TargetAssetId { get; set; }
    public decimal SourceAmount { get; set; }
    public decimal TargetAmount { get; set; }
    public string SourceAddress { get; set; }
    public string TargetAddress { get; set; }
    public object CryptoPacket { get; set; } // Assuming CryptoPacket is of type object for flexibility

    // Added properties
    public string Secret { get; set; }
    public string SecretHash { get; set; }

    public void InitiateSwap()
    {
        Secret = GenerateSecret();
        SecretHash = GenerateHash(Secret);

        LockSourceAssets(SourceChainId, SourceAssetId, SourceAmount, SecretHash);
        ShareSecretHashWithTargetChain(TargetChainId, SecretHash);
    }

    public void ClaimSwap()
    {
        UnlockTargetAssets(TargetChainId, TargetAssetId, TargetAmount, Secret);
        ClaimTargetAssets(TargetChainId, TargetAssetId, TargetAmount, Secret);
    }

    // Private methods implementations

    private string GenerateSecret()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var result = new char[10];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }
        return new string(result);
    }

    private string GenerateHash(string secret)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(secret);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }

    // Implementations of other methods remain unchanged...
    private void LockSourceAssets(string chainId, string assetId, decimal amount, string secretHash)
    {
        Console.WriteLine($"Locked {amount} units of {assetId} on chain {chainId} with secret hash {secretHash}");
    }

    private void ShareSecretHashWithTargetChain(string chainId, string secretHash)
    {
        Console.WriteLine($"Shared secret hash {secretHash} with chain {chainId}");
    }

    private void UnlockTargetAssets(string chainId, string assetId, decimal amount, string secret)
    {
        Console.WriteLine($"Unlocked {amount} units of {assetId} on chain {chainId} with secret {secret}");
    }

    private void ClaimTargetAssets(string chainId, string assetId, decimal amount, string secret)
    {
        Console.WriteLine($"Claimed {amount} units of {assetId} on chain {chainId} with secret {secret}");
    }
}
