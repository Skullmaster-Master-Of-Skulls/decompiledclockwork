using System;
using System.Net.NetworkInformation;

namespace OracleInternal.Network
{
	// Token: 0x02000156 RID: 342
	internal class NetEnvironment
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x00091EA4 File Offset: 0x000900A4
		static NetEnvironment()
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				if (networkInterface.Supports(NetworkInterfaceComponent.IPv4))
				{
					NetEnvironment.gotIPv4 = true;
				}
				if (networkInterface.Supports(NetworkInterfaceComponent.IPv6))
				{
					NetEnvironment.gotIPv6 = true;
				}
			}
		}

		// Token: 0x04000F1B RID: 3867
		public static readonly bool gotIPv4;

		// Token: 0x04000F1C RID: 3868
		public static readonly bool gotIPv6;
	}
}
