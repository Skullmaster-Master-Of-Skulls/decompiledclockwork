using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200061C RID: 1564
	public abstract class NetworkInterface
	{
		// Token: 0x0600302B RID: 12331 RVA: 0x000D00D7 File Offset: 0x000CF0D7
		public static NetworkInterface[] GetAllNetworkInterfaces()
		{
			new NetworkInformationPermission(NetworkInformationAccess.Read).Demand();
			return SystemNetworkInterface.GetNetworkInterfaces();
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000D00E9 File Offset: 0x000CF0E9
		public static bool GetIsNetworkAvailable()
		{
			return SystemNetworkInterface.InternalGetIsNetworkAvailable();
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x000D00F0 File Offset: 0x000CF0F0
		public static int LoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.InternalLoopbackInterfaceIndex;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600302E RID: 12334
		public abstract string Id { get; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600302F RID: 12335
		public abstract string Name { get; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003030 RID: 12336
		public abstract string Description { get; }

		// Token: 0x06003031 RID: 12337
		public abstract IPInterfaceProperties GetIPProperties();

		// Token: 0x06003032 RID: 12338
		public abstract IPv4InterfaceStatistics GetIPv4Statistics();

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06003033 RID: 12339
		public abstract OperationalStatus OperationalStatus { get; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06003034 RID: 12340
		public abstract long Speed { get; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003035 RID: 12341
		public abstract bool IsReceiveOnly { get; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003036 RID: 12342
		public abstract bool SupportsMulticast { get; }

		// Token: 0x06003037 RID: 12343
		public abstract PhysicalAddress GetPhysicalAddress();

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003038 RID: 12344
		public abstract NetworkInterfaceType NetworkInterfaceType { get; }

		// Token: 0x06003039 RID: 12345
		public abstract bool Supports(NetworkInterfaceComponent networkInterfaceComponent);
	}
}
