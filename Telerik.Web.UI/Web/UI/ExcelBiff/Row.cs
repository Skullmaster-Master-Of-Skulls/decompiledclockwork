using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ADB RID: 2779
	internal class Row
	{
		// Token: 0x060068B0 RID: 26800 RVA: 0x00188517 File Offset: 0x00186717
		public Row() : this(12.75)
		{
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x00188528 File Offset: 0x00186728
		public Row(double height)
		{
			this.height = height;
		}

		// Token: 0x17002254 RID: 8788
		// (get) Token: 0x060068B2 RID: 26802 RVA: 0x00188537 File Offset: 0x00186737
		// (set) Token: 0x060068B3 RID: 26803 RVA: 0x0018853F File Offset: 0x0018673F
		public double Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x17002255 RID: 8789
		// (get) Token: 0x060068B4 RID: 26804 RVA: 0x00188548 File Offset: 0x00186748
		// (set) Token: 0x060068B5 RID: 26805 RVA: 0x00188550 File Offset: 0x00186750
		public bool AutoSize
		{
			get
			{
				return this.autoSize;
			}
			set
			{
				this.autoSize = value;
			}
		}

		// Token: 0x04001BD2 RID: 7122
		public const double DefaultHeight = 12.75;

		// Token: 0x04001BD3 RID: 7123
		private double height;

		// Token: 0x04001BD4 RID: 7124
		private bool autoSize;
	}
}
