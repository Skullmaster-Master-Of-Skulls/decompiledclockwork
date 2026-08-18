using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018C5 RID: 6341
	public class RadFilterFildDesciptorsEventArgs : EventArgs
	{
		// Token: 0x0600F579 RID: 62841 RVA: 0x0037BFAA File Offset: 0x0037A1AA
		public RadFilterFildDesciptorsEventArgs()
		{
		}

		// Token: 0x0600F57A RID: 62842 RVA: 0x0037BFB2 File Offset: 0x0037A1B2
		public RadFilterFildDesciptorsEventArgs(RadFilterFilterableView filterableView)
		{
			this._filterableView = filterableView;
		}

		// Token: 0x170049FC RID: 18940
		// (get) Token: 0x0600F57B RID: 62843 RVA: 0x0037BFC1 File Offset: 0x0037A1C1
		public RadFilterFilterableView FilterableView
		{
			get
			{
				return this._filterableView;
			}
		}

		// Token: 0x0400464A RID: 17994
		private RadFilterFilterableView _filterableView;
	}
}
