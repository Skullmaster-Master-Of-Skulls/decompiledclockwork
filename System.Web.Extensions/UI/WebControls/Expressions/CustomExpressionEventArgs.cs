using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000CA RID: 202
	public class CustomExpressionEventArgs : EventArgs
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00025F0A File Offset: 0x0002410A
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00025F12 File Offset: 0x00024112
		public IQueryable Query { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00025F1B File Offset: 0x0002411B
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00025F23 File Offset: 0x00024123
		public IDictionary<string, object> Values { get; private set; }

		// Token: 0x06000A05 RID: 2565 RVA: 0x00025F2C File Offset: 0x0002412C
		public CustomExpressionEventArgs(IQueryable source, IDictionary<string, object> values)
		{
			this.Query = source;
			this.Values = values;
		}
	}
}
