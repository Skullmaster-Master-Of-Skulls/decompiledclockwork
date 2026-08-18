using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AB9 RID: 2745
	internal class Margins
	{
		// Token: 0x17002231 RID: 8753
		// (get) Token: 0x06006820 RID: 26656 RVA: 0x00185BF4 File Offset: 0x00183DF4
		// (set) Token: 0x06006821 RID: 26657 RVA: 0x00185BFC File Offset: 0x00183DFC
		public double Left
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

		// Token: 0x17002232 RID: 8754
		// (get) Token: 0x06006822 RID: 26658 RVA: 0x00185C05 File Offset: 0x00183E05
		// (set) Token: 0x06006823 RID: 26659 RVA: 0x00185C0D File Offset: 0x00183E0D
		public double Right
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

		// Token: 0x17002233 RID: 8755
		// (get) Token: 0x06006824 RID: 26660 RVA: 0x00185C16 File Offset: 0x00183E16
		// (set) Token: 0x06006825 RID: 26661 RVA: 0x00185C1E File Offset: 0x00183E1E
		public double Top
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

		// Token: 0x17002234 RID: 8756
		// (get) Token: 0x06006826 RID: 26662 RVA: 0x00185C27 File Offset: 0x00183E27
		// (set) Token: 0x06006827 RID: 26663 RVA: 0x00185C2F File Offset: 0x00183E2F
		public double Bottom
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

		// Token: 0x04001B44 RID: 6980
		private double left = 0.7;

		// Token: 0x04001B45 RID: 6981
		private double right = 0.7;

		// Token: 0x04001B46 RID: 6982
		private double top = 0.75;

		// Token: 0x04001B47 RID: 6983
		private double bottom = 0.75;
	}
}
