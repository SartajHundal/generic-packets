using System;

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
}

public class TransferEventArgs : EventArgs
{
    public string From { get; }
    public string To { get; }
    public uint Value { get; }

    public TransferEventArgs(string from, string to, uint value)
    {
        From = from;
        To = to;
        Value = value;
    }
}
