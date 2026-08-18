using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000627 RID: 1575
	public class PingOptions
	{
		// Token: 0x06003076 RID: 12406 RVA: 0x000D18AD File Offset: 0x000D08AD
		internal PingOptions(IPOptions options)
		{
			this.ttl = (int)options.ttl;
			this.dontFragment = ((options.flags & 2) > 0);
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000D18E3 File Offset: 0x000D08E3
		public PingOptions(int ttl, bool dontFragment)
		{
			if (ttl <= 0)
			{
				throw new ArgumentOutOfRangeException("ttl");
			}
			this.ttl = ttl;
			this.dontFragment = dontFragment;
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000D1913 File Offset: 0x000D0913
		public PingOptions()
		{
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x000D1926 File Offset: 0x000D0926
		// (set) Token: 0x0600307A RID: 12410 RVA: 0x000D192E File Offset: 0x000D092E
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

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x000D1946 File Offset: 0x000D0946
		// (set) Token: 0x0600307C RID: 12412 RVA: 0x000D194E File Offset: 0x000D094E
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

		// Token: 0x04002E28 RID: 11816
		private const int DontFragmentFlag = 2;

		// Token: 0x04002E29 RID: 11817
		private int ttl = 128;

		// Token: 0x04002E2A RID: 11818
		private bool dontFragment;
	}
}
