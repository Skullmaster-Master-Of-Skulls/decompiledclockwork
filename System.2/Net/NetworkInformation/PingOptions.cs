using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002EC RID: 748
	public class PingOptions
	{
		// Token: 0x06001A58 RID: 6744 RVA: 0x0007FE42 File Offset: 0x0007E042
		internal PingOptions(IPOptions options)
		{
			this.ttl = (int)options.ttl;
			this.dontFragment = ((options.flags & 2) > 0);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0007FE76 File Offset: 0x0007E076
		public PingOptions(int ttl, bool dontFragment)
		{
			if (ttl <= 0)
			{
				throw new ArgumentOutOfRangeException("ttl");
			}
			this.ttl = ttl;
			this.dontFragment = dontFragment;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0007FEA6 File Offset: 0x0007E0A6
		public PingOptions()
		{
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0007FEB9 File Offset: 0x0007E0B9
		// (set) Token: 0x06001A5C RID: 6748 RVA: 0x0007FEC1 File Offset: 0x0007E0C1
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ttl = value;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0007FED9 File Offset: 0x0007E0D9
		// (set) Token: 0x06001A5E RID: 6750 RVA: 0x0007FEE1 File Offset: 0x0007E0E1
		public bool DontFragment
		{
			get
			{
				return this.dontFragment;
			}
			set
			{
				this.dontFragment = value;
			}
		}

		// Token: 0x04001A8B RID: 6795
		private const int DontFragmentFlag = 2;

		// Token: 0x04001A8C RID: 6796
		private int ttl = 128;

		// Token: 0x04001A8D RID: 6797
		private bool dontFragment;
	}
}
