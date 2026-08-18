using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A7A RID: 2682
	internal class Column
	{
		// Token: 0x06006753 RID: 26451 RVA: 0x0018250C File Offset: 0x0018070C
		public Column() : this(8.43)
		{
		}

		// Token: 0x06006754 RID: 26452 RVA: 0x0018251D File Offset: 0x0018071D
		public Column(double width)
		{
			this.width = width;
		}

		// Token: 0x17002206 RID: 8710
		// (get) Token: 0x06006755 RID: 26453 RVA: 0x0018252C File Offset: 0x0018072C
		// (set) Token: 0x06006756 RID: 26454 RVA: 0x00182534 File Offset: 0x00180734
		public double Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x04001A0F RID: 6671
		public const double DefaultWidth = 8.43;

		// Token: 0x04001A10 RID: 6672
		private double width;
	}
}
