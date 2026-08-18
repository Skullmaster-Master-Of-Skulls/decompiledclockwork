using System;
using System.Linq;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000C6 RID: 198
	public class QueryCreatedEventArgs : EventArgs
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00025B3C File Offset: 0x00023D3C
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x00025B44 File Offset: 0x00023D44
		public IQueryable Query { get; set; }

		// Token: 0x060009E3 RID: 2531 RVA: 0x00025B4D File Offset: 0x00023D4D
		public QueryCreatedEventArgs(IQueryable query)
		{
			this.Query = query;
		}
	}
}
