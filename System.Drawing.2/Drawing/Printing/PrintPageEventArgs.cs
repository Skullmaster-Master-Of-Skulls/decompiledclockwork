using System;

namespace System.Drawing.Printing
{
	// Token: 0x0200006C RID: 108
	public class PrintPageEventArgs : EventArgs
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x00020B23 File Offset: 0x0001ED23
		public PrintPageEventArgs(Graphics graphics, Rectangle marginBounds, Rectangle pageBounds, PageSettings pageSettings)
		{
			this.graphics = graphics;
			this.marginBounds = marginBounds;
			this.pageBounds = pageBounds;
			this.pageSettings = pageSettings;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00020B4F File Offset: 0x0001ED4F
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00020B57 File Offset: 0x0001ED57
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00020B60 File Offset: 0x0001ED60
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00020B68 File Offset: 0x0001ED68
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x00020B70 File Offset: 0x0001ED70
		public bool HasMorePages
		{
			get
			{
				return this.hasMorePages;
			}
			set
			{
				this.hasMorePages = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00020B79 File Offset: 0x0001ED79
		public Rectangle MarginBounds
		{
			get
			{
				return this.marginBounds;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00020B81 File Offset: 0x0001ED81
		public Rectangle PageBounds
		{
			get
			{
				return this.pageBounds;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00020B89 File Offset: 0x0001ED89
		public PageSettings PageSettings
		{
			get
			{
				return this.pageSettings;
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00020B91 File Offset: 0x0001ED91
		internal void Dispose()
		{
			this.graphics.Dispose();
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00020B9E File Offset: 0x0001ED9E
		internal void SetGraphics(Graphics value)
		{
			this.graphics = value;
		}

		// Token: 0x040006F3 RID: 1779
		private bool hasMorePages;

		// Token: 0x040006F4 RID: 1780
		private bool cancel;

		// Token: 0x040006F5 RID: 1781
		private Graphics graphics;

		// Token: 0x040006F6 RID: 1782
		private readonly Rectangle marginBounds;

		// Token: 0x040006F7 RID: 1783
		private readonly Rectangle pageBounds;

		// Token: 0x040006F8 RID: 1784
		private readonly PageSettings pageSettings;

		// Token: 0x040006F9 RID: 1785
		internal bool CopySettingsToDevMode = true;
	}
}
