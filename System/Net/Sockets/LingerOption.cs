using System;

namespace System.Net.Sockets
{
	// Token: 0x020005AB RID: 1451
	public class LingerOption
	{
		// Token: 0x06002CD0 RID: 11472 RVA: 0x000C1D63 File Offset: 0x000C0D63
		public LingerOption(bool enable, int seconds)
		{
			this.Enabled = enable;
			this.LingerTime = seconds;
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x000C1D79 File Offset: 0x000C0D79
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x000C1D81 File Offset: 0x000C0D81
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x000C1D8A File Offset: 0x000C0D8A
		// (set) Token: 0x06002CD4 RID: 11476 RVA: 0x000C1D92 File Offset: 0x000C0D92
		public int LingerTime
		{
			get
			{
				return this.lingerTime;
			}
			set
			{
				this.lingerTime = value;
			}
		}

		// Token: 0x04002AD5 RID: 10965
		private bool enabled;

		// Token: 0x04002AD6 RID: 10966
		private int lingerTime;
	}
}
