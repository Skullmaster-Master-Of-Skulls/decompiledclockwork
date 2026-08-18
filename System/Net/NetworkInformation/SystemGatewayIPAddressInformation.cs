using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005E9 RID: 1513
	internal class SystemGatewayIPAddressInformation : GatewayIPAddressInformation
	{
		// Token: 0x06002FBB RID: 12219 RVA: 0x000CF270 File Offset: 0x000CE270
		internal SystemGatewayIPAddressInformation(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000CF27F File Offset: 0x000CE27F
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x04002CC6 RID: 11462
		private IPAddress address;
	}
}
