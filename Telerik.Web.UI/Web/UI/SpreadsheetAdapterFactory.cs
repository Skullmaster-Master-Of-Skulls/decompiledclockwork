using System;

namespace Telerik.Web.UI
{
	// Token: 0x020008AC RID: 2220
	internal class SpreadsheetAdapterFactory : ISpreadsheetAdapterFactory
	{
		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x0600526D RID: 21101 RVA: 0x001005EF File Offset: 0x000FE7EF
		// (set) Token: 0x0600526E RID: 21102 RVA: 0x001005F7 File Offset: 0x000FE7F7
		public ISpreadsheet Owner { get; set; }

		// Token: 0x0600526F RID: 21103 RVA: 0x00100600 File Offset: 0x000FE800
		public SpreadsheetAdapterFactory(ISpreadsheet owner)
		{
			this.Owner = owner;
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x0010060F File Offset: 0x000FE80F
		public ISpreadsheetAdapter CreateAdapter()
		{
			return new SpreadsheetToolbarAdapter(this.Owner);
		}
	}
}
