using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000473 RID: 1139
	public class RadFilterExpressionItemCreatedEventArgs : EventArgs
	{
		// Token: 0x060028E3 RID: 10467 RVA: 0x000844A0 File Offset: 0x000826A0
		public RadFilterExpressionItemCreatedEventArgs(RadFilterExpressionItem item)
		{
			this.item = item;
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060028E4 RID: 10468 RVA: 0x000844AF File Offset: 0x000826AF
		public RadFilterExpressionItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04000A59 RID: 2649
		private RadFilterExpressionItem item;
	}
}
