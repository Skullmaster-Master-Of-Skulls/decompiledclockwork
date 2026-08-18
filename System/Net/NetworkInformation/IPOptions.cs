using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200060A RID: 1546
	internal struct IPOptions
	{
		// Token: 0x06002FDA RID: 12250 RVA: 0x000CF554 File Offset: 0x000CE554
		internal IPOptions(PingOptions options)
		{
			this.ttl = 128;
			this.tos = 0;
			this.flags = 0;
			this.optionsSize = 0;
			this.optionsData = IntPtr.Zero;
			if (options != null)
			{
				this.ttl = (byte)options.Ttl;
				if (options.DontFragment)
				{
					this.flags = 2;
				}
			}
		}

		// Token: 0x04002DB6 RID: 11702
		internal byte ttl;

		// Token: 0x04002DB7 RID: 11703
		internal byte tos;

		// Token: 0x04002DB8 RID: 11704
		internal byte flags;

		// Token: 0x04002DB9 RID: 11705
		internal byte optionsSize;

		// Token: 0x04002DBA RID: 11706
		internal IntPtr optionsData;
	}
}
