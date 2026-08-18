using System;

namespace System.Net.Sockets
{
	// Token: 0x0200036D RID: 877
	public class LingerOption
	{
		// Token: 0x06001FD9 RID: 8153 RVA: 0x00095234 File Offset: 0x00093434
		public LingerOption(bool enable, int seconds)
		{
			this.Enabled = enable;
			this.LingerTime = seconds;
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x0009524A File Offset: 0x0009344A
		// (set) Token: 0x06001FDB RID: 8155 RVA: 0x00095252 File Offset: 0x00093452
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

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x0009525B File Offset: 0x0009345B
		// (set) Token: 0x06001FDD RID: 8157 RVA: 0x00095263 File Offset: 0x00093463
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

		// Token: 0x04001DE6 RID: 7654
		private bool enabled;

		// Token: 0x04001DE7 RID: 7655
		private int lingerTime;
	}
}
