using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A6F RID: 2671
	internal class Borders
	{
		// Token: 0x060066FD RID: 26365 RVA: 0x0018199D File Offset: 0x0017FB9D
		public Borders(Range range)
		{
			this.range = range;
		}

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x060066FE RID: 26366 RVA: 0x001819AC File Offset: 0x0017FBAC
		public Border Left
		{
			get
			{
				if (this.left == null)
				{
					this.left = new Border(this.range, BorderKind.Left);
				}
				return this.left;
			}
		}

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x060066FF RID: 26367 RVA: 0x001819CE File Offset: 0x0017FBCE
		public Border Right
		{
			get
			{
				if (this.right == null)
				{
					this.right = new Border(this.range, BorderKind.Right);
				}
				return this.right;
			}
		}

		// Token: 0x170021E4 RID: 8676
		// (get) Token: 0x06006700 RID: 26368 RVA: 0x001819F0 File Offset: 0x0017FBF0
		public Border Top
		{
			get
			{
				if (this.top == null)
				{
					this.top = new Border(this.range, BorderKind.Top);
				}
				return this.top;
			}
		}

		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x06006701 RID: 26369 RVA: 0x00181A12 File Offset: 0x0017FC12
		public Border Bottom
		{
			get
			{
				if (this.bottom == null)
				{
					this.bottom = new Border(this.range, BorderKind.Bottom);
				}
				return this.bottom;
			}
		}

		// Token: 0x040019BF RID: 6591
		private readonly Range range;

		// Token: 0x040019C0 RID: 6592
		private Border left;

		// Token: 0x040019C1 RID: 6593
		private Border right;

		// Token: 0x040019C2 RID: 6594
		private Border top;

		// Token: 0x040019C3 RID: 6595
		private Border bottom;
	}
}
