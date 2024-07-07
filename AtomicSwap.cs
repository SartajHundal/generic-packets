using System;
using System.Security.Cryptography;

public class AtomicSwap 
{
    // Properties remain unchanged

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
        random.NextChars(chars.Length, result);
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
