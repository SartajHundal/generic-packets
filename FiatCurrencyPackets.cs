public class FiatCurrencyPacket : Packet
{
    public string Sender { get; set; }
    public string Recipient { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string TransactionId { get; set; }
    // Add specific properties for fiat currency packets
}
