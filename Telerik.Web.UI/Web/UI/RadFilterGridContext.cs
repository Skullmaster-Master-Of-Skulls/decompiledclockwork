using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018CC RID: 6348
	internal class RadFilterGridContext
	{
		// Token: 0x0600F59E RID: 62878 RVA: 0x0037C300 File Offset: 0x0037A500
		public RadFilterGridContext() : this(GridFilterExpressionType.Sql)
		{
		}

		// Token: 0x0600F59F RID: 62879 RVA: 0x0037C309 File Offset: 0x0037A509
		public RadFilterGridContext(GridFilterExpressionType expressionType)
		{
			this.ExpressionType = expressionType;
		}

		// Token: 0x17004A04 RID: 18948
		// (get) Token: 0x0600F5A0 RID: 62880 RVA: 0x0037C318 File Offset: 0x0037A518
		// (set) Token: 0x0600F5A1 RID: 62881 RVA: 0x0037C320 File Offset: 0x0037A520
		public GridFilterExpressionType ExpressionType { get; set; }
	}
}
