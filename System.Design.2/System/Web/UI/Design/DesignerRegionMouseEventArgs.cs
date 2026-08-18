using System;
using System.Drawing;

namespace System.Web.UI.Design
{
	// Token: 0x02000036 RID: 54
	public sealed class DesignerRegionMouseEventArgs : EventArgs
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000D0F7 File Offset: 0x0000B2F7
		public DesignerRegionMouseEventArgs(DesignerRegion region, Point location)
		{
			this._location = location;
			this._region = region;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000D10D File Offset: 0x0000B30D
		public Point Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000D115 File Offset: 0x0000B315
		public DesignerRegion Region
		{
			get
			{
				return this._region;
			}
		}

		// Token: 0x0400012D RID: 301
		private Point _location;

		// Token: 0x0400012E RID: 302
		private DesignerRegion _region;
	}
}
