using System;

// Interface for MetaMask functionality
public interface IMetaMask
{
    void SwitchNetwork(string network);
    string CheckNetworkStatus();
}

// Class representing MetaMask functionality
public class MetaMask : IMetaMask
{
    // Method to switch to a different Ethereum network
    public void SwitchNetwork(string network)
    {
        // Your code to switch to the specified Ethereum network in MetaMask
        Console.WriteLine($"Switching MetaMask network to {network}...");
        // Example: MetaMask.switchNetwork(network);
        Console.WriteLine("MetaMask network switched successfully.");
    }

    // Method to check the status of the Ethereum network
    public string CheckNetworkStatus()
    {
        // Your code to check the status of the Ethereum network in MetaMask
        // Example: string networkStatus = MetaMask.checkNetworkStatus();
        // For simplicity, let's assume it returns a string indicating the status
        string networkStatus = "Healthy"; // Placeholder for network status
        return networkStatus;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Instantiate MetaMask class through the interface
        IMetaMask metaMask = new MetaMask();

        // Example usage: Switch to a different Ethereum network
        string newNetwork = "Rinkeby";
        metaMask.SwitchNetwork(newNetwork);

        // Example usage: Check the status of the Ethereum network
        string status = metaMask.CheckNetworkStatus();
        Console.WriteLine($"Ethereum network status: {status}");
    }
}
