using System;
using System.IO;
using Newtonsoft.Json;

public class WalletSessionManager
{
    private const string SessionFileName = "wallet_session.json";

    public void SaveSession(WalletSession session)
    {
        try
        {
            string json = JsonConvert.SerializeObject(session);
            File.WriteAllText(SessionFileName, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving session: {ex.Message}");
        }
    }

    public WalletSession LoadSession()
    {
        try
        {
            if (File.Exists(SessionFileName))
            {
                string json = File.ReadAllText(SessionFileName);
                return JsonConvert.DeserializeObject<WalletSession>(json);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading session: {ex.Message}");
        }

        return new WalletSession(); // Or handle null case as needed
    }
}

public class WalletSession
{
    public string WalletAddress { get; set; }
    public string AccessToken { get; set; }

    public WalletSession()
    {
        WalletAddress = "";
        AccessToken = "";
    }
}
