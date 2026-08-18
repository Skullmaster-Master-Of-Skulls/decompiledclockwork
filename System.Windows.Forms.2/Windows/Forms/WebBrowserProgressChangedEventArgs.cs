using System;

namespace System.Windows.Forms
{
	// Token: 0x0200043F RID: 1087
	public class WebBrowserProgressChangedEventArgs : EventArgs
	{
		// Token: 0x06004B8C RID: 19340 RVA: 0x0013A8C7 File Offset: 0x00138AC7
		public WebBrowserProgressChangedEventArgs(long currentProgress, long maximumProgress)
		{
			this.currentProgress = currentProgress;
			this.maximumProgress = maximumProgress;
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06004B8D RID: 19341 RVA: 0x0013A8DD File Offset: 0x00138ADD
		public long CurrentProgress
		{
			get
			{
				return this.currentProgress;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x0013A8E5 File Offset: 0x00138AE5
		public long MaximumProgress
		{
			get
			{
				return this.maximumProgress;
			}
		}

		// Token: 0x0400283B RID: 10299
		private long currentProgress;

		// Token: 0x0400283C RID: 10300
		private long maximumProgress;
	}
}
