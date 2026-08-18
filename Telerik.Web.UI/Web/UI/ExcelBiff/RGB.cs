using System;
using System.Drawing;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ADA RID: 2778
	internal class RGB
	{
		// Token: 0x060068AD RID: 26797 RVA: 0x0018849D File Offset: 0x0018669D
		public RGB(byte r, byte g, byte b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x001884BA File Offset: 0x001866BA
		public int Distance(Color color)
		{
			return Math.Abs((int)(color.R - this.r)) + Math.Abs((int)(color.G - this.g)) + Math.Abs((int)(color.B - this.b));
		}

		// Token: 0x17002253 RID: 8787
		// (get) Token: 0x060068AF RID: 26799 RVA: 0x001884F7 File Offset: 0x001866F7
		public bool IsChromatic
		{
			get
			{
				return this.r == this.g && this.g == this.b;
			}
		}

		// Token: 0x04001BCF RID: 7119
		private byte r;

		// Token: 0x04001BD0 RID: 7120
		private byte g;

		// Token: 0x04001BD1 RID: 7121
		private byte b;
	}
}
