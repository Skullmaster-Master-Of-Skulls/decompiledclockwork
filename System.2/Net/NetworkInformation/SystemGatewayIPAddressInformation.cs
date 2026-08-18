using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002AF RID: 687
	internal class SystemGatewayIPAddressInformation : GatewayIPAddressInformation
	{
		// Token: 0x060019A2 RID: 6562 RVA: 0x0007E24D File Offset: 0x0007C44D
		private SystemGatewayIPAddressInformation(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x0007E25C File Offset: 0x0007C45C
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0007E264 File Offset: 0x0007C464
		internal static GatewayIPAddressInformationCollection ToGatewayIpAddressInformationCollection(IPAddressCollection addresses)
		{
			GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
			foreach (IPAddress ipaddress in addresses)
			{
				gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(ipaddress));
			}
			return gatewayIPAddressInformationCollection;
		}

		// Token: 0x04001913 RID: 6419
		private IPAddress address;
	}
}
