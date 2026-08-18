using System;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200002F RID: 47
	internal class OverriddenBuildPlanMarkerPolicy : IBuildPlanPolicy, IBuilderPolicy
	{
		// Token: 0x060000AF RID: 175 RVA: 0x0000367C File Offset: 0x0000187C
		public void BuildUp(IBuilderContext context)
		{
			throw new InvalidOperationException(Resources.MarkerBuildPlanInvoked);
		}
	}
}
