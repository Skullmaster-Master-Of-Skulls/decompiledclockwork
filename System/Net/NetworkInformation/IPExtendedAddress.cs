using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005F3 RID: 1523
	internal struct IPExtendedAddress
	{
		// Token: 0x06002FD6 RID: 12246 RVA: 0x000CF361 File Offset: 0x000CE361
		internal IPExtendedAddress(IPAddress address, IPAddress mask)
		{
			this.address = address;
			this.mask = mask;
		}

		// Token: 0x04002CF2 RID: 11506
		internal IPAddress mask;

		// Token: 0x04002CF3 RID: 11507
		internal IPAddress address;
	}
}
