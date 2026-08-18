using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200008D RID: 141
	public class ContextDataSourceContextData
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x00002050 File Offset: 0x00000250
		public ContextDataSourceContextData()
		{
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001B063 File Offset: 0x00019263
		public ContextDataSourceContextData(object context)
		{
			this.Context = context;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001B072 File Offset: 0x00019272
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0001B07A File Offset: 0x0001927A
		public object Context { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001B083 File Offset: 0x00019283
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0001B08B File Offset: 0x0001928B
		public object EntitySet { get; set; }
	}
}
