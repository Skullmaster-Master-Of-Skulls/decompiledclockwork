using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200004C RID: 76
	public class BuildPlanStrategy : BuilderStrategy
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00004538 File Offset: 0x00002738
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public override void PreBuildUp(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			IPolicyList policyList;
			IBuildPlanPolicy buildPlanPolicy = context.Policies.Get(context.BuildKey, out policyList);
			if (buildPlanPolicy == null || buildPlanPolicy is OverriddenBuildPlanMarkerPolicy)
			{
				IPolicyList policyList2;
				IBuildPlanCreatorPolicy buildPlanCreatorPolicy = context.Policies.Get(context.BuildKey, out policyList2);
				if (buildPlanCreatorPolicy != null)
				{
					buildPlanPolicy = buildPlanCreatorPolicy.CreatePlan(context, context.BuildKey);
					(policyList ?? policyList2).Set(buildPlanPolicy, context.BuildKey);
				}
			}
			if (buildPlanPolicy != null)
			{
				buildPlanPolicy.BuildUp(context);
			}
		}
	}
}
