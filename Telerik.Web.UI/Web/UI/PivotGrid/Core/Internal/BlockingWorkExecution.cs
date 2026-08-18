using System;

namespace Telerik.Web.UI.PivotGrid.Core.Internal
{
	// Token: 0x020006E6 RID: 1766
	internal class BlockingWorkExecution : WorkExecutionContext
	{
		// Token: 0x06003EF9 RID: 16121 RVA: 0x000C87D5 File Offset: 0x000C69D5
		public override void Execute()
		{
			if (base.ActionToExecute == null)
			{
				return;
			}
			base.ActionToExecute();
		}
	}
}
