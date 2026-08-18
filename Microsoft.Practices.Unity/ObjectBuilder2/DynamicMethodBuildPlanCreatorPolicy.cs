using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200005A RID: 90
	public class DynamicMethodBuildPlanCreatorPolicy : IBuildPlanCreatorPolicy, IBuilderPolicy
	{
		// Token: 0x06000186 RID: 390 RVA: 0x000058A4 File Offset: 0x00003AA4
		public DynamicMethodBuildPlanCreatorPolicy(IStagedStrategyChain strategies)
		{
			this.strategies = strategies;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000058B4 File Offset: 0x00003AB4
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public IBuildPlanPolicy CreatePlan(IBuilderContext context, NamedTypeBuildKey buildKey)
		{
			Guard.ArgumentNotNull(buildKey, "buildKey");
			DynamicBuildPlanGenerationContext dynamicBuildPlanGenerationContext = new DynamicBuildPlanGenerationContext(buildKey.Type);
			IBuilderContext context2 = this.GetContext(context, buildKey, dynamicBuildPlanGenerationContext);
			context2.Strategies.ExecuteBuildUp(context2);
			return new DynamicMethodBuildPlan(dynamicBuildPlanGenerationContext.GetBuildMethod());
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000058FA File Offset: 0x00003AFA
		private IBuilderContext GetContext(IBuilderContext originalContext, NamedTypeBuildKey buildKey, DynamicBuildPlanGenerationContext generatorContext)
		{
			return new BuilderContext(this.strategies.MakeStrategyChain(), originalContext.Lifetime, originalContext.PersistentPolicies, originalContext.Policies, buildKey, generatorContext);
		}

		// Token: 0x04000059 RID: 89
		private IStagedStrategyChain strategies;
	}
}
