using System;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// Manages the saving and loading of wallet sessions to and from a JSON file.
/// </summary>
public class WalletSessionManager
{
    private const string SessionFileName = "wallet_session.json";

    /// <summary>
    /// Saves the provided wallet session to a JSON file.
    /// </summary>
    /// <param name="session">The wallet session to save.</param>
    public void SaveSession(WalletSession session)
    {
        try
        {
            string json = JsonConvert.SerializeObject(session);
            File.WriteAllText(SessionFileName, json);
            Console.WriteLine("Session saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving session: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads the wallet session from the JSON file.
    /// </summary>
    /// <returns>The loaded wallet session, or a new session if loading fails.</returns>
    public WalletSession LoadSession()
    {
        try
        {
            if (File.Exists(SessionFileName))
            {
                string json = File.ReadAllText(SessionFileName);
                return JsonConvert.DeserializeObject<WalletSession>(json);
            }
            else
            {
                Console.WriteLine("No session file found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading session: {ex.Message}");
        }

        return new WalletSession(); // Or handle null case as needed
    }

    /// <summary>
    /// Automatically reconnects to the wallet session if the connection is lost.
    /// </summary>
    /// <param name="session">The wallet session to reconnect.</param>
    public void ReconnectSession(WalletSession session)
    {
        // Placeholder for automatic reconnection logic
        Console.WriteLine("Attempting automatic reconnection...");
        // Your reconnection logic goes here
        Console.WriteLine("Automatic reconnection successful.");
    }
}

/// <summary>
/// Represents a wallet session containing the wallet address and access token.
/// </summary>
public class WalletSession
{
    /// <summary>
    /// Gets or sets the wallet address.
    /// </summary>
    public string WalletAddress { get; set; }

    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WalletSession"/> class.
    /// </summary>
    public WalletSession()
    {
        WalletAddress = "";
        AccessToken = "";
    }

    /// <summary>
    /// Returns a string representation of the wallet session.
    /// </summary>
    /// <returns>A string representing the wallet session.</returns>
    public override string ToString()
    {
        return $"Wallet Address: {WalletAddress}, Access Token: {AccessToken}";
    }
}
