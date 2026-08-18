using System;

namespace AutoComboBox
{
	// Token: 0x020000BF RID: 191
	public class EdgePadding
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0003A894 File Offset: 0x00039894
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0003A8AC File Offset: 0x000398AC
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

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0003A8B8 File Offset: 0x000398B8
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0003A8D0 File Offset: 0x000398D0
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

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0003A8DC File Offset: 0x000398DC
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0003A8F4 File Offset: 0x000398F4
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

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0003A900 File Offset: 0x00039900
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0003A918 File Offset: 0x00039918
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

		// Token: 0x06000736 RID: 1846 RVA: 0x0003A922 File Offset: 0x00039922
		public EdgePadding(int left, int right, int top, int bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0003A94C File Offset: 0x0003994C
		public int TotalWidth
		{
			get
			{
				return this.left + this.right;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0003A96C File Offset: 0x0003996C
		public int TotalHeight
		{
			get
			{
				return this.top + this.bottom;
			}
		}

		// Token: 0x04000597 RID: 1431
		private int top;

		// Token: 0x04000598 RID: 1432
		private int left;

		// Token: 0x04000599 RID: 1433
		private int right;

		// Token: 0x0400059A RID: 1434
		private int bottom;
	}
}
