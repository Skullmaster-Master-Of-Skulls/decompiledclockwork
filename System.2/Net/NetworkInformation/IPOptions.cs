using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D4 RID: 724
	internal struct IPOptions
	{
		// Token: 0x060019C3 RID: 6595 RVA: 0x0007E484 File Offset: 0x0007C684
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

		// Token: 0x04001A39 RID: 6713
		internal byte ttl;

		// Token: 0x04001A3A RID: 6714
		internal byte tos;

		// Token: 0x04001A3B RID: 6715
		internal byte flags;

		// Token: 0x04001A3C RID: 6716
		internal byte optionsSize;

		// Token: 0x04001A3D RID: 6717
		internal IntPtr optionsData;
	}
}
