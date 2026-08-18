using System;

namespace AutoComboBox
{
	// Token: 0x020000D9 RID: 217
	public class ControlPadding
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0004241C File Offset: 0x0004141C
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00042434 File Offset: 0x00041434
		public int Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00042440 File Offset: 0x00041440
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00042458 File Offset: 0x00041458
		public int Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00042464 File Offset: 0x00041464
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0004247C File Offset: 0x0004147C
		public int Top
		{
			get
			{
				return this.top;
			}
			set
			{
				this.top = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00042488 File Offset: 0x00041488
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x000424A0 File Offset: 0x000414A0
		public int Bottom
		{
			get
			{
				return this.bottom;
			}
			set
			{
				this.bottom = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x000424AC File Offset: 0x000414AC
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x000424C4 File Offset: 0x000414C4
		public int Middle
		{
			get
			{
				return this.middle;
			}
			set
			{
				this.middle = value;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000424CE File Offset: 0x000414CE
		public ControlPadding(int left, int right, int top, int bottom, int middle)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
			this.middle = middle;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00042500 File Offset: 0x00041500
		public int LeftAndRight
		{
			get
			{
				return this.left + this.right;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00042520 File Offset: 0x00041520
		public int LeftAndRightAndMiddle
		{
			get
			{
				return this.left + this.right + this.middle;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x00042548 File Offset: 0x00041548
		public int TopAndBottom
		{
			get
			{
				return this.top + this.bottom;
			}
		}

		// Token: 0x04000639 RID: 1593
		private int left;

		// Token: 0x0400063A RID: 1594
		private int right;

		// Token: 0x0400063B RID: 1595
		private int top;

		// Token: 0x0400063C RID: 1596
		private int bottom;

		// Token: 0x0400063D RID: 1597
		private int middle;
	}
}
