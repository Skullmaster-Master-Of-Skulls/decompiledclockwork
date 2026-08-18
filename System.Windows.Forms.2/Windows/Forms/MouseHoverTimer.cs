using System;

namespace System.Windows.Forms
{
	// Token: 0x020003B2 RID: 946
	internal class MouseHoverTimer : IDisposable
	{
		// Token: 0x06003ED3 RID: 16083 RVA: 0x00110844 File Offset: 0x0010EA44
		public MouseHoverTimer()
		{
			int num = SystemInformation.MouseHoverTime;
			if (num == 0)
			{
				num = 400;
			}
			this.mouseHoverTimer.Interval = num;
			this.mouseHoverTimer.Tick += this.OnTick;
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x00110894 File Offset: 0x0010EA94
		public void Start(ToolStripItem item)
		{
			if (item != this.currentItem)
			{
				this.Cancel(this.currentItem);
			}
			this.currentItem = item;
			if (this.currentItem != null)
			{
				this.mouseHoverTimer.Enabled = true;
			}
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x001108C6 File Offset: 0x0010EAC6
		public void Cancel()
		{
			this.mouseHoverTimer.Enabled = false;
			this.currentItem = null;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x001108DB File Offset: 0x0010EADB
		public void Cancel(ToolStripItem item)
		{
			if (item == this.currentItem)
			{
				this.Cancel();
			}
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x001108EC File Offset: 0x0010EAEC
		public void Dispose()
		{
			if (this.mouseHoverTimer != null)
			{
				this.Cancel();
				this.mouseHoverTimer.Dispose();
				this.mouseHoverTimer = null;
			}
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x0011090E File Offset: 0x0010EB0E
		private void OnTick(object sender, EventArgs e)
		{
			this.mouseHoverTimer.Enabled = false;
			if (this.currentItem != null && !this.currentItem.IsDisposed)
			{
				this.currentItem.FireEvent(EventArgs.Empty, ToolStripItemEventType.MouseHover);
			}
		}

		// Token: 0x04002495 RID: 9365
		private Timer mouseHoverTimer = new Timer();

		// Token: 0x04002496 RID: 9366
		private const int SPI_GETMOUSEHOVERTIME_WIN9X = 400;

		// Token: 0x04002497 RID: 9367
		private ToolStripItem currentItem;
	}
}
