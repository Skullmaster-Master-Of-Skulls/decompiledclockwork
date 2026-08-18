using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000396 RID: 918
	public interface IRadFilterableContainer
	{
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06001FA0 RID: 8096
		// (remove) Token: 0x06001FA1 RID: 8097
		event EventHandler<RadFilterFildDesciptorsEventArgs> FieldDescriptorsReady;

		// Token: 0x06001FA2 RID: 8098
		void ApplyFilterExpressions(RadFilterGroupExpression expressionRoot, bool shouldBind);
	}
}
