using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000005 RID: 5
	public interface IBuildPlanCreatorPolicy : IBuilderPolicy
	{
		// Token: 0x06000005 RID: 5
		IBuildPlanPolicy CreatePlan(IBuilderContext context, NamedTypeBuildKey buildKey);
	}
}
