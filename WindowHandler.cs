using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Transaction
{
    public string TransactionId { get; set; }
    // Add other transaction properties as needed
}

public partial class TransactionHandler : IDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private List<Transaction> pendingTransactions = new List<Transaction>();
    private DotNetObjectReference<TransactionHandler> _dotNetHelper;

    public TransactionHandler(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    protected override void OnInitialized()
    {
        _dotNetHelper = DotNetObjectReference.Create(this);
        // Hook up window closing event
        _jsRuntime.InvokeVoidAsync("window.addEventListener", "beforeunload", 
            "handleBeforeUnload", _dotNetHelper);
    }

    // Method to add a transaction to the pending list
    public void AddTransaction(Transaction transaction)
    {
        pendingTransactions.Add(transaction);
    }

    // Method to execute pending transactions before window closes
    [JSInvokable]
    public async Task ExecutePendingTransactions()
    {
        foreach (var transaction in pendingTransactions)
        {
            // Execute transaction logic here
            // For example, you can use Web3.js interop to interact with Ethereum blockchain
            // Execute transaction logic
            Console.WriteLine($"Executing transaction: {transaction.TransactionId}");
            // Simulate async operation
            await Task.Delay(100); // Simulating transaction execution time
        }
    }

    public void Dispose()
    {
        // Unhook window closing event
        _jsRuntime.InvokeVoidAsync("window.removeEventListener", "beforeunload", 
            "handleBeforeUnload", _dotNetHelper);
        _dotNetHelper?.Dispose();
    }

    [JSInvokable]
    public async Task HandleBeforeUnload()
    {
        await ExecutePendingTransactions();
    }
}
