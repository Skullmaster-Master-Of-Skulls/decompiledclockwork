using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CE1 RID: 3297
	internal static class GlobalOptions
	{
		// Token: 0x1700276B RID: 10091
		// (get) Token: 0x06007B36 RID: 31542 RVA: 0x001C4806 File Offset: 0x001C2A06
		public static OperationExecutionStrategy PreferredExecutionStrategy
		{
			get
			{
				return OperationExecutionStrategy.Blocking;
			}
		}
	}
}
