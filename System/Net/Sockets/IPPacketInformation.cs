using System;

namespace System.Net.Sockets
{
	// Token: 0x020005D5 RID: 1493
	public struct IPPacketInformation
	{
		// Token: 0x06002EEF RID: 12015 RVA: 0x000CEED5 File Offset: 0x000CDED5
		internal IPPacketInformation(IPAddress address, int networkInterface)
		{
			this.address = address;
			this.networkInterface = networkInterface;
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000CEEE5 File Offset: 0x000CDEE5
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000CEEED File Offset: 0x000CDEED
		public int Interface
		{
			get
			{
				return this.networkInterface;
			}
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000CEEF5 File Offset: 0x000CDEF5
		public static bool operator ==(IPPacketInformation packetInformation1, IPPacketInformation packetInformation2)
		{
			return packetInformation1.Equals(packetInformation2);
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000CEF0A File Offset: 0x000CDF0A
		public static bool operator !=(IPPacketInformation packetInformation1, IPPacketInformation packetInformation2)
		{
			return !packetInformation1.Equals(packetInformation2);
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000CEF24 File Offset: 0x000CDF24
		public override bool Equals(object comparand)
		{
			if (comparand == null)
			{
				return false;
			}
			if (!(comparand is IPPacketInformation))
			{
				return false;
			}
			IPPacketInformation ippacketInformation = (IPPacketInformation)comparand;
			return this.address.Equals(ippacketInformation.address) && this.networkInterface == ippacketInformation.networkInterface;
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000CEF6D File Offset: 0x000CDF6D
		public override int GetHashCode()
		{
			return this.address.GetHashCode() + this.networkInterface.GetHashCode();
		}

		// Token: 0x04002C6A RID: 11370
		private IPAddress address;

		// Token: 0x04002C6B RID: 11371
		private int networkInterface;
	}
}
