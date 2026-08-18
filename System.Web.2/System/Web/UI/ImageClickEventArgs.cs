using System;

namespace System.Web.UI
{
	// Token: 0x020002A9 RID: 681
	public sealed class ImageClickEventArgs : EventArgs
	{
		// Token: 0x06001FA2 RID: 8098 RVA: 0x000656E8 File Offset: 0x000638E8
		public ImageClickEventArgs(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000656FE File Offset: 0x000638FE
		public ImageClickEventArgs(int x, int y, double xRaw, double yRaw)
		{
			this.X = x;
			this.Y = y;
			this.XRaw = xRaw;
			this.YRaw = yRaw;
		}

		// Token: 0x04001AB5 RID: 6837
		public int X;

		// Token: 0x04001AB6 RID: 6838
		public int Y;

		// Token: 0x04001AB7 RID: 6839
		public double XRaw;

		// Token: 0x04001AB8 RID: 6840
		public double YRaw;
	}
}
