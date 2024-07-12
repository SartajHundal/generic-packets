using System;
using System.Collections.Generic;

public class Security
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public uint TotalSupply { get; private set; }
    public Dictionary<string, uint> BalanceOf { get; private set; }

    public event EventHandler<TransferEventArgs> Transfer;

    public Security(string name, string symbol, uint initialSupply)
    {
        Name = name;
        Symbol = symbol;
        TotalSupply = initialSupply;
        BalanceOf = new Dictionary<string, uint>();
        BalanceOf[GetOwner()] = initialSupply;
    }

    public void Transfer(string from, string to, uint value)
    {
        if (!BalanceOf.ContainsKey(from) || BalanceOf[from] < value)
        {
            throw new InvalidOperationException("Insufficient balance");
        }

        BalanceOf[from] -= value;
        if (!BalanceOf.ContainsKey(to))
        {
            BalanceOf[to] = 0;
        }
        BalanceOf[to] += value;

        Transfer?.Invoke(this, new TransferEventArgs(from, to, value));
    }

    public void Rename(string newName)
    {
        Name = newName;
    }

    public void WhiteLabel(string newSymbol)
    {
        Symbol = newSymbol;
    }

    private string GetOwner()
    {
        // In a real-world scenario, you would implement logic to determine the owner.
        return "OwnerAddress";
    }

    // New method to simulate price fluctuations based on demand and supply
    public void SimulatePriceFluctuation()
    {
        // Example: Increase price by 5% if demand is high, decrease by 5% if supply is high
        // This is a simplified example. Actual implementation would depend on specific criteria.
        double newPrice = TotalSupply > 10000 ? TotalSupply * 0.95 : TotalSupply * 1.05;
        Description = $"Current Price: {newPrice}";
    }

    // New method to adjust total supply based on economic policies
    public void AdjustTotalSupplyBasedOnEconomicPolicies()
    {
        // Example: Reduce total supply by 10% to simulate an economic policy aimed at controlling inflation
        TotalSupply = (uint)(TotalSupply * 0.9);
    }

    // New method to update balances based on trading volume
    public void UpdateBalancesBasedOnTradingVolume()
    {
        // Example: Distribute 1% of total supply equally among all current holders to simulate trading volume effects
        uint distributionAmount = (uint)(TotalSupply * 0.01);
        foreach (var holder in BalanceOf.Keys.ToList())
        {
            BalanceOf[holder] += distributionAmount / BalanceOf.Count;
        }
    }
}
