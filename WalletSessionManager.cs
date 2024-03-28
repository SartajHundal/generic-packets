using System;
using System.IO;
using Newtonsoft.Json;

public class WalletSessionManager
{
    private const string SessionFileName = "wallet_session.json";

    public void SaveSession(WalletSession session)
    {
        string json = JsonConvert.SerializeObject(session);
        File.WriteAllText(SessionFileName, json);
    }

    public WalletSession LoadSession()
    {
        if (File.Exists(SessionFileName))
        {
            string json = File.ReadAllText(SessionFileName);
            return JsonConvert.DeserializeObject<WalletSession>(json);
        }
        else
        {
            return new WalletSession(); // Or handle null case as needed
        }
    }
}

public class WalletSession
{
    // Add properties to store relevant session information
    public string WalletAddress { get; set; }
    public string AccessToken { get; set; }
    // Add other properties as needed

    // Add constructors if necessary
    public WalletSession()
    {
        // Initialize default values if needed
        WalletAddress = "";
        AccessToken = "";
    }
}
