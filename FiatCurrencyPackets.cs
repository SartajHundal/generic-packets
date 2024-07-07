public class FiatCurrencyPacket : Packet
{
    public string Sender { get; set; }
    public string Recipient { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string TransactionId { get; set; }
    public DateTime Date { get; set; } // Transaction date and time
    public string CountryCode { get; set; } // ISO country code for international transactions
    public string ExchangeRate { get; set; } // Optional: Exchange rate used for conversion
    public string Fee { get; set; } // Optional: Transaction fee charged
    public string Status { get; set; } // Transaction status (e.g., pending, completed, failed)
    public string Notes { get; set; } // Optional: Any additional notes or comments about the transaction
}
