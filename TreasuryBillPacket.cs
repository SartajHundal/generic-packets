public class TreasuryBillPacket : Packet
{
    public string Issuer { get; set; }
    public DateTime MaturityDate { get; set; }
    public decimal FaceValue { get; set; }
    // Add specific properties for treasury bill packets
}
