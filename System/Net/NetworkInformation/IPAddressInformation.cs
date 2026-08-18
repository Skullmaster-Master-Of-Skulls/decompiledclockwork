using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005DA RID: 1498
	public abstract class IPAddressInformation
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002F32 RID: 12082
		public abstract IPAddress Address { get; }

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002F33 RID: 12083
		public abstract bool IsDnsEligible { get; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002F34 RID: 12084
		public abstract bool IsTransient { get; }
	}
}
