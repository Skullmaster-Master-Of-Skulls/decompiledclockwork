using System;

namespace System.Net.Sockets
{
	// Token: 0x020003A0 RID: 928
	public struct IPPacketInformation
	{
		// Token: 0x060022A6 RID: 8870 RVA: 0x000A521A File Offset: 0x000A341A
		internal IPPacketInformation(IPAddress address, int networkInterface)
		{
			this.address = address;
			this.networkInterface = networkInterface;
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x000A522A File Offset: 0x000A342A
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000A5232 File Offset: 0x000A3432
		public int Interface
		{
			get
			{
				return this.networkInterface;
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000A523A File Offset: 0x000A343A
		public static bool operator ==(IPPacketInformation packetInformation1, IPPacketInformation packetInformation2)
		{
			return packetInformation1.Equals(packetInformation2);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000A524F File Offset: 0x000A344F
		public static bool operator !=(IPPacketInformation packetInformation1, IPPacketInformation packetInformation2)
		{
			return !packetInformation1.Equals(packetInformation2);
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000A5268 File Offset: 0x000A3468
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

		// Token: 0x060022AC RID: 8876 RVA: 0x000A52AF File Offset: 0x000A34AF
		public override int GetHashCode()
		{
			return this.address.GetHashCode() + this.networkInterface.GetHashCode();
		}

		// Token: 0x04001F9C RID: 8092
		private IPAddress address;

		// Token: 0x04001F9D RID: 8093
		private int networkInterface;
	}
}
