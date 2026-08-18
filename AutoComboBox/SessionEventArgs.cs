using System;

namespace AutoComboBox
{
	// Token: 0x020000D5 RID: 213
	public class SessionEventArgs
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x0004032A File Offset: 0x0003F32A
		public SessionEventArgs(DateTime oldDtpNowAdjusted, DateTime newDtpNowAdjusted)
		{
			this.oldDtpNowAdjusted = oldDtpNowAdjusted;
			this.newDtpNowAdjusted = newDtpNowAdjusted;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00040344 File Offset: 0x0003F344
		public DateTime OldDtpNowAdjusted
		{
			get
			{
				return this.oldDtpNowAdjusted;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0004035C File Offset: 0x0003F35C
		public DateTime NewDtpNowAdjusted
		{
			get
			{
				return this.newDtpNowAdjusted;
			}
		}

		// Token: 0x04000619 RID: 1561
		private DateTime oldDtpNowAdjusted;

		// Token: 0x0400061A RID: 1562
		private DateTime newDtpNowAdjusted;
	}
}
